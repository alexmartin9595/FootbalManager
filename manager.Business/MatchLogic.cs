using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using manager.Data;
using manager.Entities;

namespace manager.Business
{
    public class MatchLogic
    {
        private MatchStorage matchStorage;
        private static double attackIndex = 1.538;
        private static double midIndex = 0.769;
        private static double defIndex = 0.385;
        private TeamPlayerLogic teamPlayerLogic;
        private GoalLogic goalLogic;

        public MatchLogic()
        {
            matchStorage = new MatchStorage();
            teamPlayerLogic = new TeamPlayerLogic();
            goalLogic = new GoalLogic();
        }

        public IEnumerable<Match> GetMatchesByUser(int teamId)
        {
            return matchStorage.GetMatchesByUser(teamId);
        }

        public Match GetMatchById(int id)
        {
            return matchStorage.GetMatchById(id);
        }

        public int GetCurrentMinute(int matchId)
        {
            return matchStorage.GetMatchById(matchId).CurrentMinute;
        }

        public void UpdateTimer(int matchId)
        {
            Match match = GetMatchById(matchId);
            match.CurrentMinute++;
            UpdateMatch(matchId, match);
        }

        public void AddMatch(Match match)
        {
            matchStorage.AddMacth(match);
        }

        public void UpdateMatch(int matchId, Match match)
        {
            Match currentMatch = GetMatchById(matchId);
            if (currentMatch == null)
                throw new Exception("Матч не найден");
            matchStorage.UpdateMatch(matchId, match);
        }

        public void CalculateMinute(int firstTeamId, int secondTeamId, int matchId, int minute)
        {
            IsSaved(firstTeamId, secondTeamId, matchId, true, minute);
            IsSaved(secondTeamId, firstTeamId, matchId, false, minute);
        }

        public double GetRandomValue()
        {
            int[] randomList = new int[100];
            Random random = new Random();
            int maxValue = 0;
            int maxNumber = 0;
            for (int i = 0; i < 100; i++)
            {
                int number = random.Next(0, 100);
                randomList[number]++;
                if (maxValue < randomList[number])
                {
                    maxValue = randomList[number];
                    maxNumber = number;
                }
            }
            double value = maxNumber;
            value /= 100;
            return value;
        }

        public List<double> GetStrikeProbability(int teamId)
        {
            List<double> attackList = new List<double>();
            List<double> probabilityList = new List<double>();
            List<double> sumList = new List<double>();
            int attackSum = 0;
            List< TeamPlayer> players = new List<TeamPlayer>();
            foreach (var player in teamPlayerLogic.GetAllPlayers(teamId))
            {
                attackSum += player.Atack;
                attackList.Add(player.Atack);
                players.Add(player);
            }

            for (int i = 0; i < attackList.Count; i++)
            {
                double currentProbability = attackList[i] / attackSum / 3;
                probabilityList.Add(currentProbability);
            }

            double currentSum = 0;
            for (int i = 0; i < probabilityList.Count; i++)
            {
                switch (players[i].Position)
                {
                    case "вратарь": currentSum += defIndex * probabilityList[i];
                        break;
                    case "защитник": currentSum += defIndex * probabilityList[i];
                        break;
                    case "полузащитник": currentSum += midIndex * probabilityList[i];
                        break;
                    case "нападающий": currentSum += attackIndex * probabilityList[i];
                        break;
                }
                sumList.Add(currentSum);
            }
            return sumList;
        }

        public int GetStrikerIndex(List<double> sumList, int teamId)
        {
            int playerindex = -1;
            double index = GetRandomValue();
            List<TeamPlayer> players = new List<TeamPlayer>();
            foreach (var player in teamPlayerLogic.GetAllPlayers(teamId))
            {
                players.Add(player);
            }
            for (int i = 0; i < sumList.Count; i++)
            {
                if (index <= sumList[i])
                {
                    return players[i].Id;                    
                }
            }
            return playerindex;
        }

        public TeamPlayer SelectStriker(int teamId)
        {
            List<double> sumList = GetStrikeProbability(teamId);
            int playerIndex = GetStrikerIndex(sumList, teamId);
            if (playerIndex == -1)
                return null;
            return teamPlayerLogic.GeTeamPlayer(playerIndex);
        }

        public double CountAverageDefence(int teamId)
        {
            double defenceSum = 0;
            foreach (var player in teamPlayerLogic.GetAllPlayers(teamId))
            {
                switch (player.Position)
                {
                    case "вратарь": 
                        break;
                    case "защитник": defenceSum += attackIndex * player.Defence;
                        break;
                    case "полузащитник": defenceSum +=  midIndex * player.Defence;
                        break;
                    case "нападающий": defenceSum += defIndex * player.Defence;
                        break;
                }
            }
            defenceSum /= 10;
            return defenceSum;
        }

        public bool IsBlocked(int attackTeamId, int defTeamId, out TeamPlayer striker)
        {
            striker = SelectStriker(attackTeamId);
            if (striker == null)
                return true;
            int strikerAttack = striker.Atack;
            double averageDefence = CountAverageDefence(defTeamId);
            double blockProbability;
            if (strikerAttack >= averageDefence)
                blockProbability = 0.75;
            else
                blockProbability = 0.75 + (averageDefence - strikerAttack) / 100;
            if (blockProbability >= 1)
                blockProbability = 0.99;
            double index = GetRandomValue();
            if (index < blockProbability)
                return true;
            return false;
        }

        public int GetCeaperDefence(int teamId)
        {
            foreach (var player in teamPlayerLogic.GetAllPlayers(teamId))
            {
                if (player.Position == "вратарь")
                    return player.Defence;
            }
            return -1;
        }

        public bool IsSaved(int attackTeamId, int defTeamId, int matchId, bool isFirst, int minute)
        {
            TeamPlayer striker;
            if (IsBlocked(attackTeamId, defTeamId, out striker))
                return true;
            double saveprobability;
            int strikerAttack = striker.Atack;
            int ceaperDefence = GetCeaperDefence(defTeamId);

            if (strikerAttack >= ceaperDefence)
                saveprobability = 0.65;
            else
                saveprobability = 0.65 + (ceaperDefence - strikerAttack) / 100;

            if (saveprobability >= 1)
                saveprobability = 0.99;
            double index = GetRandomValue();
            if (index < saveprobability)
                return true;

            Match match = GetMatchById(matchId);
            int teamId = -1;
            if (isFirst)
            {
                match.FirstTeamGoals++;
                teamId = match.FirstTeamId;
            }
            else
            {
                match.SecondTeamGoals++;
                teamId = match.SecondTeamId;
            }
            Goal goal = new Goal
            {
                MatchId = matchId,
                PlayerId = striker.Id,
                Minute = minute, 
                TeamId = teamId
            };
            goalLogic.AddGoal(goal);
            matchStorage.UpdateMatch(matchId, match);
            return false;
        }
    }
}

