using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLeagueProgram.ChampionModels
{
    public class Champion
    {
        public Champion()
        {
        }

        public string Name { get; set; }
        public string Title { get; set; }

        public string Image { get; set; }
        public string Mastery { get; set; }

        public int MasteryNumber { get; set; }

        public string FullName { get { return $"{Name}: {Title}"; } }

    }
}
