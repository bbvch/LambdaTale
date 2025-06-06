using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LambdaTale.Sdk;

internal sealed class StepDefinition : IStepDefinition
{
    public string Text { get; set; }

    public Func<IStepContext, Task> Body { get; set; }

    public ICollection<Func<IStepContext, Task>> Teardowns { get; } = [];

    public string SkipReason { get; set; }

    public RemainingSteps FailureBehavior { get; set; }

    public GetStepDisplayText DisplayTextFunc { get; set; } =
        (stepText, isBackgroundStep) =>
            (isBackgroundStep ? "(Background) " : null) + stepText;

    public IStepDefinition Skip(string reason)
    {
        this.SkipReason = reason;
        return this;
    }

    public IStepDefinition Teardown(Func<IStepContext, Task> action)
    {
        if (action != null)
        {
            this.Teardowns.Add(action);
        }

        return this;
    }

    public IStepDefinition OnFailure(RemainingSteps behavior)
    {
        this.FailureBehavior = behavior;
        return this;
    }

    public IStepDefinition DisplayText(GetStepDisplayText func)
    {
        this.DisplayTextFunc = func;
        return this;
    }

    IStepBuilder IStepBuilder.Skip(string reason) => this.Skip(reason);

    IStepBuilder IStepBuilder.Teardown(Func<IStepContext, Task> action) => this.Teardown(action);

    IStepBuilder IStepBuilder.OnFailure(RemainingSteps behavior) => this.OnFailure(behavior);
}
