using BattleShip.BLL.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShip.UI
{
    public class BattleFlow
    {
        public void Start()
        {
            string Again = "y";
            bool playagain = true;

            while (playagain)
            {
                ConsoleIO.Welcome();
                
                Player player1 = new Player();
                Player.setUpPlayer(player1);

                Player player2 = new Player();
                Player.setUpPlayer(player2);

                ShotStatus shooting = new ShotStatus();
                Random PlayerRandom = new Random();
                int random = PlayerRandom.Next(1, 2);

                while (shooting != ShotStatus.Victory)
                {
                    if (random == 1)
                    {
                        Player.PlayerBoard(player1, player2);
                        shooting = Player.ShootShip(player2);
                        random = 2;
                    }
                    else 
                    {
                        Player.PlayerBoard(player2, player1);
                        shooting = Player.ShootShip(player1);
                        random = 1;
                    }                    
                }

                Console.WriteLine("Want to play again? y or n");
                Again = Console.ReadLine();
                if (Again == "y")
                {
                    playagain = true;
                }
                else
                    Console.WriteLine("Good Game!!!!");
                playagain = false;
            }
        }
    }
}
