using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using BattleShip.BLL.Ships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class Player
    {
        public string Name { get; set; }
        public Board Board { get; set; }

        public Player()
        {
            Board = new Board();
        }

        public static void setUpPlayer(Player currentPlayer)
        {
            currentPlayer.Name = ConsoleIO.GetNameFromUser("Please Enter Your Name:");
            Console.Clear();

            PlaceShip(currentPlayer.Name, currentPlayer.Board);
            Console.Clear();
        }

        public static void PlayerBoard(Player player, Player player2)
        {
            Board Board = new Board();
            string xrows = "ABCDEFGHIJ";
            char xchar = 'A';

            Console.Write("  1  2  3  4  5  6  7  8  9 10");

            for (int x = 0; x < 10; x++)
            {
                xchar = xrows[x];
                Console.WriteLine();
                Console.Write($"{xchar}");

                for (int y = 0; y < 10; y++)
                {
                    Coordinate coordinate = new Coordinate(x + 1, y + 1);
                    ShotHistory displayshot = new ShotHistory();

                    switch (displayshot = player.Board.CheckCoordinate(coordinate))
                    {
                        case ShotHistory.Hit:
                            Console.ForegroundColor = ConsoleColor.Red;Console.Write("[H]");
                            break;
                        case ShotHistory.Miss:
                            Console.ForegroundColor = ConsoleColor.Yellow;Console.Write("[M]");
                            break;
                        case ShotHistory.Unknown:
                            break;
                        default:
                            break;
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            Console.ReadKey();
            Console.Clear();
        }

        public static ShotStatus ShootShip(Player player)
        {
            while (true)
            {                
                Console.WriteLine("Fire you shot!!");
                FireShotResponse status = player.Board.FireShot(ConsoleIO.GetCoord());

                switch (status.ShotStatus)
                {
                    case ShotStatus.Duplicate:
                        Console.WriteLine("Already been targetted");
                        continue;
                    case ShotStatus.Hit:
                        Console.WriteLine("Direct Hit");
                        return status.ShotStatus;
                    case ShotStatus.HitAndSunk:
                        Console.WriteLine("Alright you sunk a ship");
                        return status.ShotStatus;
                    case ShotStatus.Invalid:
                        Console.WriteLine("That is Invalid");
                        continue;
                    case ShotStatus.Miss:
                        Console.WriteLine("Shot Miss!");
                        return status.ShotStatus;
                    case ShotStatus.Victory:
                        Console.WriteLine("Nice Job You Won!!!");
                        return status.ShotStatus;
                    default:
                        break;
                }
            }
        }

        public static void PlaceShip(string name, Board board)
        {
            Console.WriteLine($"{name} Place your ship: ");
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Place your {Enum.GetName(typeof(ShipType), (ShipType)i)}.");
                PlaceShipRequest request = new PlaceShipRequest();
                {
                    request.Coordinate = ConsoleIO.GetCoord();
                    request.Direction = ConsoleIO.GetDirection();
                    request.ShipType = (ShipType)i;
                };

                ShipPlacement spacevalidity = board.PlaceShip(request);

                switch (spacevalidity)
                {
                    case ShipPlacement.NotEnoughSpace:
                        i--;
                        Console.WriteLine("Not enough space, place somewhere else.");
                        continue;
                    case ShipPlacement.Overlap:
                        i--;
                        Console.WriteLine("Overlap another ship, place somewhere else.");
                        continue;
                    case ShipPlacement.Ok:
                        Console.WriteLine("Good spot!");
                        break;
                    default:
                        break;
                }
            }
        }     
    }
}

