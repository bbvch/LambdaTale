using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace LambdaTale.Execution;

public class ScenarioInfo
{
    private static readonly ITypeInfo objectType = Reflector.Wrap(typeof(object));

    public string ScenarioDisplayName { get; }

    public MethodInfo MethodToRun { get; }

    public IReadOnlyCollection<object> ConvertedDataRow { get; }

    public ScenarioInfo(IMethodInfo testMethod, object[] dataRow, string scenarioOutlineDisplayName)
    {
        testMethod = testMethod ?? throw new ArgumentNullException(nameof(testMethod));

        var parameters = testMethod.GetParameters().ToList();
        var typeParameters = testMethod.GetGenericArguments().ToList();

        ITypeInfo[] typeArguments;
        if (testMethod.IsGenericMethodDefinition)
        {
            typeArguments = typeParameters
                .Select(typeParameter => InferTypeArgument(typeParameter.Name, parameters, dataRow))
                .ToArray();

            this.MethodToRun = testMethod.MakeGenericMethod(typeArguments).ToRuntimeMethod();
        }
        else
        {
            typeArguments = [];
            this.MethodToRun = testMethod.ToRuntimeMethod();
        }

        var parameterInfos = this.MethodToRun.GetParameters();
        var passedArguments = Reflector.ConvertArguments(dataRow, parameterInfos.Select(p => p.ParameterType).ToArray());

        var generatedArguments = GetGeneratedArguments(
            typeParameters, typeArguments, parameters, parameterInfos, passedArguments.Length);

        var arguments = passedArguments
            .Select(value => new Argument(value))
            .Concat(generatedArguments)
            .ToList();

        this.ScenarioDisplayName = GetScenarioDisplayName(scenarioOutlineDisplayName, typeArguments, parameters, arguments);
        this.ConvertedDataRow = arguments.Select(argument => argument.Value).ToList();
    }

    private static ITypeInfo InferTypeArgument(
        string typeParameterName, List<IParameterInfo> parameters, object[] passedArguments)
    {
        var sawNullValue = false;
        ITypeInfo typeArgument = null;
        for (var index = 0; index < Math.Min(parameters.Count, passedArguments.Length); ++index)
        {
            var parameterType = parameters[index].ParameterType;
            if (parameterType.IsGenericParameter && parameterType.Name == typeParameterName)
            {
                var passedArgument = passedArguments[index];
                if (passedArgument == null)
                {
                    sawNullValue = true;
                }
                else if (typeArgument == null)
                {
                    typeArgument = Reflector.Wrap(passedArgument.GetType());
                }
                else if (typeArgument.Name != passedArgument.GetType().FullName)
                {
                    return objectType;
                }
            }
        }

        return typeArgument == null || (sawNullValue && typeArgument.IsValueType) ? objectType : typeArgument;
    }

    private static IEnumerable<Argument> GetGeneratedArguments(
        List<ITypeInfo> typeParameters,
        ITypeInfo[] typeArguments,
        List<IParameterInfo> parameters,
        ParameterInfo[] parameterInfos,
        int passedArgumentsCount)
    {
        for (var missingArgumentIndex = passedArgumentsCount;
            missingArgumentIndex < parameters.Count;
            ++missingArgumentIndex)
        {
            var parameterInfo = parameterInfos[missingArgumentIndex];
            if (parameterInfo.HasDefaultValue)
            {
                yield return new Argument(parameterInfo.DefaultValue);
                continue;
            }

            var parameterType = parameters[missingArgumentIndex].ParameterType;
            if (parameterType.IsGenericParameter)
            {
                ITypeInfo concreteType = null;
                for (var typeParameterIndex = 0; typeParameterIndex < typeParameters.Count; ++typeParameterIndex)
                {
                    var typeParameter = typeParameters[typeParameterIndex];
                    if (typeParameter.Name == parameterType.Name)
                    {
                        concreteType = typeArguments[typeParameterIndex];
                        break;
                    }
                }

                parameterType = concreteType ??
                                throw new InvalidOperationException(
                                    $"The type of parameter \"{parameters[missingArgumentIndex].Name}\" cannot be resolved.");
            }

            yield return new Argument(((IReflectionTypeInfo)parameterType).Type);
        }
    }

    private static string GetScenarioDisplayName(
        string scenarioOutlineDisplayName,
        IReadOnlyList<ITypeInfo> typeArguments,
        List<IParameterInfo> parameters,
        IReadOnlyList<Argument> arguments)
    {
        var typeArgumentsString = typeArguments.Any()
            ? $"<{string.Join(", ", typeArguments.Select(TypeUtility.ConvertToSimpleTypeName))}>"
            : string.Empty;

        var parameterAndArgumentTokens = new List<string>();
        int parameterIndex;
        for (parameterIndex = 0; parameterIndex < arguments.Count; parameterIndex++)
        {
            if (arguments[parameterIndex].IsGeneratedDefault)
            {
                continue;
            }

            parameterAndArgumentTokens.Add(string.Concat(
                parameterIndex >= parameters.Count ? "???" : parameters[parameterIndex].Name,
                ": ",
                arguments[parameterIndex].ToString()));
        }

        for (; parameterIndex < parameters.Count; parameterIndex++)
        {
            parameterAndArgumentTokens.Add(parameters[parameterIndex].Name + ": ???");
        }

        return $"{scenarioOutlineDisplayName}{typeArgumentsString}({string.Join(", ", parameterAndArgumentTokens)})";
    }

    private sealed class Argument
    {
        private static readonly MethodInfo genericFactoryMethod = CreateGenericFactoryMethod();

        public Argument(Type type)
        {
            this.Value = genericFactoryMethod.MakeGenericMethod(type).Invoke(null, null);
            this.IsGeneratedDefault = true;
        }

        public Argument(object value) => this.Value = value;

        public object Value { get; }

        public bool IsGeneratedDefault { get; }

        public override string ToString() => ArgumentFormatter.Format(this.Value);

        private static MethodInfo CreateGenericFactoryMethod()
        {
            Expression<Func<object>> template = () => CreateDefault<object>();
            return ((MethodCallExpression)template.Body).Method.GetGenericMethodDefinition();
        }

        private static T CreateDefault<T>() => default;
    }
}
