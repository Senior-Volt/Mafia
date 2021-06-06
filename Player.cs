using System;

namespace mafia_game
{
    class Player
    {
        public string Name { get; set; }
        public bool IsMafia { get; set; }

        public string Chat()
        {
            return Console.ReadLine();
        }

        public string Vote()
        {
            return Console.ReadLine();
        }
    }
}
