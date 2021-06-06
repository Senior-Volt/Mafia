using System.Collections.Generic;

namespace mafia_game
{
    class TimesOfDay
    {
        private IReadOnlyList<Player> allPlayers;
        private List<Player> players;
        private Dictionary<Player, int> votes = new Dictionary<Player, int>();

        public TimesOfDay(List<Player> players, IReadOnlyList<Player> allPlayers)
        {
            this.players = players;
            this.allPlayers = allPlayers;
        }

        public void Chat()
        {
            foreach (Player player in players)
            {
                System.Console.WriteLine($"Говорит игрок {player.Name}");
                var text = player.Chat();
                System.Console.WriteLine($"Игрок {player.Name} сказал: {text}");
            }
        }

        public Player Poll()
        {
            foreach (Player player in players)
            {
                System.Console.WriteLine($"Голосует игрок {player.Name}");
                var text = player.Vote();
                foreach (Player player1 in allPlayers)
                {
                    if (player1.Name == text)
                    {
                        if (votes.ContainsKey(player1))
                            votes[player1] += 1;
                        else
                            votes.Add(player1, 1);
                    }
                }
            }

            Player playerToKick = null;
            int maxVotes = 0;

            foreach (var item in votes)
            {
                if (maxVotes < item.Value)
                {
                    maxVotes = item.Value;
                    playerToKick = item.Key;
                }
            }

            return playerToKick;
        }
    }
}
