
namespace MealAssistant
{
    public class Meal
    {
        // implement
        public Meal(string mealName, int preference, DateTime lastEaten, MealType mealType, MealLocation mealLocation)
        {
            MealName = mealName;
            _preference = preference;
            _lastEaten = lastEaten;
            MealType = mealType;
            MealLocation = mealLocation;
            UpdateScore();
        }

        public string MealName { get; set; }
        private int _preference;
        public int Preference
        {
            get { return _preference; }
            set
            {
                _preference = value;
                UpdateScore();
            }
        }
        private DateTime _lastEaten;
        public DateTime LastEaten
        {
            get { return _lastEaten; }
            set
            {
                _lastEaten = value;
                UpdateScore();
            }
        }
        public MealType MealType { get; set; }
        public MealLocation MealLocation { get; set; }
        public double Score { get; set; }

        public void UpdateScore()
        {
            double daysSinceEaten = (DateTime.Now - LastEaten).TotalDays;

            if (daysSinceEaten < 3)
            {
                daysSinceEaten = 0;
            }

            Score = Preference * Math.Log(daysSinceEaten + 1);
        }

        public void UpdateDate(DateTime? date = null)
        {
            DateTime trueDate = date ?? DateTime.Today;
            LastEaten = trueDate;
        }
    }
}