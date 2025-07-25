using System.Text.Json;

namespace MealAssistant
{
    public class SaveManager
    {
        public static void SaveTo(string saveLocation, Dictionary<string, Meal> meals)
        {
            string json = JsonSerializer.Serialize(meals);

            BackUp(saveLocation);

            File.WriteAllText(saveLocation, json);
        }

        public static Dictionary<string, Meal> LoadFrom(string saveLocation)
        {
            string json = File.ReadAllText(saveLocation);

            Dictionary<string, Meal>? meals = JsonSerializer.Deserialize<Dictionary<string, Meal>>(json);

            if (meals != null)
            {
                return meals;
            }

            return [];

        }

        private static void BackUp(string saveLocation)
        {
            if (!Path.Exists(saveLocation))
            {
                return;
            }

            string newSaveDirectory = saveLocation + "_backups/";

            Directory.CreateDirectory(newSaveDirectory);

            File.Copy(saveLocation, newSaveDirectory + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".json");
        }
    }
}