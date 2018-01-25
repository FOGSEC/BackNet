﻿using NetworkManager;
using Shared;
using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace Commands
{
    internal class PlaySound : ICommand
    {
        public string name { get; } = "playsound";

        public string description { get; } = "Play a .wav audio file on the remote server";

        public string syntaxHelper { get; } = "playsound [FileName.wav]";

        public bool isLocal { get; } = false;

        public List<string> validArguments { get; } = new List<string>()
        {
            "?"
        };


        public bool PreProcessCommand(List<string> args)
        {
            throw new NotImplementedException();
        }

        public void ClientMethod(List<string> args)
        {
            var result = GlobalNetworkManager.ReadLine();

            switch (result)
            {
                case "InvalidFormat":
                    ColorTools.WriteCommandError("Wrong format, expected .wav file");
                    break;
                case "FileError":
                    ColorTools.WriteCommandError("File not found on the remote server");
                    break;
                default:
                    ColorTools.WriteCommandSuccess("Playing sound");
                    break;
            }
        }

        public void ServerMethod(List<string> args)
        {
            var filename = args[0];

            if (!File.Exists(filename))
            {
                GlobalNetworkManager.WriteLine("FileError");
                return;
            }
            
            try
            {
                var player = new SoundPlayer(filename);
                player.Play();
                GlobalNetworkManager.WriteLine("OK");
            }
            catch (InvalidOperationException)
            {
                GlobalNetworkManager.WriteLine("InvalidFormat");
            }
        }
    }
}
