﻿using System;

namespace Ton618.Utilities
{
    /// <summary>
    /// Provides access to the Win32 API.
    /// </summary>
    internal static partial class ClientNatives
    {
        
        internal delegate bool SetCursorPosFunc(int x, int y);
        internal static bool SetCursorPos(int x, int y) => Lookup<SetCursorPosFunc>("user32.dll", "SetCursorPos")(x, y);

        
        internal delegate bool SystemParametersInfoFunc(uint uAction, uint uParam, ref IntPtr lpvParam, uint flags);
        internal static bool SystemParametersInfo(uint uAction, uint uParam, ref IntPtr lpvParam, uint flags) => Lookup<SystemParametersInfoFunc>("user32.dll", "SystemParametersInfoW")(uAction, uParam, ref lpvParam, flags);
    }
}
