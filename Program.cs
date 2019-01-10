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
      GameService gs = new GameService();
      gs.Setup();
      string playerName = Console.ReadLine();
      Console.WriteLine($"Hello {playerName}!");

      gs.StartGame();


    }
  }
}
