using System.Dynamic;
using System.Runtime.CompilerServices;

namespace ToDo
{
    internal class Program
    {
        private static readonly string todoFilePath = "ToDo.txt";

        public static object ShowTask { get; private set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Min Att Göra Lista");

            while (true)
            {
                Console.WriteLine("\n Välj ett alternative");
                Console.WriteLine("1 Lägg till ny uppgift");
                Console.WriteLine("2. visa alla uppgifter");
                Console.WriteLine("3. Avsluta");
                Console.Write("Ditt Val: ");

                MenuChoice choice = GetMenuChoice();
                switch (choice)
                {
                    case MenuChoice.AddTask:
                        AddTask();
                        break;
                    case MenuChoice.ShowTask:
                        ShowTasks();
                        break;
                    case MenuChoice.Exit:
                        Console.WriteLine("Tack för att du använde att göra");
                        return;

                    default:
                        Console.WriteLine("OGILTIGT VAL, försök igen");
                        break;
                }
            }
        }

       
        private static void ShowTasks()
        {
            try
            {
                if(!File.Exists(todoFilePath))
                {
                    Console.WriteLine("Inga uppgifter Hittades ");
                    return ;
                }
                string[] tasks = File.ReadAllLines(todoFilePath);
                if (tasks.Length == 0)
                {

                    Console.WriteLine("inga uppgifter i Listan");
                    return;
                }
                Console.WriteLine("Dina Uppgifter");
                for (int i = 0; i < tasks.Length; i++)
                {
                    Console.WriteLine($"{i + 1} {tasks[i]}");
                }
                Console.WriteLine($"\n Total: {tasks.Length} uppgifter");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Fel vid inläsning av uppgifter"); Console.WriteLine(ex.Message);
            }
        }

        private static void AddTask()
        {
            Console.WriteLine("Skriv in din nya uppgift");
            string? task = Console.ReadLine();
            if (string.IsNullOrEmpty(task))
            {
                Console.WriteLine("uppgiften kan inte vara tom!");
                return;
            }
            try
            {
                //Lägg till uppgiften med timestamp
                string taskWithTimestamp = $"[{DateTime.Now:yyyy-mm-dd HH:mm:ss}]{ task}";
                  File.AppendAllText(todoFilePath, taskWithTimestamp + Environment.NewLine);
                Console.WriteLine("Uppgift tillagd");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid sparade av uppgiften: {ex.Message}");
            }
        }

        private static MenuChoice GetMenuChoice() {
            string? input = Console.ReadLine();
            if (input != null && int.TryParse(input, out int choice)
                && Enum.IsDefined(typeof(MenuChoice), choice))
            {
                return (MenuChoice)choice;
            }
            return MenuChoice.Invalid;
}        }         
    }
