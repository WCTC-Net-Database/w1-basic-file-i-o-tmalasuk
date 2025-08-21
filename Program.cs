class Program
{
    static void Main()
    {
        Console.Write("Enter your character's name: ");
        string name = Console.ReadLine();

        Console.Write("Enter your character's class: ");
        string characterClass = Console.ReadLine();

        Console.Write("Enter your character's level: ");
        int level = int.Parse(Console.ReadLine());

        Console.Write("Enter your character's equipment (separate items with a '|'): ");
        string[] equipment = Console.ReadLine().Split('|');

        Console.WriteLine($"Welcome, {name} the {characterClass}! You are level {level} and your equipment includes: {string.Join(", ", equipment)}.");
    }
}