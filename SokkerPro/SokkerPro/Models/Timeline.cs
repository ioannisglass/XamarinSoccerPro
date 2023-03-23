using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.Models
{
    public enum TimelineType
    {
        Goal,
        Own_Goal,
        Penalty_Fail,
        Penalty_Success,
        Yellow,
        Red,
        YellowRed,
        Subst,
        Corner
    }
    public enum TeamType
    {
        Home,
        Away
    }
    public class Timeline
    {
        public int index;
        public string time { get; set; } = "";
        public TimelineType timelineType { get; set; }
        public TeamType teamType { get; set; }
        public string MainPlayer{ get; set; } = "";
        public string RelatePlayer { get; set; } = "";
        public bool IsHomeEvent
        {
            get
            {
                return teamType == TeamType.Home;
            }
        }
        public bool IsAwayEvent
        {
            get
            {
                return teamType == TeamType.Away;
            }
        }
        public string mark
        {
            get
            {
                switch(timelineType)
                {
                    case TimelineType.Goal:
                        return "goal.png";
                    case TimelineType.Own_Goal:
                        return "own_goal.png";
                    case TimelineType.Penalty_Fail:
                        return "penalty_fail.png";
                    case TimelineType.Penalty_Success:
                        return "penalty_success.png";
                    case TimelineType.Yellow:
                        return "yellow_card.png";
                    case TimelineType.Red:
                        return "red_card.png";
                    case TimelineType.YellowRed:
                        return "yellow_red_card.png";
                    case TimelineType.Subst:
                        return "substitution.png";
                    case TimelineType.Corner:
                        return "corner.png";
                    default:
                        return "";
                }
            }
        }

        public Timeline(int index, string time, TimelineType timelineType, TeamType teamType, string MainPlayer, string RelatePlayer)
        {
            this.index = index;
            this.time = time;
            this.timelineType = timelineType;
            this.teamType = teamType;
            this.MainPlayer = MainPlayer;
            this.RelatePlayer = RelatePlayer;
        }

        public Timeline()
        {
        }
    }
}
