// Meal Assistant CLI

namespace MealAssistant
{
    class Program
    {
        static void Main()
        {
            // set up variables
            string currentMessage = "Welcome!";
            string appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string directory = Path.Combine(appDataDir, "MealAssistant");
            Directory.CreateDirectory(directory);
            string saveFile = Path.Combine(directory, "saveData.json");
            MealAssistant mealAssistant;
            bool isRunning = true;

            // initialize mealAssistant, either from save if exists or empty if no save
            if (File.Exists(saveFile))
            {
                mealAssistant = new MealAssistant(saveFile);
            }
            else
            {
                mealAssistant = new MealAssistant();
            }

            // main loop
            while (isRunning)
            {
                Console.Clear();

                Console.WriteLine(currentMessage + "\n");

                mealAssistant.ShowRecommendations();
                Console.WriteLine();

                Console.WriteLine("Enter a command (hint: type help to see commands)");
                Console.Write("> ");

                string? inputRaw = Console.ReadLine();
                string input = inputRaw != null ? inputRaw.Trim().ToLower() : string.Empty;
                string[] command = input.Split();

                bool isValidCommand = CheckValid(command);
                if (!isValidCommand)
                {
                    command = [ "invalid" ];
                }

                Console.Clear();
                // run command and update current message
                switch (command[0])
                {
                    //implement commands
                    case "help":
                        showHelp();
                        currentMessage = "Exited help menu";
                        break;
                    case "add":
                        mealAssistant.Add(input.Replace("add ", ""));
                        currentMessage = "added " + input.Replace("add ", "");
                        break;
                    case "update":
                        mealAssistant.Update(input.Replace("update ", ""));
                        currentMessage = "updated " + input.Replace("update ", "");
                        
                        break;
                    case "modify":
                        currentMessage = command[0] + " not yet implemented";
                        mealAssistant.Modify(input.Replace("modify ", ""));
                        break;
                    case "info":
                        currentMessage = input.Replace("info ", "") + "info shown";
                        mealAssistant.Show(input.Replace("info ", ""));
                        break;
                    case "quit":
                        SaveManager.SaveTo(saveFile, mealAssistant.meals);
                        isRunning = false;
                        break;
                    case "showall":
                        mealAssistant.ShowAll();
                        currentMessage = "Information shown";
                        break;
                    case "save":
                        SaveManager.SaveTo(saveFile, mealAssistant.meals);
                        currentMessage = "Saved to file";
                        break;
                    case "breakdown":
                        mealAssistant.Breakdown(input.Replace("breakdown ", ""));
                        break;
                    default:
                        currentMessage = "Invalid command";
                        break;

                }
            }
        }


        // returns true if command is a valid single command or has multiple elements
        static bool CheckValid(string[] command)
        {
            // list of valid single commands
            string[] singleCommands = ["help", "quit", "showall", "save"];

            // check if is single command
            foreach (var validSingleCommand in singleCommands)
            {
                if (command[0] == validSingleCommand) {
                    return true;
                }
            }

            // check if has multiple elements, return false if it doesn't
            if (command.Length < 2)
            {
                return false;
            }

            // return true if not a single command but has multiple elements
            return true;
       }

        // help menu that displays commands
        static void showHelp()
        {
            // list of commands with explanations
            string[] commands = {
                "quit - save data and terminate program",
                "save - save data",
                "add mealName - add a new meal with name mealName",
                "update mealName - update the last ate date for mealName",
                "showall - display all stored meals",
                "modify mealName - change properties of mealName",
                "breakdown mealType - get a comprehensive review of recommendations for mealType",
                "   mealType Options: Breakfast, Lunch, Snack, Dinner",
                "info mealName - get all properties of mealName" };

            // formatting
            Console.WriteLine("HELP MENU\nCommands:");

            // write each command
            foreach (var command in commands)
            {
                Console.WriteLine("   " + command);
            }

            Utils.WaitForKeyPress();

        }

   }
}