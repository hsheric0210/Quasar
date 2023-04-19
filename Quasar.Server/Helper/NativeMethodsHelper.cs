﻿using System;
using Q3C273.Server.Utilities;

namespace Q3C273.Server.Helper
{
    public static class NativeMethodsHelper
    {
        private const int LVM_FIRST = 0x1000;
        private const int LVM_SETITEMSTATE = LVM_FIRST + 43;

        private const int WM_VSCROLL = 277;
        private static readonly IntPtr SB_PAGEBOTTOM = new IntPtr(7);

        public static int MakeWin32Long(short wLow, short wHigh)
        {
            return wLow << 16 | (int)wHigh;
        }

        public static void SetItemState(IntPtr handle, int itemIndex, int mask, int value)
        {
            var lvItem = new NativeMethods.LVITEM
            {
                stateMask = mask,
                state = value
            };

            NativeMethods.SendMessageListViewItem(handle, LVM_SETITEMSTATE, new IntPtr(itemIndex), ref lvItem);
        }

        public static void ScrollToBottom(IntPtr handle)
        {
            NativeMethods.SendMessage(handle, WM_VSCROLL, SB_PAGEBOTTOM, IntPtr.Zero);
        }
    }
}
