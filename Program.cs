using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      //welcome
      //whats your name
      //do you want to play?
      //create an instance of gameservice and run startgame or setup
      Console.Write("Please enter your name: ");
      string playerName = Console.ReadLine();
      GameService gm = new GameService();
      gm.Setup();
      IRoom curRoom = gm.CurrentRoom;
      Console.WriteLine($"Hello {playerName}! You are at {curRoom.Name}");


    }
  }
}
