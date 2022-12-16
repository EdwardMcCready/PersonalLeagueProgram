using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLeagueProgram
{
    public class APIController
    {
        private string Key { get; set; }
        private string Region { get; set; }
        private string SummonerID { get; set; }
        public APIController(string key, string region)
        {
            Key = key;
            Region = region;
        }

        public Riot_Models.SummonerDTO GetSummonerByName(string summonerName)
        {
            var summoner_V4 = new API.Summoner_V4(Key, Region);
            var summoner = summoner_V4.GetSummonerByName(summonerName);
            if (summoner != null)
                SummonerID = summoner.ID;

            return summoner;
        }

        public List<Riot_Models.ChampionMasteryDto> GetChampionMasteries()
        {
            var champion_Mastery_V4 = new API.Champion_Mastery_V4(Key, Region);

            return champion_Mastery_V4.GetChampionMasteryByID(SummonerID);
        }
    }
}
