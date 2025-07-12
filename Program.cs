// See https://aka.ms/new-console-template for more information

namespace MealAssistant
{
    class Program
    {
        static void Main()
        {
            bool running = true;
            string currentMessage = "Welcome!";
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string directory = Path.Combine(appDataDir, "MealAssistant");
            Directory.CreateDirectory(directory);
            string saveFile = Path.Combine(directory, "saveData.json");
            MealAssistant mealAssistant;

            if (File.Exists(saveFile))
            {
                mealAssistant = new MealAssistant(saveFile);
            }
            else
            {
                mealAssistant = new MealAssistant();
            }

            while (running)
            {
                Console.Clear();

                Console.WriteLine(currentMessage + "\n");

                ShowRecommendations();

                Console.WriteLine();

                Console.WriteLine("Enter a command (hint: type help to see commands)");
                Console.Write("> ");
                string? inputRaw = Console.ReadLine();
                string input = inputRaw != null ? inputRaw.Trim().ToLower() : string.Empty;

                Console.Clear();
                switch (input)
                {
                    //implement commands
                    case "help":
                        showHelp();
                        currentMessage = "Exited help menu";
                        break;
                    default:
                        currentMessage = "Invalid command";
                        break;

                }
            }
        }

        static void ShowRecommendations()
        {
            //implement
            Console.WriteLine("Recommendation display not implemented");
        }

        static void showHelp()
        {
            string[] commands = {
                "command1 - example",
                "command2 - example2" };

            Console.WriteLine("HELP MENU\nCommands:");

            foreach (var command in commands)
            {
                Console.WriteLine("     " + command);
            }

            try
            {
                Console.WriteLine("Press any key to return to main menu"); 
                Console.ReadKey(true);
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("DEBUG MODE OR OTHERWISE NO CONSOLE. Press Enter to return to main menu");
                Console.ReadLine();
            }

        }

    }
}