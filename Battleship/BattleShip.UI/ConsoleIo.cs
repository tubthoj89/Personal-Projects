using BattleShip.BLL.GameLogic;
using BattleShip.BLL.Requests;
using BattleShip.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class ConsoleIO
    {
        public static void Welcome()
        {
            Console.WriteLine("*****************************");
            Console.WriteLine("****Welcome to BATTLESHIP****");
            Console.WriteLine("***Press**Enter**To**Begin***");
            Console.WriteLine("*****************************");
            Console.ReadKey();
            Console.Clear();
        }


        public static string GetNameFromUser(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string result = Console.ReadLine();
                if (result == "" || result == string.Empty)
                {
                    Console.WriteLine("You entered a blank, please try again");
                    continue;
                }
                return result;
            }
        }

        public static ShipDirection GetDirection()
        {
            do
            {
                Console.WriteLine("Ship direction, U(up), D(down), L(left), R(right): ");
                string directinput = Console.ReadLine();
                directinput = directinput.ToUpper();

                switch (directinput)
                {
                    case "U":
                        return ShipDirection.Up;
                    case "D":
                        return ShipDirection.Down;
                    case "L":
                        return ShipDirection.Left;
                    case "R":
                        return ShipDirection.Right;
                    default:
                        Console.WriteLine("Invalid answer, please try again.");
                        continue;
                }
            } while (true);
        }

        public static Coordinate GetCoord()
        {
            string xstring = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int y = 0;
            int x = 0;

            while (true)
            {
                Console.WriteLine("Enter coordinate: ");
                string coord = Console.ReadLine();
                coord = coord.ToUpper();
                char xChar = coord[0];
                y = int.Parse(coord.Substring(1));
                if (char.IsLetter(xChar) == true)
                {
                    for (int i = 0; i < xstring.Length; i++)
                    {
                        if (xChar == xstring[i])
                        {
                            x = i + 1;
                        }
                    }
                    if (x > 10 || x < 1)
                    {
                        Console.WriteLine("Invalid!");
                        continue;
                    }
                    if (y > 10 || y < 1)
                    {
                        Console.WriteLine("Invalid");
                        continue;
                    }

                }
                Coordinate xy = new Coordinate(x, y);
                return xy;
            }
        }
    }
}




