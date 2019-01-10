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


      Console.WriteLine("What would you like to do?");
      string userInput = Console.ReadLine().ToLower();
      string[] inputArr = userInput.Split(" ");
      string command = inputArr[0];

      string value;



      // if(value != null){
      //   Go(value);
      // }

      switch (command)
      {
        case "look":
          Console.Clear();
          Look();
          break;
        case "inventory":
          Console.Clear();
          Inventory();
          break;
        case "reset":
          Reset();
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
      }

    }

    public void Go(string direction)
    {
      Console.WriteLine($"You're trying to go {direction}");
      if (CurrentRoom.Name == "Top of A Mountain" && !CurrentRoom.Exits.ContainsKey(direction))
      {
        Playing = false;
        Console.WriteLine("OOP! You can't go that way");
        Console.WriteLine("Ruh roh! You fell off the mountain!");
        return;
      }
      if (CurrentRoom.Exits.ContainsKey(direction))
      {
        CurrentRoom = CurrentRoom.Exits[direction];
        // if thcze current room is the mtn and you got the boat you drop it yo
        Look();
      }
      else
      {
        Console.WriteLine("OOP! You can't go that way");
      }
    }

    public void Help()
    {
      throw new System.NotImplementedException();
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
          Console.WriteLine($"{i} is in your inventory");
        });
      }
      // Console.WriteLine(CurrentPlayer.Inventory);
      //iterate over the Inventory lists
      System.Console.WriteLine("Press Enter to Continue");
      Console.ReadLine();
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
    }

    public void Quit()
    {
      Playing = false;
      Console.WriteLine("Would you like to play again?");
    }

    public void Reset()
    {
      Console.Clear();
    }

    public void Setup()
    {
      Room mountain = new Room("Top of A Mountain", "You are at the top of a mountain. Ah! Don't fall! There a strong zephyr wind!");
      Room cave = new Room("In a Cave", "You have entered the cave! Woo! Now go find the treasure.");
      Room tunnel = new Room("Tunnel", "Down de tunnel! ");
      Room water = new Room("Water", "AH! YOU'RE SWIMMING! TRUFFLE SHUFFLE!"); //need to use boat to get to treasure
      Room hole = new Room("Hole", "You've entered a hole. But wait, there's boat. Neat!");
      Room treasure = new Room("Treasure Chest", "Excellente! You've got the bounty matey!");

      Item boat = new Item("Boat", "Hope this helps float ya to the treasure");
      hole.Items.Add(boat);

      mountain.Exits.Add("west", cave);

      cave.Exits.Add("north", hole); //cave goes to hole to get boat
      cave.Exits.Add("south", tunnel);
      cave.Exits.Add("east", mountain);

      hole.Exits.Add("east", mountain); //takes you back to start
      hole.Exits.Add("south", cave); //leaves hole with boat back to cave

      tunnel.Exits.Add("west", water);
      tunnel.Exits.Add("north", cave);

      water.Exits.Add("north", treasure); //would like to make the exit to treasure a randomly generated exit, ALSO must use boat to get to treasure
      water.Exits.Add("east", tunnel);

      CurrentRoom = mountain;
    }

    public void StartGame()
    {
      while (Playing)
      {
        Console.Clear();
        IRoom curRoom = CurrentRoom;
        Console.WriteLine($"You are currently at {curRoom.Name}. {curRoom.Description}");
        GetUserInput();
      }
    }

    public void TakeItem(string itemName)
    {
      GetUserInput();
      Item item = CurrentRoom.Items.Find(i =>
      {
        return i.Name == itemName;
      });

      if (item != null)
      {
        CurrentRoom.Items.Remove(item);
        CurrentPlayer.Inventory.Add(item);
      }
      else
      {
        Console.WriteLine("There is nothing to take");
      }
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
    public GameService(Player currPlayer)
    {
      CurrentPlayer = currPlayer;
      Playing = true;
    }
  }
}