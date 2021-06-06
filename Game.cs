using System;
using System.Collections.Generic;

namespace mafia_game
{
    class Game
    {
        private List<Player> allPlayers = new List<Player>();
        private Dictionary<bool, List<Player>> playersByRoles = new Dictionary<bool, List<Player>>();
        private TimesOfDay timesOfDay;
        private bool isDay;

        public Game(List<Player> players)
        {
            this.allPlayers = players;
        }

        public void Start()
        {
            AssignRoles();
            isDay = false;
            timesOfDay = new TimesOfDay(playersByRoles[true], allPlayers);
        }

        public void Update()
        {
            string daytimeName = isDay ? "день" : "ночь";
            System.Console.WriteLine($"Сейчас {daytimeName}");
            System.Console.WriteLine("Сейчас игроки общаются");
            timesOfDay.Chat();
            System.Console.WriteLine("Сейчас игроки голосуют");
            var playerToKick = timesOfDay.Poll();
            KickPlayer(playerToKick);
            System.Console.WriteLine($"Игрок {playerToKick.Name} изгнан из игры");
            ChangeTimeOfDay();
        }

        public void ChangeTimeOfDay()
        {
            isDay = !isDay;

            if (isDay)
                timesOfDay = new TimesOfDay(allPlayers, allPlayers);
            else
                timesOfDay = new TimesOfDay(playersByRoles[true], allPlayers);
        }

        public bool CheckWin()
        {
            return playersByRoles[true].Count >= playersByRoles[false].Count || playersByRoles[true].Count == 0;
        }

        private void AssignRoles()
        {
            int mafiaCount = allPlayers.Count / 4;

            List<int> mafiaIndexes = new List<int>(mafiaCount);

            playersByRoles.Add(true, new List<Player>());
            playersByRoles.Add(false, new List<Player>());

            GenerateMafiaIndexes(mafiaCount, mafiaIndexes);

            for (int i = 0; i < allPlayers.Count; i++)
            {
                if (!mafiaIndexes.Contains(i))
                {
                    allPlayers[i].IsMafia = false;
                    playersByRoles[false].Add(allPlayers[i]);
                }
                else
                {
                    allPlayers[i].IsMafia = true;
                    playersByRoles[true].Add(allPlayers[i]);
                }
            }
        }

        private void GenerateMafiaIndexes(int mafiaCount, List<int> mafiaIndexes)
        {
            for (int i = 0; i < mafiaCount; i++)
            {
                int indexOfPlayer = 0;
                do
                {
                    indexOfPlayer = new Random().Next(0, allPlayers.Count);
                }
                while (mafiaIndexes.Contains(indexOfPlayer));

                mafiaIndexes.Add(indexOfPlayer);
            }
        }

        private void KickPlayer(Player player)
        {
            bool isMafia = player.IsMafia;
            allPlayers.Remove(player);
            playersByRoles[isMafia].Remove(player);
        }
    }
}
