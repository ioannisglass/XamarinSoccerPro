using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.Models
{
    public class Standing
    {
        public string team_name { get; set; }
        public string team_logo { get; set; }
        public long overall_league_position { get; set; }
        public long overall_league_payed { get; set; }
        public long overall_league_GF { get; set; }
        public long overall_league_GA { get; set; }
        public string overall_league_G
        {
            get => overall_league_GF + ":" + overall_league_GA;
        }
        public long overall_league_PTS { get; set; }
    }
}
