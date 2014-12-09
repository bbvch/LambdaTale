﻿// <copyright file="StepInvoker.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

namespace Xbehave.Execution
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;
    using Xbehave.Sdk;
    using Xunit.Abstractions;
    using Xunit.Sdk;

    public class StepInvoker : XbehaveDelegateInvoker
    {
        private readonly string stepDisplayName;
        private readonly Step step;
        private readonly List<Action> teardowns = new List<Action>();

        public StepInvoker(
            string stepDisplayName,
            Step step,
            ITest test,
            IMessageBus messageBus,
            Type testClass,
            object[] constructorArguments,
            MethodInfo testMethod,
            object[] testMethodArguments,
            IReadOnlyList<BeforeAfterTestAttribute> beforeAfterAttributes,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
            : base(
                test,
                messageBus,
                testClass,
                constructorArguments,
                testMethod,
                testMethodArguments,
                beforeAfterAttributes,
                aggregator,
                cancellationTokenSource)
        {
            Guard.AgainstNullArgument("step", step);

            this.stepDisplayName = stepDisplayName;
            this.step = step;
        }

        public string StepDisplayName
        {
            get { return this.stepDisplayName; }
        }

        public IEnumerable<Action> Teardowns
        {
            get { return this.teardowns.ToArray(); }
        }

        protected override async Task InvokeDelegatesAsync()
        {
            try
            {
                var result = this.step.Body();
                var task = result as Task;
                if (task != null)
                {
                    await task;
                }
            }
            finally
            {
                this.teardowns.AddRange(this.step.Disposables.Select(disposable => (Action)disposable.Dispose));
                this.teardowns.AddRange(this.step.Teardowns);
            }
        }
    }
}