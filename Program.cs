
    internal class Program
    {
        static void Main(string[] args)
        {
            var masterList = initializeCharList();
            var exit = false;
            while (!exit)
            {

                Console.WriteLine("1. Display Characters\n2. Add Character\n3. Level Up Character\n4. Exit");
                Console.Write("> ");
                var userInput = Console.ReadLine();

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
            rewriteFile(masterList);
            var lines = File.ReadAllLines("input.csv");


            foreach (var line in lines)
            {
                var column = line.Split(",");
                var name = column[0];
                var profession = column[1];
                var level = column[2];
                var hp = column[3];
                var equipment = column[4];

                Console.WriteLine($"Name: {name}");
                Console.WriteLine($"Class: {profession}");
                Console.WriteLine($"Level: {level}");
                Console.WriteLine($"HP: {hp}");
                var equip = equipment.Split("|");
                Console.Write("Equipment: ");
                foreach (var item in equip)
                {
                    Console.Write($"{item} ");

                }

                Console.WriteLine();
                Console.WriteLine("-----------");

            }
        }

        //choice 2
        static void AddCharacter(List<Character> masterList)
        {
            Console.Write("Enter character name: ");
            var name = Console.ReadLine();

            Console.Write("Enter character profession: ");
            var profession = Console.ReadLine();

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
                        Character selectedCharObj = character;
                        selectedCharObj.Level++;
                        rewriteFile(masterList);
                        Console.WriteLine($"{desiredCharName} has been leveled up! Current level: {selectedCharObj.Level}");
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
            var lines = File.ReadAllLines("input.csv");

            var startingCharacters = new List<Character>();


            foreach (var line in lines)
            {
                var column = line.Split(",");
                var name = column[0];
                var profession = column[1];
                var level = column[2];
                var hp = column[3];
                var equipment = column[4];
                var equip = equipment.Split("|");

                var thisCharacter = new Character(name, profession, int.Parse(level), int.Parse(hp), equip.ToList());

                startingCharacters.Add(thisCharacter);


            }

            return startingCharacters;
        }

        static void rewriteFile(List<Character> masterList)
        {

            File.WriteAllText("input.csv", string.Empty);

            using (StreamWriter writer = new StreamWriter("input.csv", append: false))
            {
                foreach (Character character in masterList)
                {
                    var equipmentString = string.Join("|", character.Equipment);
                    var line =
                        $"{character.Name},{character.Profession},{character.Level},{character.HP},{equipmentString}";
                    writer.WriteLine(line);
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

        public Character(string name, string profession, int level, int hp, List<string> equipment)
        {
            this.Name = name;
            this.Profession = profession;
            this.Level = level;
            this.HP = hp;
            this.Equipment = equipment;
        }
    }






