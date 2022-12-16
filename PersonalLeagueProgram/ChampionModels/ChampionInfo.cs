using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLeagueProgram.ChampionModels
{
    public class ChampionInfo
    {
        public Dictionary<string, ChampionData> Data { get; set; }
    }

    public class ChampionData
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public ChampionImage Image {get;set;}
    }

    public class ChampionImage
    {
        public string Full { get; set; }
    }
}
