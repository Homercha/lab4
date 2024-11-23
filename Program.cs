using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TourismManagement
{
    // Основний клас для туристичних маршрутів
    [Serializable]
    [XmlInclude(typeof(Excursion))] // Включення підкласів для серіалізації
    [XmlInclude(typeof(Hiking))]
    [XmlInclude(typeof(Cruise))]
    [XmlInclude(typeof(Safari))]
    [XmlInclude(typeof(HorseRiding))]
    [XmlInclude(typeof(Cycling))]
    [XmlInclude(typeof(MotorcycleTour))]
    [XmlInclude(typeof(TrainTour))]
    [XmlInclude(typeof(AirTour))]
    public class Tour
    {
        // Назва маршруту
        public string Name { get; set; }
        // Тривалість маршруту
        public double Duration { get; set; }
        // Кількість зупинок
        public int Stops { get; set; }
        // Чи тривалість у годинах
        public bool IsInHours { get; set; }
        // Вартість маршруту
        public double Cost { get; set; }

        // Перевірка даних маршруту на валідність
        public void ValidateData()
        {
            if (Duration <= 0)
                throw new ArgumentException("Тривалість маршруту має бути більше 0.");
            if (Stops < 0)
                throw new ArgumentException("Кількість зупинок не може бути від'ємною.");
        }

        // Планування маршруту (віртуальний метод для перевизначення у підкласах)
        public virtual void Plan()
        {
            Console.WriteLine("Планування туристичного маршруту...");
        }

        // Розрахунок вартості за тривалістю
        public virtual double CalculateTripCost(double duration)
        {
            return duration * 100;
        }

        // Розрахунок вартості за тривалістю і кількістю зупинок
        public virtual double CalculateTripCost(double duration, int stops)
        {
            return duration * 100 + stops * 50;
        }

        // Розрахунок вартості у годинах
        public virtual double CalculateTripCostInHours(double hours)
        {
            return hours * 5; // Наприклад, 5 грн за годину
        }
    }

    // Клас для екскурсій
    [Serializable]
    public class Excursion : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування екскурсії: огляд визначних місць.");
        }
    }

    [Serializable]
    public class Hiking : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування пішого походу: складання маршруту з зупинками.");
        }
    }

    [Serializable]
    public class Cruise : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування круїзу: відвідування міст та розважальних заходів на борту.");
        }
    }

    [Serializable]
    public class Safari : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування сафарі: відвідування дикої природи та екзотичних місць.");
        }
    }

    [Serializable]
    public class HorseRiding : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування кінної подорожі: маршрути для вершників.");
        }
    }

    [Serializable]
    public class Cycling : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування велосипедного маршруту: оптимальний шлях для велосипедистів.");
        }
    }

    [Serializable]
    public class MotorcycleTour : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування мотоциклетного маршруту: швидкість та пригоди.");
        }
    }

    [Serializable]
    public class TrainTour : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування залізничної подорожі: маршрути на потягах.");
        }
    }

    [Serializable]
    public class AirTour : Tour
    {
        public override void Plan()
        {
            Console.WriteLine("Планування авіаційного маршруту: подорожі літаками.");
        }
    }

    // Головний клас програми
    class Program
    {
        private const string FilePath = "routes.xml";
        private static List<Tour> SavedTours;

        // Статичний конструктор для ініціалізації збережених маршрутів
        static Program()
        {
            SavedTours = LoadSavedTours();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Оберіть дію:");
                Console.WriteLine("1. Обрати маршрут");
                Console.WriteLine("2. Відтворити збережені маршрути");
                Console.WriteLine("3. Видалити маршрут");
                Console.WriteLine("4. Вихід");
                Console.Write("Ваш вибір: ");
                string mainChoice = Console.ReadLine();

                if (mainChoice == "1")
                {
                    HandleNewTour();
                }
                else if (mainChoice == "2")
                {
                    DisplaySavedTours();
                }
                else if (mainChoice == "3")
                {
                    DeleteSavedTour();
                }
                else if (mainChoice == "4")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                }
            }
        }

        static void HandleNewTour()
        {
            Console.WriteLine("Оберіть тип туристичного маршруту:");
            Console.WriteLine("1. Екскурсія");
            Console.WriteLine("2. Піший похід");
            Console.WriteLine("3. Круїз");
            Console.WriteLine("4. Сафари");
            Console.WriteLine("5. Кінний маршрут");
            Console.WriteLine("6. Велосипедний маршрут");
            Console.WriteLine("7. Мотоциклетний маршрут");
            Console.WriteLine("8. Залізнична подорож");
            Console.WriteLine("9. Авіаційний маршрут");
            Console.Write("Ваш вибір: ");
            string choice = Console.ReadLine();

            Tour selectedTour = choice switch
            {
                "1" => new Excursion { Name = "Екскурсія" },
                "2" => new Hiking { Name = "Піший похід" },
                "3" => new Cruise { Name = "Круїз" },
                "4" => new Safari { Name = "Сафари" },
                "5" => new HorseRiding { Name = "Кінний маршрут" },
                "6" => new Cycling { Name = "Велосипедний маршрут" },
                "7" => new MotorcycleTour { Name = "Мотоциклетний маршрут" },
                "8" => new TrainTour { Name = "Залізнична подорож" },
                "9" => new AirTour { Name = "Авіаційний маршрут" },
                _ => null
            };

            if (selectedTour == null)
            {
                Console.WriteLine("Невірний вибір.");
                return;
            }

            Console.WriteLine($"Ви обрали: {selectedTour.Name}");
            SetTourData(selectedTour);
        }

        static void SetTourData(Tour tour)
        {
            Console.WriteLine("Вкажіть, у яких одиницях задається тривалість:");
            Console.WriteLine("1. У годинах");
            Console.WriteLine("2. У днях");
            Console.Write("Ваш вибір: ");
            string unitChoice = Console.ReadLine();

            tour.IsInHours = unitChoice == "1";

            bool isValid = false;
            do
            {
                try
                {
                    Console.Write("Введіть тривалість маршруту (позитивне число): ");
                    tour.Duration = ParsePositiveDouble(Console.ReadLine());

                    Console.Write("Введіть кількість зупинок (позитивне число): ");
                    tour.Stops = ParsePositiveInt(Console.ReadLine());

                    Console.WriteLine("Оберіть метод розрахунку вартості:");
                    Console.WriteLine("1. На основі лише тривалості");
                    Console.WriteLine("2. На основі тривалості та зупинок");
                    Console.Write("Ваш вибір: ");
                    string costChoice = Console.ReadLine();

                    if (costChoice == "1")
                    {
                        tour.Cost = tour.IsInHours
                            ? tour.CalculateTripCostInHours(tour.Duration)
                            : tour.CalculateTripCost(tour.Duration);
                    }
                    else if (costChoice == "2")
                    {
                        tour.Cost = tour.CalculateTripCost(tour.Duration, tour.Stops);
                    }
                    else
                    {
                        Console.WriteLine("Невірний вибір методу розрахунку. Повторіть.");
                        continue;
                    }

                    isValid = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}. Спробуйте ще раз.");
                }
            } while (!isValid);

            tour.Plan();
            Console.WriteLine($"Загальна вартість маршруту: {tour.Cost} грн.");
            SaveTourPrompt(tour);
        }

        static void SaveTourPrompt(Tour tour)
        {
            Console.Write("Бажаєте зберегти маршрут? (y/n): ");
            string saveChoice = Console.ReadLine()?.ToLower();

            if (saveChoice == "y")
            {
                SavedTours.Add(tour);
                SaveToursToFile();
                Console.WriteLine("Маршрут збережено.");
            }
        }

        static void DisplaySavedTours()
        {
            if (!SavedTours.Any())
            {
                Console.WriteLine("Немає збережених маршрутів.");
                return;
            }

            Console.WriteLine("Список збережених маршрутів:");
            for (int i = 0; i < SavedTours.Count; i++)
            {
                var tour = SavedTours[i];
                Console.WriteLine($"{i + 1}. {tour.Name} - Тривалість: {(tour.IsInHours ? $"{tour.Duration} годин" : $"{tour.Duration} днів")}, Зупинки: {tour.Stops}, Вартість: {tour.Cost} грн.");
            }

            Console.Write("Введіть номер маршруту для перегляду або 0 для виходу: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index > 0 && index <= SavedTours.Count)
            {
                var selectedTour = SavedTours[index - 1];
                Console.WriteLine($"Маршрут: {selectedTour.Name}, Тривалість: {(selectedTour.IsInHours ? $"{selectedTour.Duration} годин" : $"{selectedTour.Duration} днів")}, Зупинки: {selectedTour.Stops}, Вартість: {selectedTour.Cost} грн.");
            }
            else
            {
                Console.WriteLine("Неправильний номер маршруту.");
            }
        }

        static void DeleteSavedTour()
        {
            if (!SavedTours.Any())
            {
                Console.WriteLine("Немає збережених маршрутів.");
                return;
            }

            Console.WriteLine("Список збережених маршрутів:");
            for (int i = 0; i < SavedTours.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {SavedTours[i].Name}");
            }

            Console.Write("Введіть номер маршруту для видалення: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int index) && index > 0 && index <= SavedTours.Count)
            {
                SavedTours.RemoveAt(index - 1);
                SaveToursToFile();
                Console.WriteLine("Маршрут видалено.");
            }
            else
            {
                Console.WriteLine("Неправильний номер маршруту.");
            }
        }

        static void SaveToursToFile()
        {
            try
            {
                using var writer = new StreamWriter(FilePath);
                var serializer = new XmlSerializer(typeof(List<Tour>));
                serializer.Serialize(writer, SavedTours);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження маршруту: {ex.Message}");
            }
        }

        static List<Tour> LoadSavedTours()
        {
            if (!File.Exists(FilePath))
                return new List<Tour>();

            try
            {
                using var reader = new StreamReader(FilePath);
                var serializer = new XmlSerializer(typeof(List<Tour>));
                return (List<Tour>)serializer.Deserialize(reader);
            }
            catch
            {
                return new List<Tour>();
            }
        }

        static double ParsePositiveDouble(string input)
        {
            if (double.TryParse(input, out double result) && result > 0)
                return result;

            throw new ArgumentException("Введіть дійсне позитивне число.");
        }

        static int ParsePositiveInt(string input)
        {
            if (int.TryParse(input, out int result) && result >= 0)
                return result;

            throw new ArgumentException("Введіть дійсне позитивне ціле число.");
        }
    }
}
