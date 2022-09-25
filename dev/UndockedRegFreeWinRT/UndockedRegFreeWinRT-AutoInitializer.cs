// Copyright (c) Microsoft Corporation and Contributors.
// Licensed under the MIT License.

// <auto-generated>
// Exclude this file from StyleCop analysis. This file isn't generated but is added to projects.
// DO NOT MODIFY. Changes to this file may cause incorrect behavior and will be lost on updates.
// </auto-generated>

using System.Reflection;
using System.Runtime.InteropServices;

namespace Microsoft.Windows.Foundation.UndockedRegFreeWinRTCS
{
    internal static class NativeMethods
    {
        [DllImport("Microsoft.WindowsAppRuntime.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        internal static extern int WindowsAppRuntime_EnsureIsLoaded();
    }

    class AutoInitialize
    {
        [global::System.Runtime.CompilerServices.ModuleInitializer]
        internal static void AccessWindowsAppSDK()
        {
            // No error handling needed as the target function does nothing (just {return S_OK}).
            // It's the act of calling the function causing the DllImport to load the DLL that
            // matters. This provides the moral equivalent of a native DLL's Import Address
            // Table (IAT) have an entry that's resolved when this module is loaded.
            NativeMethods.WindowsAppRuntime_EnsureIsLoaded();
        }
    }
}
