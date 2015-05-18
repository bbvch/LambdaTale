﻿// <copyright file="RemainingSteps.cs" company="xBehave.net contributors">
//  Copyright (c) xBehave.net contributors. All rights reserved.
// </copyright>

namespace Xbehave
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Indicates the behavior of remaining steps when a step fails.
    /// </summary>
    [SuppressMessage("Microsoft.Naming", "CA1717:OnlyFlagsEnumsShouldHavePluralNames", Justification = "Makes sense here.")]
    public enum RemainingSteps
    {
        /// <summary>
        /// Skip remaining steps.
        /// </summary>
        Skip = 0,

        /// <summary>
        /// Run remaining steps.
        /// </summary>
        Run = 1,
    }
}
