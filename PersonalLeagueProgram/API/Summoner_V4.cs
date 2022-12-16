using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLeagueProgram.API
{
    public class Summoner_V4 : RiotAPI
    {
        public Summoner_V4(string key, string region) : base(key, region)
        {

        }

        public Riot_Models.SummonerDTO GetSummonerByName(string summonerName)
        {
            string path = $"summoner/v4/summoners/by-name/{summonerName}";

            return SendRequest<Riot_Models.SummonerDTO>(path);
        }
    }
}
