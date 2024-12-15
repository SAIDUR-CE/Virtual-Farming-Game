using System;
using System.Collections.Generic;

// Interface defining the Produce method
public interface IProduce
{
    void Produce();
}

// Abstract base class for farm entities (animals, crops, etc.)
public abstract class FarmEntity
{
    public string Name { get; set; }

    // Abstract method to produce items
    public abstract void Produce();

    // Abstract method to display info about the entity
    public abstract void DisplayInfo();
}

// Abstract class for animals (inherits FarmEntity and implements IProduce)
public abstract class Animal : FarmEntity, IProduce
{
    public int Age { get; set; }

    // Constructor to initialize animal properties
    public Animal(string name, int age)
    {
        Name = name;
        Age = age;
    }

    // Overriding the Produce method from IProduce 
    public override void Produce()
    {
        Console.WriteLine($"{Name} is producing goods.");
    }

    // Overriding DisplayInfo method
    public override void DisplayInfo()
    {
        Console.WriteLine($"{Name}, Age: {Age}");
    }
}

// Concrete class for Cow
public class Cow : Animal
{
    public Cow(string name, int age) : base(name, age) { }

    // Overriding Produce for Cow
    public override void Produce()
    {
        base.Produce();
        Console.WriteLine($"{Name} produced milk, beef, and leather.");
    }
}

// Concrete class for Chicken
public class Chicken : Animal
{
    public Chicken(string name, int age) : base(name, age) { }

    // Overriding Produce for Chicken
    public override void Produce()
    {
        base.Produce();
        Console.WriteLine($"{Name} laid eggs.");
    }
}

// Concrete class for Sheep
public class Sheep : Animal
{
    public Sheep(string name, int age) : base(name, age) { }

    // Overriding Produce for Sheep
    public override void Produce()
    {
        base.Produce();
        Console.WriteLine($"{Name} produced mutton and wool.");
    }
}

// Class representing an item (e.g., tool)
public class Tool
{
    public string ToolName { get; set; }

    public Tool(string toolName)
    {
        ToolName = toolName;
    }

    // Overloading the + operator to add tool to inventory
    public static Tool operator +(Tool tool1, Tool tool2)
    {
        Console.WriteLine($"Combining tools: {tool1.ToolName} and {tool2.ToolName}");
        return tool1; // Just returns the first tool for simplicity
    }
}

// Inventory class to manage items
public class Inventory
{
    public List<Tool> Tools { get; set; } = new List<Tool>();

    // Method to add tool to inventory (Overloaded)
    public void AddTool(Tool tool)
    {
        if (Tools.Count < 10)
        {
            Tools.Add(tool);
            Console.WriteLine($"{tool.ToolName} added to inventory.");
        }
        else
        {
            Console.WriteLine("Inventory full! Cannot add more tools.");
        }
    }

    // Overloading AddTool to accept two tools at once
    public void AddTool(Tool tool1, Tool tool2)
    {
        if (Tools.Count + 2 <= 10)
        {
            Tools.Add(tool1);
            Tools.Add(tool2);
            Console.WriteLine($"Both {tool1.ToolName} and {tool2.ToolName} added to inventory.");
        }
        else
        {
            Console.WriteLine("Not enough space to add both tools.");
        }
    }

    public void DisplayInventory()
    {
        Console.WriteLine("Inventory contains:");
        foreach (var tool in Tools)
        {
            Console.WriteLine(tool.ToolName);
        }
    }
}

// Player class
public class Player
{
    public string Name { get; set; }
    public Inventory PlayerInventory { get; private set; }
    public List<FarmEntity> FarmEntities { get; set; } = new List<FarmEntity>();

    // Static field to count number of animals created
    public static int AnimalCount = 0;

    // Constructor for Player
    public Player(string name)
    {
        Name = name;
        PlayerInventory = new Inventory();
    }

    // Copy constructor (creates a new player with same data)
    public Player(Player existingPlayer)
    {
        Name = existingPlayer.Name;
        PlayerInventory = new Inventory();
        Console.WriteLine($"Created new player by copying {existingPlayer.Name}");
    }

    // Static method to track total animals
    public static void TrackAnimal()
    {
        AnimalCount++;
        Console.WriteLine($"Total animals on the farm: {AnimalCount}");
    }

    // Method to add entity to the farm (e.g., animals, crops)
    public void AddFarmEntity(FarmEntity entity)
    {
        FarmEntities.Add(entity);
        Console.WriteLine($"{entity.Name} has been added to the farm.");
        if (entity is Animal) TrackAnimal();  // Increase animal count
    }

    // Method to interact with farm entities (e.g., animals produce goods)
    public void Harvest()
    {
        Console.WriteLine($"{Name} is harvesting...");
        foreach (var entity in FarmEntities)
        {
            entity.Produce();
        }
    }
}

// Main program to run the game
public class Program
{
    public static void Main(string[] args)
    {
        // Create player
        Player player = new Player("Farmer John");

        // Create and add animals and crop to the farm
        Cow cow = new Cow("Daisy", 3);
        Chicken chicken = new Chicken("Cluck", 2);
        Sheep sheep = new Sheep("Wooly", 4);
        
        player.AddFarmEntity(cow);
        player.AddFarmEntity(chicken);
        player.AddFarmEntity(sheep);

        // Create tools and add to inventory
        Tool wateringCan = new Tool("Watering Can");
        Tool shovel = new Tool("Shovel");

        player.PlayerInventory.AddTool(wateringCan);
        player.PlayerInventory.AddTool(shovel);

        // Create a copy of the player
        Player newPlayer = new Player(player);

        // Display player info and farm entities
        Console.WriteLine($"Welcome {player.Name} to your farm!");
        player.PlayerInventory.DisplayInventory();
        player.Harvest();

        // Display farm entity details
        foreach (var entity in player.FarmEntities)
        {
            entity.DisplayInfo();
        }

        // Use operator overloading with tools
        Tool combinedTool = wateringCan + shovel;
    }
}