using I18NPortable;
using Newtonsoft.Json;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SokkerPro.Models;
using SokkerPro.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;
using static SokkerPro.Views.LivePage;

namespace SokkerPro.ViewModels
{

    public class MatchsViewModel : BaseViewModel
    {
        static Color winingColor = new Color(0, 0.8, 0);
        static Color drawColor = new Color(0, 0, 0);
        static Color losingColor = new Color(1, 0.2, 0);
        public class TeamInfo
        {
            public int team_key;
            public string team_name;
            public string team_badge;
        }

        public class TempResponse
        {
            public string status;
            public Fixture data;
        }
        public class ChartResponse
        {
            public int game_time;
            public int home_pts;
            public int away_pts;
        }

        public class TimelineList : List<Timeline>
        {
            public string Half_Name { get; set; }
            public List<Timeline> Timelines => this;
        }

        public Fixture _match = new Fixture();
        public Fixture Match
        {
            get { return _match; }
            set { SetProperty(ref _match, value); }
        }
        public List<TimelineList> _timeline = new List<TimelineList>();
        public List<TimelineList> Timeline
        {
            get { return _timeline; }
            set { SetProperty(ref _timeline, value); }
        }
        public List<Statistic> _statistic = new List<Statistic>();
        public List<Statistic> Statistics
        {
            get { return _statistic; }
            set { SetProperty(ref _statistic, value); }
        }
        public class LineupItem
        {
            public Lineup home { get; set; }
            public Lineup away { get; set; }
        }
        public List<LineupItem> _lineup = new List<LineupItem>();
        public List<LineupItem> Lineups
        {
            get { return _lineup; }
            set { SetProperty(ref _lineup, value); }
        }

        public List<Standing> _standing = new List<Standing>();
        public List<Standing> Standings
        {
            get { return _standing; }
            set { SetProperty(ref _standing, value); }
        }

        public PlotModel _liveChart;
        public PlotModel LiveChart
        {
            get { return _liveChart; }
            set { SetProperty(ref _liveChart, value); }
        }

        String[] order_keys = new String[] {
            "possessiontime",
            "goal_attempts",
            "shots-ongoal",
            "shots-offgoal",
            "shots-blocked",
            "free_kick",
            "corners",
            "offsides",
            "saves",
            "fouls",
            "yellowcards",
            "passes-total",
            "passes-accurate",
            "attacks-attacks",
            "attacks-dangerous_attacks"
        };
        Dictionary<String, String> english = new Dictionary<String, String>
        {
            { "shots-total", "Total shots" },
            { "shots-ongoal", "Shots on goal" },
            { "shots-offgoal", "Shots off goal" },
            { "shots-blocked", "Blocked shots" },
            { "shots-insidebox", "Shots inside box" },
            { "shots-outsidebox", "Shots outside box" },
            { "passes-total", "Total passes" },
            { "passes-accurate", "Accurate passes" },
            { "passes-percentage", "Passing percentage" },
            { "attacks-attacks", "Total attacks" },
            { "attacks-dangerous_attacks", "Dangerous attacks" },
            { "goals", "Goals" },
            { "penalties", "Penalties" },
            { "injuries", "Injuries" },
            { "fouls", "Fouls" },
            { "corners", "Corners" },
            { "offsides", "Offside" },
            { "possessiontime", "Ball possession" },
            { "yellowcards", "Yellow cards" },
            { "redcards", "Red cards" },
            { "yellowredcards", "YellowRed cards" },
            { "saves", "Saves" },
            { "substitutions", "Substitutions" },
            { "goal_kick", "Goal kicks" },
            { "goal_attempts", "Goal attempts" },
            { "free_kick", "Free kicks" },
            { "throw_in", "Throw-ins" },
            { "ball_safe", "Ball safe" }
        };
        Dictionary<String, String> portuguese = new Dictionary<String, String>
        {
            { "shots-total", "Remate Totais" },
            { "shots-ongoal", "Remates ao Gol" },
            { "shots-offgoal", "Remates fora do Gol" },
            { "shots-blocked", "Remates Bloqueados" },
            { "shots-insidebox", "Remates dentro da Trave" },
            { "shots-outsidebox", "Rematess fora da Trave" },
            { "passes-total", "Passes Totais" },
            { "passes-accurate", "Acerto dos Passes" },
            { "passes-percentage", "Pocentagem dos Passses" },
            { "attacks-attacks", "Ataques Totais" },
            { "attacks-dangerous_attacks", "Ataques Perigosos" },
            { "goals", "Gols" },
            { "penalties", "Penaltis" },
            { "injuries", "Lesões" },
            { "fouls", "Faltas" },
            { "corners", "Cantos" },
            { "offsides", "Fora de Jogo" },
            { "possessiontime", "Posse de Bola" },
            { "yellowcards", "Cartão Amarelo" },
            { "redcards", "Cartão Vermelho" },
            { "yellowredcards", "YellowRed cards" },
            { "saves", "Defesas" },
            { "substitutions", "Substituições" },
            { "goal_kick", "Chutes ao Gol" },
            { "goal_attempts", "Tentativa de Gols" },
            { "free_kick", "Free kicks" },
            { "throw_in", "Throw-ins" },
            { "ball_safe", "Ball safe" }
        };

        public string _prediction_1x2_title;
        public string prediction_1x2_title
        {
            get { return _prediction_1x2_title; }
            set { SetProperty(ref _prediction_1x2_title, value); }
        }
        public Color _prediction_1x2_color;
        public Color prediction_1x2_color
        {
            get { return _prediction_1x2_color; }
            set { SetProperty(ref _prediction_1x2_color, value); }
        }
        public string _prediction_corner_title;
        public string prediction_corner_title
        {
            get { return _prediction_corner_title; }
            set { SetProperty(ref _prediction_corner_title, value); }
        }
        public string _prediction_corner_value;
        public string prediction_corner_value
        {
            get { return _prediction_corner_value; }
            set { SetProperty(ref _prediction_corner_value, value); }
        }
        public Color _prediction_corner_color;
        public Color prediction_corner_color
        {
            get { return _prediction_corner_color; }
            set { SetProperty(ref _prediction_corner_color, value); }
        }
        public string _prediction_btts_title;
        public string prediction_btts_title
        {
            get { return _prediction_btts_title; }
            set { SetProperty(ref _prediction_btts_title, value); }
        }
        public string _prediction_btts_value;
        public string prediction_btts_value
        {
            get { return _prediction_btts_value; }
            set { SetProperty(ref _prediction_btts_value, value); }
        }
        public Color _prediction_btts_color;
        public Color prediction_btts_color
        {
            get { return _prediction_btts_color; }
            set { SetProperty(ref _prediction_btts_color, value); }
        }
        public string _prediction_over05_title;
        public string prediction_over05_title
        {
            get { return _prediction_over05_title; }
            set { SetProperty(ref _prediction_over05_title, value); }
        }
        public string _prediction_over05_value;
        public string prediction_over05_value
        {
            get { return _prediction_over05_value; }
            set { SetProperty(ref _prediction_over05_value, value); }
        }
        public Color _prediction_over05_color;
        public Color prediction_over05_color
        {
            get { return _prediction_over05_color; }
            set { SetProperty(ref _prediction_over05_color, value); }
        }
        public string _prediction_over15_title;
        public string prediction_over15_title
        {
            get { return _prediction_over15_title; }
            set { SetProperty(ref _prediction_over15_title, value); }
        }
        public string _prediction_over15_value;
        public string prediction_over15_value
        {
            get { return _prediction_over15_value; }
            set { SetProperty(ref _prediction_over15_value, value); }
        }
        public Color _prediction_over15_color;
        public Color prediction_over15_color
        {
            get { return _prediction_over15_color; }
            set { SetProperty(ref _prediction_over15_color, value); }
        }
        public string _prediction_over25_title;
        public string prediction_over25_title
        {
            get { return _prediction_over25_title; }
            set { SetProperty(ref _prediction_over25_title, value); }
        }
        public string _prediction_over25_value;
        public string prediction_over25_value
        {
            get { return _prediction_over25_value; }
            set { SetProperty(ref _prediction_over25_value, value); }
        }
        public Color _prediction_over25_color;
        public Color prediction_over25_color
        {
            get { return _prediction_over25_color; }
            set { SetProperty(ref _prediction_over25_color, value); }
        }

        public MatchsViewModel()
        {
            
        }

        public async Task<Fixture> getMatch(int match_id)
        {
            Fixture match = new Fixture();

            try
            {
                var request = WebRequest.Create(App.BACKEND_URL + "/fixture/" + match_id + "/" + App.token) as HttpWebRequest;
                request.Method = "GET";
                string responseContent = null;
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();

                        var resp = JsonConvert.DeserializeObject<TempResponse>(responseContent);
                        match = resp.data;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return match;
        }

        public async Task LoadChartAsync(int fixture_id)
        {
            try
            {
                var request = WebRequest.Create(App.BACKEND_URL + "/getChart/" + fixture_id) as HttpWebRequest;
                request.Method = "GET";
                string responseContent = null;
                using (var response = (HttpWebResponse)(await Task<WebResponse>.Factory.FromAsync(request.BeginGetResponse, request.EndGetResponse, null)))
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();

                        var resp = JsonConvert.DeserializeObject<List<ChartResponse>>(responseContent);

                        var model = new PlotModel();
                        model.PlotAreaBorderColor = OxyColor.FromRgb(242, 242, 242);

                        model.Axes.Add(new LinearAxis()
                        {
                            Position = AxisPosition.Bottom,
                            Minimum = 0,
                            Maximum = 90,
                            MajorStep = 5,
                            MinorStep = 5,
                            TickStyle = TickStyle.None,
                            MajorGridlineStyle = LineStyle.Solid,
                            MajorGridlineColor = OxyColor.FromRgb(242, 242, 242),
                            MajorGridlineThickness = 1
                        });
                        model.Axes.Add(new LinearAxis()
                        {
                            Position = AxisPosition.Left,
                            TickStyle = TickStyle.None,
                            MajorGridlineStyle = LineStyle.Solid,
                            MajorGridlineColor = OxyColor.FromRgb(242, 242, 242),
                            MajorGridlineThickness = 1
                        });

                        var s1 = new LineSeries()
                        {
                            Color = OxyColors.Red,
                            StrokeThickness = 1,
                            MarkerType = MarkerType.Circle,
                            MarkerSize = 2,
                            MarkerStroke = OxyColors.SkyBlue,
                            MarkerFill = OxyColors.White,
                            MarkerStrokeThickness = 1
                        };
                        var s2 = new LineSeries()
                        {
                            Color = OxyColor.FromRgb(0, 204, 51),
                            StrokeThickness = 1,
                            MarkerType = MarkerType.Circle,
                            MarkerSize = 2,
                            MarkerStroke = OxyColors.SkyBlue,
                            MarkerFill = OxyColors.White,
                            MarkerStrokeThickness = 1
                        };

                        foreach (ChartResponse chart in resp)
                        {
                            s1.Points.Add(new DataPoint(chart.game_time, chart.home_pts));
                            s2.Points.Add(new DataPoint(chart.game_time, chart.away_pts));
                        }

                        model.Series.Add(s1);
                        model.Series.Add(s2);

                        LiveChart = model;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        public async void LoadMatchInfo(int match_id, bool bInit)
        {
            Match = await getMatch(match_id);
            if (bInit)
            {
                try
                {
                    // Prediction
                    Dictionary<String, int> pred = Match.prediction;
                    if (pred["corner"] >= 10)
                    {
                        prediction_corner_title = "OVER";
                        prediction_corner_value = "10";
                        prediction_corner_color = drawColor;
                    }
                    else if (pred["corner"] >= 0)
                    {
                        prediction_corner_title = "UNDER";
                        prediction_corner_value = "11.5";
                        prediction_corner_color = drawColor;
                    }
                    else
                    {
                        prediction_corner_title = "";
                        prediction_corner_value = "RISK";
                        prediction_corner_color = winingColor;
                    }

                    if (pred["btts"] >= 60)
                    {
                        prediction_btts_title = "YES";
                        prediction_btts_color = winingColor;
                    }
                    else if (pred["btts"] > 20 || pred["btts"] < 0)
                    {
                        prediction_btts_title = "RISK";
                        prediction_btts_color = drawColor;
                    }
                    else
                    {
                        prediction_btts_title = "NO";
                        prediction_btts_color = losingColor;
                    }
                    if (pred["btts"] < 0)
                        prediction_btts_value = "";
                    else
                        prediction_btts_value = pred["btts"] + "%";

                    if (pred["home_win"] < 0 && pred["away_win"] < 0)
                    {
                        prediction_1x2_title = "RISK";
                        prediction_1x2_color = losingColor;
                    }
                    else if (pred["home_win"] > pred["away_win"])
                    {
                        if (pred["home_win"] >= 80)
                        {
                            prediction_1x2_title = "1 WIN";
                            prediction_1x2_color = winingColor;
                        }
                        else
                        {
                            prediction_1x2_title = "1x";
                            prediction_1x2_color = drawColor;
                        }
                    }
                    else
                    {
                        if (pred["away_win"] >= 80)
                        {
                            prediction_1x2_title = "2 WIN";
                            prediction_1x2_color = winingColor;
                        }
                        else
                        {
                            prediction_1x2_title = "2x";
                            prediction_1x2_color = drawColor;
                        }
                    }

                    if (pred["over05"] >= 60)
                    {
                        prediction_over05_title = "YES";
                        prediction_over05_color = winingColor;
                    }
                    else if (pred["over05"] > 30)
                    {
                        prediction_over05_title = "RISK";
                        prediction_over05_color = drawColor;
                    }
                    else
                    {
                        prediction_over05_title = "NO";
                        prediction_over05_color = losingColor;
                    }
                    prediction_over05_value = pred["over05"] + "%";

                    if (pred["over15"] >= 60)
                    {
                        prediction_over15_title = "YES";
                        prediction_over15_color = winingColor;
                    }
                    else if (pred["over15"] > 30)
                    {
                        prediction_over15_title = "RISK";
                        prediction_over15_color = drawColor;
                    }
                    else
                    {
                        prediction_over15_title = "NO";
                        prediction_over15_color = losingColor;
                    }
                    prediction_over15_value = pred["over15"] + "%";

                    if (pred["over25"] >= 60)
                    {
                        prediction_over25_title = "YES";
                        prediction_over25_color = winingColor;
                    }
                    else if (pred["over25"] > 30)
                    {
                        prediction_over25_title = "RISK";
                        prediction_over25_color = drawColor;
                    }
                    else
                    {
                        prediction_over25_title = "NO";
                        prediction_over25_color = losingColor;
                    }
                    prediction_over25_value = pred["over25"] + "%";
                } catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }

                // Lineup
                List<LineupItem> ups = new List<LineupItem>();
                for (int i = 0; i < Match.lineup.Count / 2; i++)
                {
                    object home = Match.lineup[i];
                    object away = Match.lineup[i + Match.lineup.Count / 2];

                    Dictionary<String, Object> homeRes = JsonConvert.DeserializeObject<Dictionary<String, Object>>(home.ToString());
                    Dictionary<String, Object> awayRes = JsonConvert.DeserializeObject<Dictionary<String, Object>>(away.ToString());

                    LineupItem up = new LineupItem();
                    up.home = new Lineup
                    {
                        lineup_number = homeRes["number"].ToString(),
                        lineup_player = homeRes["player_name"].ToString()
                    };
                    up.away = new Lineup
                    {
                        lineup_number = awayRes["number"].ToString(),
                        lineup_player = awayRes["player_name"].ToString()
                    };
                    ups.Add(up);
                }
                Lineups = ups;

                // Rank
                List<Standing> standings = new List<Standing>();
                foreach (Dictionary<String, Object> stand in Match.standing)
                {
                    if (stand == null)
                        continue;
                    if (stand["standings"] == null)
                        continue;
                    Dictionary<String, Object> sub_stand = JsonConvert.DeserializeObject<Dictionary<String, Object>>(stand["standings"].ToString());
                    if (sub_stand == null)
                        continue;
                    if (sub_stand["data"] == null)
                        continue;
                    List<Dictionary<String, Object>> real_stand = JsonConvert.DeserializeObject<List<Dictionary<String, Object>>>(sub_stand["data"].ToString());
                    foreach (Dictionary<String, Object> real_stand_item in real_stand)
                    {
                        Standing newitem = new Standing();
                        if (real_stand_item.ContainsKey("position"))
                            newitem.overall_league_position = (long)real_stand_item["position"];
                        if (real_stand_item.ContainsKey("points"))
                            newitem.overall_league_PTS = (long)real_stand_item["points"];
                        newitem.team_name = (String)real_stand_item["team_name"];
                        if (real_stand_item.ContainsKey("overall"))
                        {
                            Dictionary<String, Object> overall = JsonConvert.DeserializeObject<Dictionary<String, Object>>(real_stand_item["overall"].ToString());
                            if (overall.ContainsKey("games_played"))
                                newitem.overall_league_payed = (long)overall["games_played"];
                            if (overall.ContainsKey("goals_scored"))
                                newitem.overall_league_GF = (long)overall["goals_scored"];
                            if (overall.ContainsKey("goals_against"))
                                newitem.overall_league_GA = (long)overall["goals_against"];
                        }
                        if (real_stand_item.ContainsKey("team"))
                        {
                            Dictionary<String, Object> team = JsonConvert.DeserializeObject<Dictionary<String, Object>>(real_stand_item["team"].ToString());
                            if (team.ContainsKey("data"))
                            {
                                Dictionary<String, Object> teamdata = JsonConvert.DeserializeObject<Dictionary<String, Object>>(team["data"].ToString());
                                newitem.team_logo = (String)teamdata["logo_path"];
                            }
                        }
                        standings.Add(newitem);
                    }
                }
                Standings = standings;
            }

            // Timeline
            List<TimelineList> resp = new List<TimelineList>();
            List<Timeline> lines = new List<Timeline>();
            resp.Add(new TimelineList() { Half_Name = "1º Half" });
            resp.Add(new TimelineList() { Half_Name = "2º Half" });
            foreach (object evt in Match.events)
            {
                Dictionary<String, Object> obj = JsonConvert.DeserializeObject<Dictionary<String, Object>>(evt.ToString());
                if (obj == null)
                    continue;
                Timeline tm = new Timeline();
                if (obj["team_id"].Equals(Match.localteam_id.ToString()))
                    tm.teamType = TeamType.Home;
                else
                    tm.teamType = TeamType.Away;
                if (obj["minute"] != null)
                {
                    tm.time = obj["minute"].ToString();
                    tm.index = Int32.Parse(obj["minute"].ToString());
                }
                if (obj["extra_minute"] != null)
                {
                    tm.time += "+" + obj["extra_minute"].ToString();
                    tm.index = tm.index * 10 + Int32.Parse(obj["extra_minute"].ToString());
                }
                tm.time += "'";

                tm.MainPlayer = (String)obj["player_name"];
                tm.RelatePlayer = (String)obj["related_player_name"];
                if (tm.RelatePlayer != null && !tm.RelatePlayer.Equals(""))
                    tm.RelatePlayer = "(" + tm.RelatePlayer + ")";

                if (obj["type"].Equals("penalty") || obj["type"].Equals("goal"))
                    tm.timelineType = TimelineType.Goal;
                else if (obj["type"].Equals("own_goal"))
                    tm.timelineType = TimelineType.Own_Goal;
                else if (obj["type"].Equals("pen_shootout_goal"))
                    tm.timelineType = TimelineType.Subst;
                else if (obj["type"].Equals("missed_penalty") || obj["type"].Equals("pen_shootout_miss"))
                    tm.timelineType = TimelineType.Subst;
                else if (obj["type"].Equals("yellowcard"))
                    tm.timelineType = TimelineType.Yellow;
                else if (obj["type"].Equals("redcard"))
                    tm.timelineType = TimelineType.Red;
                else if (obj["type"].Equals("yellowred"))
                    tm.timelineType = TimelineType.YellowRed;
                else if (obj["type"].Equals("substitution"))
                    tm.timelineType = TimelineType.Subst;
                else
                    tm.timelineType = TimelineType.Corner;
                int time = Int32.Parse(Regex.Match(tm.time, @"\d+").Value);
                if (time <= 45)
                    resp[0].Add(tm);
                if (time > 45)
                    resp[1].Add(tm);

            }
            foreach (object evt in Match.corners)
            {
                Dictionary<String, Object> obj = JsonConvert.DeserializeObject<Dictionary<String, Object>>(evt.ToString());
                if (obj == null)
                    continue;
                Timeline tm = new Timeline();
                try
                {
                    if (obj["team_id"].ToString().Equals(Match.localteam_id.ToString()))
                        tm.teamType = TeamType.Home;
                    else
                        tm.teamType = TeamType.Away;
                } catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                    tm.teamType = TeamType.Home;
                }
                if (obj["minute"] != null)
                {
                    tm.time = obj["minute"].ToString();
                    tm.index = Int32.Parse(obj["minute"].ToString());
                }
                if (obj["extra_minute"] != null)
                {
                    tm.time += "+" + obj["extra_minute"].ToString();
                    tm.index = tm.index * 10 + Int32.Parse(obj["extra_minute"].ToString());
                }
                tm.time += "'";

                tm.MainPlayer = (String)obj["comment"];
                tm.timelineType = TimelineType.Corner;

                int time = Int32.Parse(Regex.Match(tm.time, @"\d+").Value);
                int index = 0;
                if (time <= 45)
                    index = 0;
                if (time > 45)
                    index = 1;

                if(tm.MainPlayer.StartsWith("Race to"))
                {
                    for(int k = resp[index].Count - 1; k >= 0; k --)
                    {
                        if(resp[index][k].timelineType == TimelineType.Corner)
                        {
                            resp[index][k].RelatePlayer = "(" + tm.MainPlayer + ")";
                            break;
                        }
                    }
                } 
                else
                    resp[index].Add(tm);

            }
            if (resp[1].Count == 0)
                resp.RemoveAt(1);
            if (resp[0].Count == 0)
                resp.RemoveAt(0);

            foreach(TimelineList list in resp)
            {
                List<Timeline> res = list.OrderBy(o => o.index).ToList();
                list.Clear();
                foreach(Timeline item in res)
                {
                    list.Add(item);
                }
            }
            Timeline = resp;

            //Standing
            List<Statistic> result = new List<Statistic>();
            for(int i = 0; i < order_keys.Length; i ++)
            {
                int home = Match.getStat(order_keys[i], true);
                int away = Match.getStat(order_keys[i], false);

                result.Add(new Statistic
                {
                    type = ((I18N.Current.Locale == "pt") ? portuguese[order_keys[i]] : english[order_keys[i]]),
                    home = home.ToString(),
                    away = away.ToString()
                });
            }
            Statistics = result;

        }

    }
}