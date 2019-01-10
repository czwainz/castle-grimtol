using System;
using CastleGrimtol.Project;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

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
      Player playa = new Player(playerName);
      GameService gs = new GameService(playa);
      Console.WriteLine($"Hello {playerName}!");


      gs.Setup();
      gs.StartGame();


    }
  }
}
