namespace MealAssistant
{
    public class Utils
    {
        public static void WaitForKeyPress()
        {
            // wait for user input to exit, any input exits in terminal, if debug mode must be enter
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