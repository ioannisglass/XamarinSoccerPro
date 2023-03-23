using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SokkerPro.Models
{
    public class Fixture : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        static String[] liveStatus = { "LIVE", "HT", "ET", "PEN_LIVE", "BREAK" };

        static Color goalColor = new Color(0, 0.6, 0.2);

        public int country_id { get; set; } = 0;
        public String country_name { get; set; } = "";
        public int league_id { get; set; } = 0;
        public String league_name { get; set; } = "";
        public int id { get; set; } = 0;
        public int? season_id { get; set; } = 0;
        public int? stage_id { get; set; } = 0;
        public int? round_id { get; set; } = 0;
        public int? group_id { get; set; } = 0;
        public int localteam_id { get; set; } = 0;
        public int visitorteam_id { get; set; } = 0;
        public int? winner_team_id { get; set; } = 0;
        public DateTime starting_time { get; set; } = new DateTime();
        public String status { get; set; } = "";
        public int race_to_goal { get; set; } = 0;
        public int home_score { get; set; } = 0;
        public int away_score { get; set; } = 0;
        public Dictionary<String, Object> scores = new Dictionary<String, Object>();
        public Dictionary<String, Object> localTeam = new Dictionary<String, Object>();
        public Dictionary<String, Object> visitorTeam = new Dictionary<String, Object>();
        public List<Object> lineup = new List<Object>();
        public List<Object> events = new List<Object>();
        public List<Object> corners = new List<Object>();
        public List<Object> stats = new List<Object>();
        public List<Dictionary<String, int>> expanded_stats = null;
        public Dictionary<String, Object> time = new Dictionary<String, Object>();
        public List<Dictionary<String, Object>> standing = new List<Dictionary<String, Object>>();
        public Dictionary<String, int> prediction = new Dictionary<String, int>();

        public int? highlight_team { get; set; } = -1;

        public bool _isFavorite;
        public bool isFav
        {
            get
            {
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;
                OnPropertyChanged("FavImage");
            }
        }
        public String FavImage
        {
            get
            {
                if (isFav)
                    return "favorite_star.png";
                return "outline_star.png";
            }
        }

        [JsonIgnore]
        public Command FavoriteCommand { get; set; } = null;


        public String HomeName
        {
            get
            {
                Object value = "";
                try
                {
                    if (!localTeam.TryGetValue("name", out value))
                        value = "";
                    if (value == null)
                        value = "";
                } catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return value.ToString();
            }
        }

        public String HomeLogo
        {
            get
            {
                Object value = "";
                try
                {
                    if (!localTeam.TryGetValue("logo_path", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return value.ToString();
            }
        }

        public String AwayName
        {
            get
            {
                Object value = "";
                try
                {
                    if (!visitorTeam.TryGetValue("name", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return value.ToString();
            }
        }

        public String AwayLogo
        {
            get
            {
                Object value = "";
                try
                {
                    if (!visitorTeam.TryGetValue("logo_path", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
                return value.ToString();
            }
        }

        public String StartingTime
        {
            get
            {
                if (starting_time == null)
                {
                    return "";
                }
                return starting_time.ToLocalTime().ToString("dd MMM | HH:mm");
            }
        }

        public String getExtraTime()
        {
            Object value = null;
            try
            {
                if (!time.TryGetValue("extra_minute", out value))
                    value = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            if (value == null)
                return null;
            return value.ToString();
        }

        public String getInjuryTime()
        {
            Object value = null;
            try
            {
                if (!time.TryGetValue("injury_time", out value))
                    value = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            if (value == null)
                return null;
            return value.ToString();
        }

        public String getMinute()
        {
            Object value = null;
            try
            {
                if (!time.TryGetValue("minute", out value))
                    value = null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
            if (value == null)
                return null;
            return value.ToString();
        }

        public String CurrentMinute
        {
            get
            {

                if (status == "HT")
                {
                    return status;
                }

                else if (getExtraTime() != null)
                {
                    if (getInjuryTime() != null)
                    {
                        return "Extra " + getExtraTime() + "+" + getInjuryTime();
                    }
                    else
                    {
                        return ("Extra " + getExtraTime());
                    }

                }
                else if (getMinute() != null)
                {
                    if (getInjuryTime() != null)
                    {
                        return getMinute() + "+" + getInjuryTime();
                    }
                    else
                    {
                        return getMinute();
                    }

                }
                else
                {
                    return (status + " - 0");
                }

            }
        }

        public String HomeScore
        {
            get
            {
                return getScore(true) + "";
            }
        }
        public Color HomeScoreColor
        {
            get
            {
                if (isLive && highlight_team != null && highlight_team == 0)
                    return goalColor;
                return Color.Red;
            }
        }
        public Color HomeNameColor
        {
            get
            {
                if (isLive && highlight_team != null && highlight_team == 0)
                    return goalColor;
                return Color.Black;
            }
        }

        public String AwayScore
        {
            get
            {
                return getScore(false) + "";
            }
        }
        public Color AwayScoreColor
        {
            get
            {
                if (isLive && highlight_team != null && highlight_team == 1)
                    return goalColor;
                return Color.Red;
            }
        }
        public Color AwayNameColor
        {
            get
            {
                if (isLive && highlight_team != null && highlight_team == 1)
                    return goalColor;
                return Color.Black;
            }
        }

        public String Score
        {
            get
            {

                if (status == "AET")
                {
                    return getScore(true) + ":" + getScore(false) + "(ET)";
                }
                else if (status == "FT_PEN")
                {
                    return getScore(true) + " : " + getScore(false) + "(" + getPenScore(true) + ":" + getPenScore(false) + ")";
                }
                else if ((scores != null))
                {
                    return getScore(true) + "   :   " + getScore(false);
                }
                else
                {
                    return "";
                }
            }

        }

        public bool isLive
        {
            get
            {
                var results = Array.FindAll(liveStatus, s => s.Equals(status));
                return results.Length > 0;
            }
        }
        public bool NotStarted
        {
            get
            {
                return status == "NS";
            }
        }
        public bool HasResult
        {
            get
            {
                return !isLive && !NotStarted;
            }
        }

        public int getPenScore(bool bHome)
        {
            Object value = "";
            try
            {
                if (bHome)
                {
                    if (!scores.TryGetValue("localteam_pen_score", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }
                else
                {
                    if (!scores.TryGetValue("visitorteam_pen_score", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            int result;
            if (!Int32.TryParse(value.ToString(), out result))
                result = 0;
            return result;
        }

        public int getScore(bool bHome)
        {
            Object value = "";
            try
            {
                if (bHome)
                {
                    if (!scores.TryGetValue("localteam_score", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }
                else
                {
                    if (!scores.TryGetValue("visitorteam_score", out value))
                        value = "";
                    if (value == null)
                        value = "";
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            int result;
            if (!Int32.TryParse(value.ToString(), out result))
                result = 0;
            return result;
        }

        public List<Dictionary<String, int>> getExpandedStats()
        {
            if ((expanded_stats == null))
            {
                expanded_stats = new List<Dictionary<String, int>>();
                foreach (Object item in stats)
                {
                    Dictionary<String, int> new_stat = new Dictionary<String, int>();
                    Dictionary<String, Object> stat = JsonConvert.DeserializeObject<Dictionary<String, Object>>(item.ToString());
                    foreach(KeyValuePair<String, Object> entry in stat) 
                    {
                        String key = entry.Key;
                        if (key == "fixture_id")
                        {
                            // TODO: Warning!!! continue If
                            continue;
                        }

                        Object value = entry.Value;
                        if (value is JContainer)
                        {
                            Dictionary<String, Object> sub_stat = JsonConvert.DeserializeObject<Dictionary<String, Object>>(value.ToString());
                            foreach (KeyValuePair<String, Object> sub_entry in sub_stat)
                            {
                                String sub_key = sub_entry.Key;
                                Object sub_value = sub_entry.Value;
                                if (sub_value == null)
                                {
                                    // TODO: Warning!!! continue If
                                    continue;
                                }

                                int result;
                                if (!Int32.TryParse(sub_value.ToString(), out result))
                                    result = 0;
                                new_stat.Add((key + ("-" + sub_key)), result);
                            }

                        }
                        else
                        {
                            if ((value == null))
                            {
                                // TODO: Warning!!! continue If
                                continue;
                            }

                            int result;
                            if (!Int32.TryParse(value.ToString(), out result))
                                result = 0;
                            new_stat.Add(key, result);
                        }

                    }

                    expanded_stats.Add(new_stat);
                }

            }

            return expanded_stats;
        }

        public int getStat(String statKey, bool bHome)
        {
            getExpandedStats();
            for (int index = 0; index <= 1; index++)
            {
                if (expanded_stats.Count > index)
                {
                    int firstid;
                    if (!expanded_stats[index].TryGetValue("team_id", out firstid))
                        firstid = 0;

                    if (bHome && firstid == localteam_id || !bHome && firstid == visitorteam_id)
                    {
                        int result;
                        if (!expanded_stats[index].TryGetValue(statKey, out result))
                            result = 0;
                        return result;
                    }
                }
            }

            return 0;
        }

        public Rectangle Home_Score_Rect
        {
            get
            {
                if (home_score + away_score == 0)
                    return new Rectangle(0, 0, 0.5, 1);
                return new Rectangle(0, 0, home_score * 1.0 / (home_score + away_score), 1);
            }
        }
        public Color Home_Score_Color
        {
            get
            {
                if (home_score >= away_score)
                    return new Color(0.29, 0.77, 0.41);
                return new Color(0.86, 0.36, 0.36);
            }
        }

        public Rectangle Away_Score_Rect
        {
            get
            {
                if (home_score + away_score == 0)
                    return new Rectangle(1, 0, 0.5, 1);
                return new Rectangle(1, 0, away_score * 1.0 / (home_score + away_score), 1);
            }
        }
        public Color Away_Score_Color
        {
            get
            {
                if (away_score > home_score)
                    return new Color(0.29, 0.77, 0.41);
                return new Color(0.86, 0.36, 0.36);
            }
        }

        public bool HomeHasRedCard
        {
            get
            {
                return (getStat("redcards", true) > 0 || getStat("yellowredcards", true) > 0);
            }
        }

        public bool AwayHasRedCard
        {
            get
            {
                return (getStat("redcards", false) > 0 || getStat("yellowredcards", false) > 0);
            }
        }
    }
}
