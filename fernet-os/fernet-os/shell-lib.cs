using System;
using System.Data;

namespace FalGUI_LIB;
public class FalGUI
{
    public ConsoleColor backgroundC = ConsoleColor.DarkCyan;
    private string currentOS = "UNKNOWN v0.1";
    private bool allowWatermark = false;
    private ConsoleColor[] list = { ConsoleColor.Black, ConsoleColor.DarkBlue, ConsoleColor.DarkGreen, ConsoleColor.DarkCyan, ConsoleColor.DarkRed,
                                    ConsoleColor.DarkMagenta, ConsoleColor.DarkYellow, ConsoleColor.Gray, ConsoleColor.DarkGray, ConsoleColor.Blue,
                                    ConsoleColor.Green, ConsoleColor.Cyan, ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.White};
    private int cBgPointer = 0;

    public FalGUI()
    {
        Console.BackgroundColor = ConsoleColor.DarkGray;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Booting up FalGUI! (If it doesn't boot in 3 seconds, there's a problem)");
        Console.Beep();
    }

    public void Configure(ConsoleColor bgColor, string OSName, string verName, bool AllowWatermark = true)
    {
        backgroundC = bgColor;
        currentOS = OSName + " " + verName;
        allowWatermark = AllowWatermark;
        for (var i = 0; i < list.Length; i++)
        {
            if (list[i] == backgroundC)
            {
                cBgPointer = i;
                break;
            }
        }
    }

    private void setXY(int x, int y) { Console.CursorLeft = x; Console.CursorTop = y; }

    public void Run()
    {
        redraw_whole:
        Console.BackgroundColor = backgroundC;
        Console.Clear();
        if (allowWatermark)
        {
            Console.CursorLeft = 1; Console.CursorTop = 1;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(currentOS);
        }
        Console.CursorVisible = false;

        Console.BackgroundColor = backgroundC;
        Console.Clear();
        Console.CursorLeft = 1; Console.CursorTop = 1;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(currentOS);
        Console.CursorVisible = false;

        var focus = "home";
        var selection = 0;
        redraw_home:
        Console.BackgroundColor = backgroundC;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.CursorTop = 3;
        Console.CursorLeft = 2;
        Console.WriteLine("<Applications>");
        Console.BackgroundColor = backgroundC;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.CursorTop = 5;
        Console.CursorLeft = 2;
        Console.WriteLine("<Settings>");
        Console.BackgroundColor = backgroundC;
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.CursorTop = 7;
        Console.CursorLeft = 2;
        Console.WriteLine("<Exit Interface>");
        if (selection == 0)
        {
            Console.CursorTop = 5;
            Console.CursorLeft = 2 + "<Settings>".Length;
            Console.Write(new String(' ', (" Change settings on " + currentOS).Length));

            Console.CursorTop = 7;
            Console.CursorLeft = 2 + "<Exit Interface>".Length;
            Console.Write(new String(' ', "<Exit Interface> Leave the interface.".Length));
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorTop = 3;
            Console.CursorLeft = 2;
            Console.Write("<Applications>");
            Console.BackgroundColor = backgroundC;
            Console.Write(" Launch applications.");
        } else if (selection == 1)
        {
            Console.CursorTop = 3;
            Console.CursorLeft = 2 + "<Applications>".Length;
            Console.Write(new String(' ', "<Applications> Launch applications.".Length));

            Console.CursorTop = 7;
            Console.CursorLeft = 2 + "<Exit Interface>".Length;
            Console.Write(new String(' ', "<Exit Interface> Leave the interface.".Length));
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorTop = 5;
            Console.CursorLeft = 2;
            Console.Write("<Settings>");
            Console.BackgroundColor = backgroundC;
            Console.Write(" Change settings on " + currentOS);
        } else if (selection == 2)
        {
            Console.CursorTop = 3;
            Console.CursorLeft = 2 + "<Applications>".Length;
            Console.Write(new String(' ', "<Applications> Launch applications.".Length));

            Console.CursorTop = 5;
            Console.CursorLeft = 2 + "<Settings>".Length;
            Console.Write(new String(' ', (" Change settings on " + currentOS).Length));
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;

            Console.CursorTop = 7;
            Console.CursorLeft = 2;
            Console.Write("<Exit Interface>");
            Console.BackgroundColor = backgroundC;
            Console.Write(" Leave the interface.");
        }
        var currentY = 5;
        while (true)
        {
            if (focus == "home")
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && (selection == 1 || selection == 2))
                {
                    selection--;
                } else if (key.Key == ConsoleKey.DownArrow && (selection == 0 || selection == 1))
                {
                    selection++;
                } else if (key.Key == ConsoleKey.Enter)
                {
                    if (selection == 1) { focus = "settings"; }
                    else if (selection == 0) { focus = "apppage"; }
                    else if (selection == 2) { Console.ResetColor(); Console.Clear(); Console.WriteLine("User has exited."); return; }
                }
                goto redraw_home;
            } else if (focus == "apppage")
            {
                Console.CursorLeft = 1;
                Console.CursorTop = 2;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                for (var l = 2; l < Console.WindowHeight - 1; l++)
                {
                    Console.CursorTop = l;
                    for (var i = 1; i < Console.WindowWidth - 1; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.CursorLeft = 1;
                }

                Console.CursorLeft = 2;
                Console.CursorTop = 3;
                Console.WriteLine("Your Applications!");
                Console.CursorTop = 3;
                Console.CursorLeft = Console.WindowWidth - 4 - ("Esc to close").Length;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Esc to close");

                var sel = 0;
                setXY(2, 4);
                redraw_options_for_apppage:
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorTop = 5;
                Console.CursorLeft = 2;
                Console.WriteLine("<Terminal>");
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorTop = 7;
                Console.CursorLeft = 2;
                Console.WriteLine("<System Information>");
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorTop = 9;
                Console.CursorLeft = 2;
                Console.WriteLine("<Text Editor>");
                if (sel == 0)
                {
                    Console.CursorTop = 7;
                    Console.CursorLeft = 2 + "<System Information>".Length;
                    Console.Write(new String(' ', (" Get information on your OS!").Length));
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.CursorTop = 5;
                    Console.CursorLeft = 2;
                    Console.Write("<Terminal>");
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" Your console!");
                }
                else
                {
                    Console.CursorTop = 5;
                    Console.CursorLeft = 2 + "<Terminal>".Length;
                    Console.Write(new String(' ', "<Terminal> Your console!".Length));
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.CursorTop = 7;
                    Console.CursorLeft = 2;
                    Console.Write("<System Information>");
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" Get information on your OS!");
                }

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    focus = "home";
                    goto redraw_whole;
                } else if (key.Key == ConsoleKey.UpArrow && sel == 1)
                {
                    sel = 0;
                    goto redraw_options_for_apppage;
                }
                else if (key.Key == ConsoleKey.DownArrow && sel == 0)
                {
                    sel = 1;
                    goto redraw_options_for_apppage;
                } else if (key.Key == ConsoleKey.Enter)
                {
                    if (sel == 0) { focus = "apppage_console"; }
                    else if (sel == 1) { focus = "apppage_sysinfo"; }
                }
            } else if (focus == "settings")
            {
                Console.CursorLeft = 1;
                Console.CursorTop = 2;
                Console.BackgroundColor = ConsoleColor.DarkGray;
                for (var l = 2; l < Console.WindowHeight - 1; l++)
                {
                    Console.CursorTop = l;
                    for (var i = 1; i < Console.WindowWidth - 1; i++)
                    {
                        Console.Write(' ');
                    }
                    Console.CursorLeft = 1;
                }

                Console.CursorLeft = 2;
                Console.CursorTop = 3;
                Console.WriteLine(currentOS + " Settings");
                Console.CursorTop = 3;
                Console.CursorLeft = Console.WindowWidth - 4 - ("Esc to save and close").Length;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Esc to save and close");

                Console.ForegroundColor = ConsoleColor.Magenta;
                setXY(2, 5);

                var sel = 0;
                redraw_settingspage:
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorTop = 7;
                Console.CursorLeft = 2;
                Console.WriteLine("<  Background: " + backgroundC + "  >");
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorTop = 9;
                Console.CursorLeft = 2;
                Console.WriteLine($"<Disable OS Watermark: {allowWatermark}>");
                if (sel == 0)
                {
                    Console.CursorTop = 9;
                    Console.CursorLeft = 2 + $"<Disable OS Watermark: {allowWatermark}>".Length;
                    Console.Write(new String(' ', ($"<Disable OS Watermark: {allowWatermark}> Remove the watermark.").Length));
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.CursorTop = 7;
                    Console.CursorLeft = 2;
                    Console.Write("<  Background: " + backgroundC + "  >");
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" Change background color.             ");
                }
                else if (sel == 1)
                {
                    Console.CursorTop = 7;
                    Console.CursorLeft = 2 + ("<  Background: " + backgroundC + "  >").Length;
                    Console.Write(new String(' ', ("<  Background: " + backgroundC + "  > Change background color.             ").Length));
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.CursorTop = 9;
                    Console.CursorLeft = 2;
                    Console.Write($"<Disable OS Watermark: {allowWatermark}>");
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(" Remove the watermark.");
                }

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    focus = "home";
                    goto redraw_whole;
                } else if (key.Key == ConsoleKey.UpArrow && sel == 1)
                {
                    sel = 0;
                    goto redraw_settingspage;
                }
                else if (key.Key == ConsoleKey.DownArrow && sel == 0)
                {
                    sel = 1;
                    goto redraw_settingspage;
                } else if (key.Key == ConsoleKey.RightArrow)
                {
                    if (sel == 0) { if (cBgPointer < list.Length - 1) { cBgPointer++; backgroundC = list[cBgPointer]; goto redraw_settingspage; }
                        else { cBgPointer = 0; backgroundC = list[cBgPointer]; goto redraw_settingspage; } }
                    
                    
                    if (sel == 1) { allowWatermark = !allowWatermark; goto redraw_settingspage; }
                } else if (key.Key == ConsoleKey.LeftArrow)
                {
                    if (sel == 0) { if (cBgPointer > 0) { cBgPointer--; backgroundC = list[cBgPointer]; goto redraw_settingspage; }
                        else { cBgPointer = 0; backgroundC = list[cBgPointer]; goto redraw_settingspage; } }
                    
                    if (sel == 1) { allowWatermark = !allowWatermark; goto redraw_settingspage; }
                }
            } else
            {
                if (focus == "apppage_console")
                {
                redraw_app_apppage_console:
                    currentY = 5;
                    Console.CursorLeft = 1;
                    Console.CursorTop = 2;
                    Console.BackgroundColor = ConsoleColor.Black;
                    for (var l = 2; l < Console.WindowHeight - 1; l++)
                    {
                        Console.CursorTop = l;
                        for (var i = 1; i < Console.WindowWidth - 1; i++)
                        {
                            Console.Write(' ');
                        }
                        Console.CursorLeft = 1;
                    }

                    Console.CursorLeft = 2;
                    Console.CursorTop = 3;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine("Terminal");
                    Console.CursorTop = 3;
                    Console.CursorLeft = Console.WindowWidth - 4 - ("Esc to close").Length;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("Esc to close");

                    var buffer = "";
                    redraw_shell:
                    Console.CursorLeft = 3;
                    Console.CursorTop = currentY;
                    Console.ForegroundColor = backgroundC;
                    Console.Write(">> ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.CursorVisible = true;

                    var inp = "";
                    var key = Console.ReadKey();
                    while (key.Key != ConsoleKey.Escape)
                    {
                        if (Console.CursorTop > Console.WindowHeight - 5)
                        {
                            goto redraw_app_apppage_console;
                        }
                        if (key.Key == ConsoleKey.Enter)
                        {
                            if (Console.CursorTop > Console.WindowHeight - 5)
                            {
                                goto redraw_app_apppage_console;
                            }
                            Console.CursorLeft = 3;
                            currentY++;
                            Console.CursorTop = currentY;
                            if (inp == "exit")
                            {
                                focus = "home";
                                goto redraw_whole;
                            } else if (inp.StartsWith("echo "))
                            {
                                var cT = 0;
                                Console.CursorTop = currentY;
                                Console.Write(' ');
                                for (var i = 0; i < inp.Replace("echo ", "").Length; i++)
                                {
                                    if (cT > Console.WindowWidth - 15)
                                    {
                                        Console.CursorLeft = 3;
                                        currentY++;
                                        Console.CursorTop = currentY;
                                        Console.Write(' ');
                                        cT = 0;
                                    }
                                    
                                    Console.Write(inp.Replace("echo ","")[i]);
                                    buffer += inp.Replace("echo ", "")[i];
                                    Console.CursorTop = currentY;
                                    cT++;
                                }
                            }
                            inp = "";
                            Console.CursorLeft = 3 + ">> ".Length;
                            Console.CursorTop = currentY;
                            currentY++;
                            Console.Write(new string(' ', inp.Length));
                            if (Console.CursorTop > Console.WindowHeight - 5)
                            {
                                goto redraw_app_apppage_console;
                            }
                            goto redraw_shell;
                        }
                        else if (key.Key == ConsoleKey.Backspace)
                        {
                            if (Console.CursorTop > Console.WindowHeight - 5)
                            {
                                goto redraw_app_apppage_console;
                            }
                            if (inp.Length > 0 && Console.CursorLeft > ">> ".Length)
                            {
                                inp = inp.Remove(inp.Length - 1, 1);
                                Console.Write(' ');
                                Console.CursorLeft--;
                            } else { Console.Beep(); Console.CursorLeft = 3 + ">> ".Length; }
                        }
                        else
                        {
                            if (Console.CursorLeft > Console.WindowWidth - 15)
                            {
                                Console.CursorTop++; currentY++;
                                Console.CursorLeft = 3;
                            }
                            if (Console.CursorTop > Console.WindowHeight - 5)
                            {
                                goto redraw_app_apppage_console;
                            }
                            inp += key.KeyChar;
                        }
                        Console.CursorTop = currentY;
                        key = Console.ReadKey();
                    }
                    if (key.Key == ConsoleKey.Escape)
                    {
                        focus = "home";
                        goto redraw_whole;
                    }
                } else if (focus == "apppage_sysinfo")
                {
                    Console.CursorLeft = 1;
                    Console.CursorTop = 2;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    for (var l = 2; l < Console.WindowHeight - 1; l++)
                    {
                        Console.CursorTop = l;
                        for (var i = 1; i < Console.WindowWidth - 1; i++)
                        {
                            Console.Write(' ');
                        }
                        Console.CursorLeft = 1;
                    }

                    Console.CursorLeft = 2;
                    Console.CursorTop = 3;
                    Console.WriteLine("System Info");
                    Console.CursorTop = 3;
                    Console.CursorLeft = Console.WindowWidth - 4 - ("Esc to close").Length;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Esc to close");

                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.CursorLeft = 5;
                    Console.CursorTop = 5;
                    Console.Write("OS Name: " + currentOS);
                    Console.CursorTop++;
                    Console.CursorLeft = 5;
                    Console.WriteLine("Running on FalGUI interface.");

                    var key = Console.ReadKey();
                    if (key.Key == ConsoleKey.Escape)
                    {
                        focus = "home";
                        goto redraw_whole;
                    }
                }
            }
        }
    }
}
