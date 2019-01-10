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

    public void GetUserInput()
    {
      IRoom curRoom = CurrentRoom;

      Console.Clear();
      Console.WriteLine($"You are currently at {curRoom.Name}.");
      Console.WriteLine("What would you like to do?");
      string userInput = Console.ReadLine().ToLower();
      string[] command = userInput.Split(" ");

      switch (command[0])
      {
        case "look":
          Console.Clear();
          Look();
          break;
        case "inventory":
          Console.Clear();
          Inventory();
          break;
        case "go":
          Go(command[1]);
          break;
      }

    }

    public void Go(string direction)
    {
      Console.WriteLine($"You're trying to go {direction}");
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      Console.WriteLine(CurrentPlayer.Inventory);
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    public void Reset()
    {

    }

    public void Setup()
    {
      Room mountain = new Room("Top of A Mountain", "You are at the top of a mountain. Ah! Don't fall! There a strong zephyr wind!");
      Room cave = new Room("Cave", "You have entered the cave! Woo! Now find the treasure.");
      Room tunnel = new Room("Tunnel", "Down de tunnel! ");
      Room water = new Room("Water", "AH! YOU'RE SWIMMING! TRUFFLE SHUFFLE!"); //need to use boat to get to treasure
      Room hole = new Room("Hole", "You've entered a hole. But wait, there's boat. Neat!");
      Room treasure = new Room("Treasure Chest", "Excellente! You've got the bounty matey!");

      Item boat = new Item("Boat", "Hope this helps float ya to the treasure");
      hole.Items.Add(boat);

      mountain.Exits.Add("west", cave);

      cave.Exits.Add("north", hole); //cave goes to hole to get boat
      hole.Exits.Add("east", mountain); //takes you back to start
      hole.Exits.Add("south", cave); //leaves hole with boat back to cave

      cave.Exits.Add("south", tunnel);
      tunnel.Exits.Add("west", water);

      water.Exits.Add("north", treasure); //would like to make the exit to treasure a randomly generated exit, ALSO must use boat to get to treasure

      CurrentRoom = mountain;
    }

    public void StartGame()
    {
      Setup();
      GetUserInput();
    }

    public void TakeItem(string itemName)
    {
      throw new System.NotImplementedException();
    }

    public void UseItem(string itemName)
    {
      throw new System.NotImplementedException();
    }
  }
}