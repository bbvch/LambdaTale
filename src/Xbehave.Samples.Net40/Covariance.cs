﻿// <copyright file="Covariance.cs" company="Adam Ralph">
//  Copyright (c) Adam Ralph. All rights reserved.
// </copyright>

namespace Xbehave.Samples
{
    using System;

    using FluentAssertions;

    public class Covariance
    {
        [Scenario]
        public static void UseCovariance()
        {
            var disposable = default(ImplicitDisposable);
            Func<ImplicitDisposable> func = () => disposable = new ImplicitDisposable();

            "Given a disposable,"
                .Given(func)
                .When(() => disposable.ToString())
                .ThenInIsolation(() => true.Should().Be(false));
        }

        [Scenario]
        public static void Inline()
        {
            var disposable = default(ImplicitDisposable);

            "Given a disposable,"
                .Given(() =>
                {
                    disposable = new ImplicitDisposable();
                    return disposable;
                })
                .When(() => disposable.ToString())
                .ThenInIsolation(() => true.Should().Be(false));
        }

        public sealed class ImplicitDisposable : IDisposable
        {
            public void Dispose()
            {
                Console.WriteLine("DISPOSED");
            }
        }
    }
}