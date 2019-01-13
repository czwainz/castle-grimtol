using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; private set; }

    public void GetUserInput()
    {

      Console.Write("What would you like to do? ");
      string userInput = Console.ReadLine().ToLower();
      string[] inputArr = userInput.Split(" ");
      string command = inputArr[0];

      string value;

      switch (command)
      {
        case "look":
          Look();
          break;
        case "inventory":
          Inventory();
          break;
        case "reset":
          Reset();
          break;
        case "quit":
          Quit();
          break;
        case "go":
          if (inputArr.Length > 1)
          {
            value = inputArr[1];
            Go(value);
          }
          else
          {
            Console.WriteLine("Not a valid direction, mate");
          }
          break;
        case "take":
          if (inputArr.Length > 1)
          {
            value = inputArr[1];
            TakeItem(value);
          }
          else
          {
            Console.WriteLine("Nothing to take, mate");
          }
          break;
        case "use":
          if (inputArr.Length > 1)
          {
            value = inputArr[1];
            UseItem(value);
          }
          else
          {
            Console.WriteLine("Nothing to use, mate");
          }
          break;
        case "help":
          Help();
          break;

      }

    }

    public void Go(string direction)
    {
      Console.Clear();
      Console.WriteLine($"You're trying to go {direction}");
      if (CurrentRoom.Name == "At the top of A Mountain" && !CurrentRoom.Exits.ContainsKey(direction))
      {
        Console.WriteLine("Ruh roh! You fell off the mountain and died! Learn your wind directions!");
        Quit();
      }
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
        if (CurrentRoom.Name == "At the top of A Mountain" && CurrentPlayer.Inventory.Count > 0)
        {
          Item boat = CurrentPlayer.Inventory.Find(i =>
          {
            return i.Name.ToLower() == "boat";
          });
          CurrentPlayer.Inventory.Remove(boat);
          CurrentRoom.Exits["west"].Exits["north"].Items.Add(boat);
          Console.WriteLine("Hiking back up, you dropped your boat!");
        }
      }
      else
      {
        Console.WriteLine("OOP! You can't go that way");
      }
      Look();
    }

    public void Help()
    {
      Console.Clear();
      Console.WriteLine(@"Here are you options! 
       go - choose a direction to move
       take - any items that are in the room 
       use - any items in your inventory 
       look - check what room you're in  
       inventory - check your inventory
       quit - allows you to exit the game
       reset - sends you back to the top of the mountain");
      GetUserInput();
    }

    public void Inventory()
    {
      if (CurrentPlayer.Inventory.Count == 0)
      {
        Console.WriteLine("You don't have any items");
      }
      else
      {
        CurrentPlayer.Inventory.ForEach(i =>
        {
          Console.WriteLine($"{i.Name} is in your inventory");
        });
      }
      System.Console.WriteLine("Press Enter to Continue");
      Console.ReadLine();
    }

    public void Look()
    {
      Console.WriteLine($"{CurrentRoom.Description}");
    }

    public void Quit()
    {
      Playing = false;
      Console.WriteLine("Would you like to play again? y/n");
      ConsoleKeyInfo res = Console.ReadKey();
      if (res.KeyChar == 'y')
      {
        Setup();
        Playing = true;
        StartGame();
      }
      else if (res.KeyChar == 'n')
      {
        Console.WriteLine("KBYEEEE!");
        Console.ReadLine();
      }
      else
      {
        Console.WriteLine("Enter y or n!");
        Console.ReadLine();
        Quit();
      }
    }

    public void Reset()
    {
      Console.Clear();
      Setup();
      Playing = true;
      StartGame();
    }

    public void Setup()
    {
      Room mountain = new Room("At the top of A Mountain", "You made to the top of a mountain. PHEW! Tough climb. There a strong zephyr wind!");
      Room cave = new Room("In a Cave", "You have entered the cave! Woo! Now go find the treasure.");
      Room tunnel = new Room("In a Tunnel", "You're going down a tunnel. To the north is the cave but where is the water? ");
      Room water = new Room("At the edge of some water", "AH! YOU'RE ABOUT TO BE SWIMMING! WHAT COULD HELP YOU IN THIS MOMENT?");
      Room hole = new Room("In a hole", "You've entered a hole and there is a rickety, old boat in the corner. Maybe it'll help if it doesn't break first.");


      Item boat = new Item("Boat", "Whatever floats your boat!");
      hole.Items.Add(boat);

      mountain.Exits.Add("west", cave);

      cave.Exits.Add("north", hole); //cave goes to hole to get boat
      cave.Exits.Add("south", tunnel);
      cave.Exits.Add("east", mountain);

      hole.Exits.Add("east", mountain); //takes you back to start
      hole.Exits.Add("south", cave); //leaves hole with boat back to cave

      tunnel.Exits.Add("west", water);
      tunnel.Exits.Add("north", cave);

      //would like to make the exit to treasure a randomly generated exit, ALSO must use boat to get to treasure
      water.Exits.Add("east", tunnel);

      CurrentRoom = mountain;
    }

    public void StartGame()
    {
      Look();
      while (Playing)
      {
        IRoom curRoom = CurrentRoom;
        Console.WriteLine($"You are currently {curRoom.Name.ToUpper()}.");
        GetUserInput();
      }
    }

    public void TakeItem(string itemName)
    {

      Item item = CurrentRoom.Items.Find(i =>
      {
        return i.Name.ToLower() == itemName;
      });

      if (item != null)
      {
        CurrentRoom.Items.Remove(item);
        CurrentPlayer.Inventory.Add(item);
        Console.WriteLine($"You have {item.Name}. {item.Description}");
      }
      else
      {
        Console.WriteLine("There is nothing to take!");
      }
    }

    public void UseItem(string itemName)
    {
      string[] winDirs = { "north", "south", "west" };
      Random rnd = new Random();
      string treasure = winDirs[rnd.Next(0, 3)];
      string hint = winDirs[rnd.Next(0, 3)];

      if (CurrentRoom.Name == "At the edge of some water" && CurrentPlayer.Inventory[0].Name == "Boat" && itemName.ToLower() == "boat")
      {
        Console.WriteLine("Yay you can use the boat! What direction?");
        Console.WriteLine($"There's a glimmer of light to the {hint}.");
        string chosenDir = Console.ReadLine();
        string splitDir;

        string[] inputDir = chosenDir.Split(" ");
        if (inputDir.Length >= 2)
        {
          splitDir = inputDir[1];
        }
        else
        {
          splitDir = inputDir[0];
        }

        string treasureDir = treasure;
        if (splitDir == treasureDir)
        {
          Console.WriteLine("YOU WON! Excellente! You've got the bounty, matey!");
          Quit();
        }
        else
        {
          System.Console.WriteLine("You chose the wrong direction. Out, back and around you go!");
          CurrentRoom = CurrentRoom.Exits["east"].Exits["north"];
        }

      }
      else if (CurrentRoom.Name != "At the edge of some water" && CurrentPlayer.Inventory[0].Name == "Boat" && itemName.ToLower() == "boat")
      {
        Console.WriteLine($"Neat you have the {itemName}, but you can't use that here.");
      }
      else if (CurrentRoom.Name == "At the edge of some water" && CurrentPlayer.Inventory[0].Name != "Boat" && itemName.ToLower() == "boat")
      {
        Console.WriteLine("You're in the right place, but you don't have the stuff");
      }
      else
      {
        System.Console.WriteLine($"Cannot use {itemName}. Bummer dude!");
      }

    }

    public GameService(Player currPlayer)
    {
      CurrentPlayer = currPlayer;
      Playing = true;
    }
  }
}