using System;
using System.Collections.Generic;

namespace DungeonExplorer 
// Main entry point of the game
{
    internal class Program
    {
        static void Main()
        {
            Game game = new Game(); // Create a new game instance
            game.Start();
        }
    }

    internal class Game
    {
        private Player player;
        private List<Room> rooms;
        private int currentRoomIndex;

        public Game()
        {
            Console.Write("Enter your name: ");
            string playerName = Console.ReadLine();
            player = new Player(playerName); // Create a player with the given name

          // Initialize rooms with their names, descriptions, items, and monsters
            rooms = new List<Room>
            {
                new Room("Dark Cave", "A damp cave with eerie sounds.", "Sword", "Goblin"),
                new Room("Ancient Library", "A dusty library filled with old books.", "Magic Scroll", "Ghost"),
                new Room("Treasure Chamber", "A golden chamber filled with riches.", "Golden Key", null)
            };

            currentRoomIndex = 0;
        }

        public void Start()
        {
            Console.WriteLine($"Welcome, {player.Name}! You have {player.Health} health points."); //Greets the player
            Console.WriteLine("Type 'explore' to enter a room, 'pickup' to take an item, 'inventory' to check items, or 'exit' to quit."); //Gives instruction on the player should play the game

            while (player.Health > 0 && currentRoomIndex < rooms.Count)
            {
                Room currentRoom = rooms[currentRoomIndex]; // Display the room's description
                Console.WriteLine(currentRoom.GetDescription());
                Console.Write("Enter command: ");
                string input = Console.ReadLine().ToLower();

                if (input == "exit") // Quit the game
                {
                    Console.WriteLine("Thanks for playing!");
                    break;
                }
                else if (input == "explore") // Starts the game
                {
                    if (currentRoom.Monster != null) // a moster attacks you
                    {
                        Console.WriteLine($"A {currentRoom.Monster} attacks you!");
                        player.TakeDamage(20);
                        if (player.Health <= 0)
                        {
                            Console.WriteLine("You have been defeated! Game Over."); // Game over
                            break;
                        }
                    }

                    if (currentRoomIndex < rooms.Count - 1)
                    {
                        Console.WriteLine("You move deeper into the dungeon...");
                        currentRoomIndex++;
                    }
                    else
                    {
                        Console.WriteLine("This is the last room. You cannot explore further.");
                    }
                }
                else if (input == "pickup")
                {
                    if (currentRoom.Item != null)
                    {
                        player.PickUpItem(currentRoom.Item);
                        currentRoom.Item = null; // Item is removed after pickup
                    }
                    else
                    {
                        Console.WriteLine("There is nothing to pick up here.");
                    }
                }
                else if (input == "inventory")
                {
                    player.ShowInventory();
                }
                else
                {
                    Console.WriteLine("Invalid command. Try again.");
                }
            }

            if (player.Health > 0 && currentRoomIndex >= rooms.Count)
            {
                Console.WriteLine("Congratulations! You have explored all rooms and survived the dungeon."); // You won the game
            }
        }
    }

    internal class Player
    {
        public string Name { get; }
        public int Health { get; private set; }
        private List<string> inventory;

        public Player(string name)
        {
            Name = name;
            Health = 100; // Player starts with 100 health points
            inventory = new List<string>();
        }

        public void TakeDamage(int damage)
        {
            Health -= damage; // Reduces your health
            Console.WriteLine($"You lost {damage} health points. Remaining health: {Health}");
        }

        public void PickUpItem(string item)
        {
            inventory.Add(item); // Add item to your inventory
            Console.WriteLine($"You picked up: {item}"); // You picked an item
        }

        public void ShowInventory()
        {
            if (inventory.Count == 0)
            {
                Console.WriteLine("Your inventory is empty.");
            }
            else
            {
                Console.WriteLine("Inventory: " + string.Join(", ", inventory));
            }
        }
    }

    internal class Room
    {
        public string Name { get; } // Room name
        public string Description { get; } // Room descriptions
        public string Item { get; set; } // Item present in a room (if any)
        public string Monster { get; } // Monster present in a room (if any)

        public Room(string name, string description, string item, string monster)
        {
            Name = name;
            Description = description;
            Item = item;
            Monster = monster;
        }

        public string GetDescription()
        {
            return $"You have entered {Name}. {Description} {(Item != null ? "You see a " + Item + " here." : "")} {(Monster != null ? "A " + Monster + " lurks nearby!" : "")}";
        }
    }
}
