// adapted from: https://raw.githubusercontent.com/xunit/assert.xunit/2.6.1/Guards.cs
using System;

namespace Xunit
{
    internal class XunitAssert
    {
        internal static T GuardArgumentNotNull<T>(string argName, T argValue) =>
            argValue ?? throw new ArgumentNullException(argName.TrimStart('@'));
    }
}
