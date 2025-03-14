using System;
using System.Media;

namespace DungeonExplorer
{
    // The main entry point of the game
    internal class Program
    {
        static void Main()
        {
            // Create a new game instance and start the game
            Game game = new Game();
            game.Start();
        }
    }

    // The Game class controls the overall game flow
    internal class Game
    {
        private Player player; // The player in the game
        private Room currentRoom; // The room the player is currently in

        // Constructor: Initializes the game by setting up the player and the first room
        public Game()
        {
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName); // Creating a player with the entered name
            currentRoom = new Room("Entrance Hall", "A dark, eerie entrance."); // Creating the starting room
        }


        // Starts the game loop
        public void Start()
        {
            bool playing = true; // Game is running until the player exits
            Console.WriteLine("Welcome to Dungeon Explorer!");
            Console.WriteLine("Type 'exit' to quit.");
            Console.WriteLine($"Hello, {player.Name}! You have {player.Health} health points. Let's begin!");

            while (playing && player.Health > 0)
            {
                // Display the current room details
                Console.WriteLine($"You are in {currentRoom.Name} - {currentRoom.Description}");
                Console.Write("Enter command: ");
                string input = Console.ReadLine().ToLower();

                // Handle user input
                if (input == "exit")
                {
                    playing = false; // Stop the game loop
                    Console.WriteLine("Thanks for playing!");
                }
                else if (input == "left")
                {
                    Console.WriteLine("You found a treasure chest! You win!");
                    playing = false;
                }
                else if (input == "right")
                {
                    Console.WriteLine("You fell into a pit! You lose 1 health.");
                    player.DecreaseHealth();
                }
                else if (input == "forward")
                {
                    Console.WriteLine("A monster appears! You escape but lose 1 health.");
                    player.DecreaseHealth();
                }
                else
                {
                    Console.WriteLine("Invalid command! You stumble and lose 1 health.");
                    player.DecreaseHealth();
                }

                // Check if the player still has health remaining
                if (player.Health > 0)
                {
                    Console.WriteLine($"{player.Name}, you now have {player.Health} health points remaining.");
                }
                else
                {
                    Console.WriteLine($"{player.Name}, you have lost all your health. Game Over!");
                    playing = false;
                }
            }
        }
    }

    // The Player class represents the player in the game
    internal class Player
    {
        public string Name { get; } // Player's name (read-only)
        public int Health { get; private set; } // Player's health (modifiable only within the class)

        // Constructor: Creates a player with a given name and initializes health
        public Player(string name)
        {
            Name = name;
            Health = 3;
        }

        // Decrease player's health
        public void DecreaseHealth()
        {
            Health--;
        }
    }

    // The Room class represents a room in the game
    internal class Room
    {
        public string Name { get; } // Room name (read-only)
        public string Description { get; } // Room description (read-only)

        // Constructor: Creates a room with a name and description
        public Room(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
            
