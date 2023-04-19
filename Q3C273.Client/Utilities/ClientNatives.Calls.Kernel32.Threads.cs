﻿using System;
using System.Runtime.InteropServices;
using Ton618.Win32PE.Structs;

namespace Ton618.Utilities
{
    internal static partial class ClientNatives
    {
        internal delegate int GetThreadIdProc(IntPtr threadHandle);
        internal static int GetThreadId(IntPtr threadHandle) => Lookup<GetThreadIdProc>("kernel32.dll", "GetThreadId")(threadHandle);

        internal delegate uint GetCurrentThreadIdProc();
        internal static uint GetCurrentThreadId() => Lookup<GetCurrentThreadIdProc>("kernel32.dll", "GetCurrentThreadId")();

        internal delegate IntPtr OpenThreadProc(
            [In] ThreadAccessRights dwDesiredAccess,
            [In, MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            [In] int dwProcessId);
        internal static IntPtr OpenThread(
            [In] ThreadAccessRights dwDesiredAccess,
            [In, MarshalAs(UnmanagedType.Bool)] bool bInheritHandle,
            [In] int dwProcessId)
            => Lookup<OpenThreadProc>("kernel32.dll", "OpenThread")(dwDesiredAccess, bInheritHandle, dwProcessId);

        internal delegate bool TerminateThreadProc(IntPtr hThread, uint dwExitCode);
        internal static bool TerminateThread(IntPtr hThread, uint dwExitCode) => Lookup<TerminateThreadProc>("kernel32.dll", "TerminateThread")(hThread, dwExitCode);
    }
}
