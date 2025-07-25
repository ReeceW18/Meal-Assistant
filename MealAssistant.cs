namespace MealAssistant
{
    public class MealAssistant
    {
        // variables
        public Dictionary<string, Meal> meals = [];

        // empty constructor
        public MealAssistant() { }

        // constructor from save file
        public MealAssistant(string file)
        {
            //load meals from file
            meals = SaveManager.LoadFrom(file);
        }

        // display top recommendations to console
        public void ShowRecommendations()
        {
            Console.WriteLine("Recommendations: ");

            foreach (MealType type in Enum.GetValues(typeof(MealType)))
            {
                var sortedMeals = GetSortedMealsWithType(type);

                if (sortedMeals.Count < 1)
                {
                    Console.WriteLine($"{type}: none found");
                    continue;
                }

                Console.WriteLine($"{type}: {sortedMeals[0].MealName}");
            }
        }

        public void Breakdown(string mealTypeString)
        {
            Console.WriteLine($"Recommendations for {mealTypeString} in descending order");
            MealType mealType;
            if (!Enum.TryParse(mealTypeString, ignoreCase: true, out mealType))
            {
                Console.WriteLine("invalid input");
                Utils.WaitForKeyPress();
                return;
            }
            List<Meal> sortedMeals = GetSortedMealsWithType(mealType);

            for (int i = 0; i < sortedMeals.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {sortedMeals[i].MealName}".PadRight(25) + $"Date: {sortedMeals[i].LastEaten.ToShortDateString()}");
            }
            Utils.WaitForKeyPress();
        }

        public void Add(string mealName = "")
        {
            int preference;
            DateTime lastEaten;
            MealType mealType;
            MealLocation mealLocation;

            preference = GetPreference(mealName);
            lastEaten = getLastEaten(mealName);
            mealType = getMealType(mealName);
            mealLocation = getMealLocation(mealName);

            meals.Add(mealName, new Meal(mealName, preference, lastEaten, mealType, mealLocation));
        }

        public void Modify(string mealName)
        {
            Console.WriteLine("Current properties: \n");
            Show(mealName);
            meals.Remove(mealName);
            Add(mealName);
        }

        public void ShowAll()
        {
            int nameColumnWidth = 20;
            int numberColumnWidth = 3;
            int columnWidth = 10;
            Console.Write("Meal name".PadRight(nameColumnWidth) + "| ");
            Console.Write("Sc".PadRight(numberColumnWidth) + "| ");
            Console.Write("Rn".PadRight(numberColumnWidth) + "| ");
            Console.Write("Date".PadRight(columnWidth) + "| ");
            Console.Write("Type".PadRight(columnWidth) + "| ");
            Console.WriteLine("Location".PadRight(columnWidth));
            Console.WriteLine();
            foreach (var meal in meals)
            {
                // implement better display
                Console.Write(meal.Key.PadRight(nameColumnWidth) + "| ");
                Console.Write($"{Math.Round(meal.Value.Score)}".PadRight(numberColumnWidth) + "| ");
                Console.Write($"{meal.Value.Preference}".PadRight(numberColumnWidth) + "| ");
                Console.Write(meal.Value.LastEaten.ToShortDateString().PadRight(columnWidth) + "| ");
                Console.Write($"{meal.Value.MealType}".PadRight(columnWidth) + "| ");
                Console.WriteLine($"{meal.Value.MealLocation}".PadRight(columnWidth));
            }

            Utils.WaitForKeyPress();
        }

        public void Show(string mealName)
        {
            Meal meal = meals[mealName];
            Console.WriteLine(meal.MealName);
            Console.WriteLine($"Score: {Math.Round(meal.Score, 2)}");
            Console.WriteLine($"Preference: {meal.Preference}");
            Console.WriteLine($"Last eaten: {meal.LastEaten.ToShortDateString()}");
            Console.WriteLine($"Meal Time: {meal.MealType}");
            Console.WriteLine($"Meal Location: {meal.MealLocation}\n");
            Utils.WaitForKeyPress();

        }
/*
                private static double GetScore(Meal meal)
                {
                    double score = 0;

                    int preference = meal.Preference;
                    double daysSinceEaten = (DateTime.Now - meal.LastEaten).TotalDays;

                    if (daysSinceEaten < 1) daysSinceEaten = 1;

                    score = preference * Math.Log(daysSinceEaten);

                    return score;
                }

                private static void UpdateScore(Meal meal)
                {
                    meal.Score = GetScore(meal);
                }

                */
        private MealLocation getMealLocation(string mealName)
        {
            int userInput;

            Console.WriteLine($"{mealName} location?");
            foreach (MealLocation location in Enum.GetValues(typeof(MealLocation)))
            {
                Console.WriteLine($"{(int)location}: {location}");
            }
            Console.WriteLine("Enter number of choice: ");

            while (!(int.TryParse(Console.ReadLine(), out userInput) && Enum.IsDefined(typeof(MealLocation), userInput)))
            {
                Console.WriteLine("Please enter a valid integer: ");
            }

            return (MealLocation)userInput;
        }

        private MealType getMealType(string mealName)
        {
            int userInput;

            Console.WriteLine($"{mealName} type?");
            foreach (MealType type in Enum.GetValues(typeof(MealType)))
            {
                Console.WriteLine($"{(int)type}: {type}");
            }
            Console.WriteLine("Enter number of choice: ");

            while (!(int.TryParse(Console.ReadLine(), out userInput) && Enum.IsDefined(typeof(MealType), userInput)))
            {
                Console.WriteLine("Please enter a valid integer: ");
            }

            return (MealType)userInput;
 
        }

        private DateTime getLastEaten(string mealName)
        {
            Console.WriteLine($"Date {mealName} last eaten, format either \"mm/dd\" or \"today\": ");
            string? input = Console.ReadLine();

            DateTime returnDate;

            while (!DateTime.TryParseExact(input, "MM/dd", null, System.Globalization.DateTimeStyles.None, out returnDate) && input != "today")
            {
                Console.WriteLine("Invalid format, try again (format either \"mm/dd\" or \"today\"): ");
                input = Console.ReadLine();
            }

            if (returnDate == DateTime.MinValue) returnDate = DateTime.Today;

            return returnDate;
        }

        private int GetPreference(string mealName)
        {
            Console.WriteLine($"{mealName} preference ranking 1-10 (10 being best): ");

            int input;

            while (!int.TryParse(Console.ReadLine(), out input) || input < 1 || input > 10)
            {
                Console.WriteLine("Please enter a value between 1 and 10");
            }

            return input;
        }
        private List<Meal> GetSortedMealsWithType(MealType type)
        {
            var filteredMeals = meals.Values
                .Where(meal => meal.MealType == type)
                .OrderByDescending(meal => meal.Score)
                .ToList();

            return filteredMeals;
        }
        public void Update(string input)
        {
            /*
            string? userDate = "";
            Console.WriteLine("Please enter date with proper format (mm/dd): ");
            userDate = Console.ReadLine();
            DateTime date = DateTime.Today;
            while (!DateTime.TryParseExact(userDate, "MM/dd", null, System.Globalization.DateTimeStyles.None, out date))
            {
                Console.WriteLine("Please enter date with proper format (mm/dd): ");
                userDate = Console.ReadLine();
            }
            */
            DateTime newDate = getLastEaten(input);

            try
            {
                meals[input].UpdateDate(newDate);
            }
            catch (KeyNotFoundException)
            {
                Console.WriteLine($"{input} does not exist, try add {input} to create it");
                Utils.WaitForKeyPress();
            }

        }
    }
}