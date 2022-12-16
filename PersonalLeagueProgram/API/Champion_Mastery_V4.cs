using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLeagueProgram.API
{
    public class Champion_Mastery_V4 : RiotAPI
    {
        public Champion_Mastery_V4(string key, string region) : base(key, region)
        {
        }

        public List<Riot_Models.ChampionMasteryDto> GetChampionMasteryByID(string id)
        {
            string path = $"champion-mastery/v4/champion-masteries/by-summoner/{id}";

            return SendRequest<List<Riot_Models.ChampionMasteryDto>>(path);
        }
    }
}
