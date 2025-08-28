using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using CsvHelper.TypeConversion;

internal class Program
{
    static void Main(string[] args)
    {
        //returns an initial list of Character objects from the input.csv file
        var masterList = initializeCharList();
        Console.WriteLine($"Welcome! {masterList.Count} characters have been loaded from save file.");

        var exit = false;
        while (!exit)
        {
            Console.WriteLine("1. Display Characters\n2. Add Character\n3. Level Up Character\n4. Exit");
            Console.Write("> ");
            var userInput = Console.ReadLine()?.Trim();

            switch (userInput)
            {
                case "1":
                    DisplayCharacters(masterList);
                    break;

                case "2":
                    AddCharacter(masterList);
                    break;

                case "3":
                    LevelUpCharacter(masterList);
                    break;

                case "4":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid input");
                    break;
            }
        }


    }

    //choice 1
    static void DisplayCharacters(List<Character> masterList)
    {
        // Keep file in sync and ensure latest information is available to display
        rewriteFile(masterList);

        using (var reader = new StreamReader("input.csv"))
        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
               {
                   HasHeaderRecord = false
               }))
        {
            csv.Context.RegisterClassMap<CharacterMap>();
            var characters = csv.GetRecords<Character>().ToList();

            foreach (var character in characters)
            {
                Console.WriteLine($"Name: {character.Name}");
                Console.WriteLine($"Class: {character.Profession}");
                Console.WriteLine($"Level: {character.Level}");
                Console.WriteLine($"HP: {character.HP}");
                Console.WriteLine($"Equipment: {string.Join(", ", character.Equipment)}");
                Console.WriteLine("+---------------------+");
            }
        }
        //spacing
        Console.WriteLine();
    }


    //choice 2
    // added trimming to prevent unexpected "" entered by CSVhelper
    static void AddCharacter(List<Character> masterList)
    {
        Console.Write("Enter character name: ");
        var name = Console.ReadLine()?.Trim();

        Console.Write("Enter character profession: ");
        var profession = Console.ReadLine()?.Trim();

        Console.Write("Enter character level: ");
        var levelInput = Console.ReadLine();
        int level;
        while (!int.TryParse(levelInput, out level) || level < 1)
        {
            Console.Write("Invalid input. Please enter a positive integer for level: ");
            levelInput = Console.ReadLine();
        }

        Console.Write("Enter character HP: ");
        var hpInput = Console.ReadLine();
        int hp;
        while (!int.TryParse(hpInput, out hp) || hp < 1)
        {
            Console.Write("Invalid input. Please enter a positive integer for HP: ");
            hpInput = Console.ReadLine();
        }

        Console.Write("Enter character equipment (separate items with '|'): ");
        var equipmentInput = Console.ReadLine();
        var equipment = equipmentInput.Split('|').Select(item => item.Trim()).ToList();

        var newCharacter = new Character(name, profession, level, hp, equipment);
        masterList.Add(newCharacter);
        rewriteFile(masterList);
        Console.WriteLine($"Character {name} added successfully!");
        Console.WriteLine();
    }

    //choice 3
    static void LevelUpCharacter(List<Character> masterList)
    {
        var validNames = masterList.Select(c => c.Name).ToList();
        Console.Write("Enter the name of the character you want to level up: ");
        var desiredCharName = Console.ReadLine();

        if (validNames.Contains(desiredCharName))
        {
            Console.WriteLine($"Leveling up {desiredCharName}...");

            foreach (Character character in masterList)
            {
                if (character.Name == desiredCharName)
                {
                    character.Level++;
                    rewriteFile(masterList);
                    Console.WriteLine($"{desiredCharName} has been leveled up! Current level: {character.Level}");
                    Console.WriteLine();
                }
            }

        }
        else
        {
            Console.WriteLine($"Character {desiredCharName} not found. Returning to main menu.");
        }
    }

    //used only once to gather starting information from the input.csv file
    static List<Character> initializeCharList()
    {
        using (var reader = new StreamReader("input.csv"))
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false // No headers in the file
            };

            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<CharacterMap>();
                return csv.GetRecords<Character>().ToList();
            }
        }

    }

    static void rewriteFile(List<Character> masterList)
    {
        using (var writer = new StreamWriter("input.csv"))
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false // No headers in the file
            };

            using (var csv = new CsvWriter(writer, config))
            {
                csv.Context.RegisterClassMap<CharacterMap>();
                csv.WriteRecords(masterList);
            }
        }
    }

}


class Character
{
    public string Name { get; set; }
    public string Profession { get; set; }
    public int Level { get; set; }
    public int HP { get; set; }
    public List<string> Equipment { get; set; }

    public Character() { }

    public Character(string name, string profession, int level, int hp, List<string> equipment)
    {
        Name = name;
        Profession = profession;
        Level = level;
        HP = hp;
        Equipment = equipment;
    }
}



class CharacterMap : ClassMap<Character>
{
    public CharacterMap()
    {
        Map(m => m.Name).Index(0);
        Map(m => m.Profession).Index(1);
        Map(m => m.Level).Index(2);
        Map(m => m.HP).Index(3);
        Map(m => m.Equipment).Index(4).TypeConverter<EquipmentConverter>();
    }
}


class EquipmentConverter : DefaultTypeConverter
{
    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
    {
        var list = value as List<string>;
        return list != null ? string.Join("|", list) : string.Empty;
    }

    public override object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
    {
        return text.Split('|', StringSplitOptions.RemoveEmptyEntries)
            .Select(s => s.Trim())
            .ToList();
    }
}





