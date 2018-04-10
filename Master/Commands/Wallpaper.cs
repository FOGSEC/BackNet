﻿using System.Collections.Generic;
using Master.AdvancedConsole;
using Master.Commands.Core;

namespace Master.Commands
{
    internal class Wallpaper : IMasterCommand
    {
        public string name { get; } = "wallpaper";

        public string description { get; } = "Set the remote slave's wallpaper";

        public bool isLocal { get; } = false;

        public List<string> validArguments { get; } = new List<string>()
        {
            "?:[remoteFileName]"
        };


        public void Process(List<string> args)
        {
            if (MasterCommandsManager.networkManager.ReadLine() == "OK")
            {
                ColorTools.WriteCommandSuccess("Wallpaper changed");
            }
            else
            {
                ColorTools.WriteCommandError("File not found on the remote slave");
            }
        }
    }
}