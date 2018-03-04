﻿using System;
using System.Collections.Generic;
using System.IO;
using Server.Commands.Core;
using Shared;

namespace Server.Commands
{
    internal class Upload : ICommand
    {
        public string name { get; } = "upload";
        
        
        public void Process(List<string> args)
        {
            // The file doesn't exist on the client side
            if (ServerCommandsManager.networkManager.ReadLine() == "KO") return;

            var dataLength = long.Parse(ServerCommandsManager.networkManager.ReadLine());

            try
            {
                using (var fs = new FileStream(args[1], FileMode.Create))
                {
                    ServerCommandsManager.networkManager.NetworkStreamToStream(fs, dataLength);
                }

                ServerCommandsManager.networkManager.WriteLine("Success");
            }
            catch (Exception)
            {
                // Delete the partially created file
                File.Delete(args[1]);
                ServerCommandsManager.networkManager.WriteLine("Error");
            }
        }
    }
}
