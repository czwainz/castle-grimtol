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
      throw new System.NotImplementedException();
    }

    public void Go(string direction)
    {
      throw new System.NotImplementedException();
    }

    public void Help()
    {
      throw new System.NotImplementedException();
    }

    public void Inventory()
    {
      throw new System.NotImplementedException();
    }

    public void Look()
    {
      throw new System.NotImplementedException();
    }

    public void Quit()
    {
      throw new System.NotImplementedException();
    }

    public void Reset()
    {
      throw new System.NotImplementedException();
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
      throw new System.NotImplementedException();
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