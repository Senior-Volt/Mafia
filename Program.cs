using System.Collections.Generic;

namespace mafia_game
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Player> players = new List<Player>();
            Player p1 = new Player()
            {
                Name = "ivan"
            };
            Player p2 = new Player()
            {
                Name = "dmitry"
            };
            Player p3 = new Player()
            {
                Name = "oleg"
            };
            Player p4 = new Player()
            {
                Name = "vasya"
            };

            players.Add(p1);
            players.Add(p2);
            players.Add(p3);
            players.Add(p4);

            Game game = new Game(players);

            game.Start();
            while(!game.CheckWin())
            {
                game.Update();
            }
            System.Console.WriteLine("Игра закончилась!");
        }
    }
}
