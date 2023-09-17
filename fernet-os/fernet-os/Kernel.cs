using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using Cosmos.System.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Sys = Cosmos.System;
using System.IO;

namespace fernet_os
{
    public class Kernel : Sys.Kernel
    {
        public static CosmosVFS fs = new();
        protected override void BeforeRun()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to fernet OS. Type commandslist to see all commands");
            Console.WriteLine("||||||||||||||||||||||");
            Console.WriteLine("|                    |");
            Console.WriteLine("|                    |");
            Console.WriteLine("|      fernet_os     |");
            Console.WriteLine("|                    |");
            Console.WriteLine("|                    |");
            Console.WriteLine("||||||||||||||||||||||");
            File.Create("0:\readme.txt");
            File.WriteAllText("0:\test.txt", "Fernet_os is os made by NikitaPos");
            try
            {
                VFSManager.RegisterVFS(fs);
            }
            catch (Exception)
            {
                Console.WriteLine("Filesystem initialization failed!");
                Console.ReadKey();
                Cosmos.System.Power.Shutdown();
            }
            Thread.Sleep(3500);
            Canvas boot = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
            boot.Clear();
            var cY = 480 / 2 - 100;
            while (cY != 480 / 2)
            {
                Cosmos.System.MouseManager.ScreenWidth = 640;
                Cosmos.System.MouseManager.ScreenHeight = 480;
                boot.Clear(Color.Black);
                boot.DrawString("Fernet-OS", 640 / 4, 480 / 2);
                cY++;
                boot.Display();
            }
boot.Disable();
        }
        
        public static bool gui = false;
        public static Canvas guiDisplay;
        protected override void Run()
        {

        shell:

            Console.Write("fernet_os> ");
            var input = Console.ReadLine();

            if (input == "commandslist")

            {

                Console.WriteLine("launch - turn on visual mode");
                Console.WriteLine("clear - clear console from text");
                Console.WriteLine("color settings - color options");
                Console.WriteLine("credits - credits");

            }

            if (input == "clear")

            {

                Console.Clear();

            }

            if (input == "credits")

            {

                Console.WriteLine("Special thanks to PratyushKing");

            }
            
            if (input == "help")

            {

                Console.WriteLine("What do you need help with?(write: guihelp, colorhelp");

            }

            if (input == "guihelp")

            {

                Console.WriteLine("Write launch");

            }

            if (input == "colorhelp")

            {

                Console.WriteLine("To change color of text write in console name of color from color list");
                Console.WriteLine("To see color list write in console name of color from color settings");

            }

            if (input == "")

            {

                Console.WriteLine("That is not command");

            }

            if (input == "color settings")

            {
                Console.WriteLine("green - change color of text to green (console)");
                Console.WriteLine("blue - change color of text to blue (console)");
                Console.WriteLine("white - change color of text to white (console)");
                Console.WriteLine("yellow - change color of text to orange (console)");
                Console.WriteLine("cyan - change color of text to brown (console)");

            }

            if (input == "green")

            {

                Console.ForegroundColor = ConsoleColor.Green;

            }

            if (input == "blue")

            {

                Console.ForegroundColor = ConsoleColor.Blue;

            }

            if (input == "white")

            {

                Console.ForegroundColor = ConsoleColor.White;

            }

            if (input == "yellow")

            {

                Console.ForegroundColor = ConsoleColor.Yellow;

            }

            if (input == "cyan")

            {

                Console.ForegroundColor = ConsoleColor.Cyan;

            }

            if (input.StartsWith("touch "))

            {

                File.Create(input.Replace("touch ", "0:\\")); //this will create file!
                
            }

            if (input.StartsWith("rm "))

            {

                File.Create(input.Replace("rm ", "0:\\")); //this will create file!
                
            }

            
            if (!gui)
            {
                
                if (input == "launch")
                {
                    gui = true;
                    Sys.MouseManager.ScreenWidth = 640;
                    Sys.MouseManager.ScreenHeight = 480;
                    var x = 640;
                    var y = 480;
                    guiDisplay = FullScreenCanvas.GetFullScreenCanvas(new Mode(640, 480, ColorDepth.ColorDepth32));
                    var yourcolor = Color.MediumPurple;
                    guiDisplay.Clear(yourcolor);
                    guiDisplay.DrawFilledCircle(Color.White, (int)Sys.MouseManager.X, (int)Sys.MouseManager.Y, 5);
                    guiDisplay.Display();
                }
            }

            else
            {
                
            }
            goto shell;
        }
    }
}
