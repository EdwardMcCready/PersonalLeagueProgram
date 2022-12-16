using System;
using System.Collections.Generic;
using System.Text;

namespace PersonalLeagueProgram
{
    public class Champion
    {
        public Champion(string name, string title, string image)
        {
            Name = name;
            Title = title;
            Image = image;
        }

        public string Name { get; set; }
        public string Title { get; set; }
        public string FullName { get { return $"{Name}: {Title}"; } }
        public string Image { get; set; }
    }
}
