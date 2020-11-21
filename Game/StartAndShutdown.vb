Module StartAndShutdown
    Sub Start_Up()
        'Cool start up screen
        Console.ForegroundColor = ConsoleColor.Green
        Console.Write("Initialising")
        Threading.Thread.Sleep(400) 'Creates 400ms time delay
        Console.Write(".")
        Threading.Thread.Sleep(400)
        Console.Write(".")
        Threading.Thread.Sleep(400)
        Console.WriteLine(".")
        Threading.Thread.Sleep(400)
        Console.Clear() 'Clears console

        Console.ForegroundColor = ConsoleColor.Cyan 'Changes foreground(text) colour to cyan
        'Big text of "Welcome to the quiz"
        Console.WriteLine(" __          __    _                                 _            _    _                            _      ")
        Threading.Thread.Sleep(200)
        Console.WriteLine(" \ \        / /   | |                               | |          | |  | |                          (_)     ")
        Threading.Thread.Sleep(200)
        Console.WriteLine("  \ \  /\  / /___ | |  ___  ___   _ __ ___    ___   | |_  ___    | |_ | |__    ___     __ _  _   _  _  ____")
        Threading.Thread.Sleep(200)
        Console.WriteLine("   \ \/  \/ // _ \| | / __|/ _ \ | '_ ` _ \  / _ \  | __|/ _ \   | __|| '_ \  / _ \   / _` || | | || ||_  /")
        Threading.Thread.Sleep(200)
        Console.WriteLine("    \  /\  /|  __/| || (__| (_) || | | | | ||  __/  | |_  (_) |  | |_ | | | ||  __/  | (_| || |_| || | / / ")
        Threading.Thread.Sleep(200)
        Console.WriteLine("     \/  \/  \___||_| \___|\___/ |_| |_| |_| \___|   \__|\___/    \__||_| |_| \___|   \__, | \__,_||_|/___|")
        Threading.Thread.Sleep(200)
        Console.WriteLine("                                                                                         | |               ")
        Threading.Thread.Sleep(200)
        Console.WriteLine("                                                                                         |_|               ")
        Threading.Thread.Sleep(200)
        Console.ForegroundColor = ConsoleColor.White 'Changes text colour back to white
    End Sub

    Sub Shut_Down()
        'Cool shut down screen
        Console.Clear()
        Console.ForegroundColor = ConsoleColor.Red
        Console.Write("Stopping Services")
        Threading.Thread.Sleep(400)
        Console.Write(".")
        Threading.Thread.Sleep(400)
        Console.Write(".")
        Threading.Thread.Sleep(400)
        Console.WriteLine(".")
        Threading.Thread.Sleep(500)
        Console.Clear()

        Console.ForegroundColor = ConsoleColor.Cyan
        'Creates big text of "Goodbye :)"
        Console.WriteLine("   _____                    _  _                       __  ")
        Threading.Thread.Sleep(200)
        Console.WriteLine("  / ____|                  | || |                     _\ \ ")
        Threading.Thread.Sleep(200)
        Console.WriteLine(" | |  __   ___    ___    __| || |__   _   _   ___    (_)| |")
        Threading.Thread.Sleep(200)
        Console.WriteLine(" | | |_ | / _ \  / _ \  / _` || '_ \ | | | | / _ \      | |")
        Threading.Thread.Sleep(200)
        Console.WriteLine(" | |__| || (_) || (_) || (_| || |_) || |_| ||  __/    _ | |")
        Threading.Thread.Sleep(200)
        Console.WriteLine("  \_____| \___/  \___/  \__,_||_.__/  \__, | \___|   (_)| |")
        Threading.Thread.Sleep(200)
        Console.WriteLine("                                       __/ |           /_/ ")
        Threading.Thread.Sleep(200)
        Console.WriteLine("                                      |___/                ")
        Threading.Thread.Sleep(1000)
        Console.ForegroundColor = ConsoleColor.White
        Environment.Exit(0)
    End Sub

End Module