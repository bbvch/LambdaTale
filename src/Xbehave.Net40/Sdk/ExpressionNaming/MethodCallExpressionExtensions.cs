﻿// <copyright file="MethodCallExpressionExtensions.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace Xbehave.Sdk.ExpressionNaming
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal static class MethodCallExpressionExtensions
    {
        public static IEnumerable<string> ToTokens(this MethodCallExpression expression)
        {
            if (expression == null)
            {
                yield break;
            }

            var isExtensionMethod = expression.Method.IsExtension();

            var argTokens = expression.Arguments
                .Skip(isExtensionMethod ? 1 : 0).Select(x => x.ToTokens())
                .Select(tokens => string.Join(" ", tokens.Reverse().ToArray()));

            foreach (var token in argTokens.AddListSeperators().Reverse())
            {
                yield return token;
            }

            var genericArgTokens = new List<string>();
            if (expression.Method.IsGenericMethod)
            {
                var genericMethod = expression.Method.GetGenericMethodDefinition();
                var parameterTypes = genericMethod.GetParameters().Select(parameter => parameter.ParameterType);

                if (!genericMethod.GetGenericArguments()
                         .All(genericType => parameterTypes.Any(parameterType => parameterType.AllowsInferenceOf(genericType))))
                {
                    genericArgTokens.AddRange(expression.Method.GetGenericArguments().Select(arg => arg.Name.ToToken()));
                }
            }

            if (genericArgTokens.Any() && argTokens.Any())
            {
                yield return "with";
            }

            foreach (var token in genericArgTokens.AddListSeperators().Reverse())
            {
                yield return token;
            }

            yield return expression.Method.Name.ToToken();

            if (isExtensionMethod)
            {
                foreach (var token in expression.Arguments[0].ToTokens())
                {
                    yield return token;
                }
            }

            if (expression.Object != null)
            {
                foreach (var token in expression.Object.ToTokens())
                {
                    yield return token;
                }
            }
            else if (!isExtensionMethod &&
                !expression.Method.DeclaringType.IsCompilerGenerated() &&
                !expression.Method.DeclaringType.IsIgnored())
            {
                yield return expression.Method.DeclaringType.Name.ToToken();
            }
        }
    }
}
