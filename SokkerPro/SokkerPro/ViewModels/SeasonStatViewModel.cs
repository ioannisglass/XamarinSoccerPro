using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SokkerPro.ViewModels
{
    public class SeasonStatViewModel : BaseViewModel
    {
        public String _str_cor_Total_Name = string.Empty;
        public string str_cor_Total_Name
        {
            get
            {
                return _str_cor_Total_Name;
            }
            set
            {
                SetProperty(ref _str_cor_Total_Name, value);
            }
        }
        public String _str_cor_Total_MarkPath = string.Empty;
        public string str_cor_Total_MarkPath
        {
            get
            {
                return _str_cor_Total_MarkPath;
            }
            set
            {
                SetProperty(ref _str_cor_Total_MarkPath, value);
            }
        }
        public String _str_cor_Total_Score = string.Empty;
        public string str_cor_Total_Score
        {
            get
            {
                return _str_cor_Total_Score;
            }
            set
            {
                SetProperty(ref _str_cor_Total_Score, value);
            }
        }

        public String _str_cor_Make_Name = string.Empty;
        public string str_cor_Make_Name
        {
            get
            {
                return _str_cor_Make_Name;
            }
            set
            {
                SetProperty(ref _str_cor_Make_Name, value);
            }
        }
        public String _str_cor_Make_MarkPath = string.Empty;
        public string str_cor_Make_MarkPath
        {
            get
            {
                return _str_cor_Make_MarkPath;
            }
            set
            {
                SetProperty(ref _str_cor_Make_MarkPath, value);
            }
        }
        public String _str_cor_Make_Score = string.Empty;
        public string str_cor_Make_Score
        {
            get
            {
                return _str_cor_Make_Score;
            }
            set
            {
                SetProperty(ref _str_cor_Make_Score, value);
            }
        }

        public String _str_cor_Take_Name = string.Empty;
        public string str_cor_Take_Name
        {
            get
            {
                return _str_cor_Take_Name;
            }
            set
            {
                SetProperty(ref _str_cor_Take_Name, value);
            }
        }
        public String _str_cor_Take_MarkPath = string.Empty;
        public string str_cor_Take_MarkPath
        {
            get
            {
                return _str_cor_Take_MarkPath;
            }
            set
            {
                SetProperty(ref _str_cor_Take_MarkPath, value);
            }
        }
        public String _str_cor_Take_Score = string.Empty;
        public string str_cor_Take_Score
        {
            get
            {
                return _str_cor_Take_Score;
            }
            set
            {
                SetProperty(ref _str_cor_Take_Score, value);
            }
        }

        public String _str_goal_Total_Name = string.Empty;
        public string str_goal_Total_Name
        {
            get
            {
                return _str_goal_Total_Name;
            }
            set
            {
                SetProperty(ref _str_goal_Total_Name, value);
            }
        }
        public string _str_goal_Total_MarkPath = string.Empty;
        public string str_goal_Total_MarkPath
        {
            get
            {
                return _str_goal_Total_MarkPath;
            }
            set
            {
                SetProperty(ref _str_goal_Total_MarkPath, value);
            }
        }
        public string _str_goal_Total_Score = string.Empty;
        public string str_goal_Total_Score
        {
            get
            {
                return _str_goal_Total_Score;
            }
            set
            {
                SetProperty(ref _str_goal_Total_Score, value);
            }
        }

        public string _str_goal_Make_Name = string.Empty;
        public string str_goal_Make_Name
        {
            get
            {
                return _str_goal_Make_Name;
            }
            set
            {
                SetProperty(ref _str_goal_Make_Name, value);
            }
        }
        public string _str_goal_Make_MarkPath = string.Empty;
        public string str_goal_Make_MarkPath
        {
            get
            {
                return _str_goal_Make_MarkPath;
            }
            set
            {
                SetProperty(ref _str_goal_Make_MarkPath, value);
            }
        }
        public string _str_goal_Make_Score = string.Empty;
        public string str_goal_Make_Score
        {
            get
            {
                return _str_goal_Make_Score;
            }
            set
            {
                SetProperty(ref _str_goal_Make_Score, value);
            }
        }
        public string _str_goal_Take_Name = string.Empty;
        public string str_goal_Take_Name
        {
            get
            {
                return _str_goal_Take_Name;
            }
            set
            {
                SetProperty(ref _str_goal_Take_Name, value);
            }
        }
        public string _str_goal_Take_MarkPath = string.Empty;
        public string str_goal_Take_MarkPath
        {
            get
            {
                return _str_goal_Take_MarkPath;
            }
            set
            {
                SetProperty(ref _str_goal_Take_MarkPath, value);
            }
        }
        public string _str_goal_Take_Score = string.Empty;
        public string str_goal_Take_Score
        {
            get
            {
                return _str_goal_Take_Score;
            }
            set
            {
                SetProperty(ref _str_goal_Take_Score, value);
            }
        }

        public string _str_yell_Total_Name = string.Empty;
        public string str_yell_Total_Name
        {
            get
            {
                return _str_yell_Total_Name;
            }
            set
            {
                SetProperty(ref _str_yell_Total_Name, value);
            }
        }
        public string _str_yell_Total_MarkPath = string.Empty;
        public string str_yell_Total_MarkPath
        {
            get
            {
                return _str_yell_Total_MarkPath;
            }
            set
            {
                SetProperty(ref _str_yell_Total_MarkPath, value);
            }
        }
        public string _str_yell_Total_Score = string.Empty;
        public string str_yell_Total_Score
        {
            get
            {
                return _str_yell_Total_Score;
            }
            set
            {
                SetProperty(ref _str_yell_Total_Score, value);
            }
        }
        public string _str_yell_Make_Name = string.Empty;
        public string str_yell_Make_Name
        {
            get
            {
                return _str_yell_Make_Name;
            }
            set
            {
                SetProperty(ref _str_yell_Make_Name, value);
            }
        }
        public string _str_yell_Make_MarkPath = string.Empty;
        public string str_yell_Make_MarkPath
        {
            get
            {
                return _str_yell_Make_MarkPath;
            }
            set
            {
                SetProperty(ref _str_yell_Make_MarkPath, value);
            }
        }
        public string _str_yell_Make_Score = string.Empty;
        public string str_yell_Make_Score
        {
            get
            {
                return _str_yell_Make_Score;
            }
            set
            {
                SetProperty(ref _str_yell_Make_Score, value);
            }
        }
        public string _str_yell_Take_Name = string.Empty;
        public string str_yell_Take_Name
        {
            get
            {
                return _str_yell_Take_Name;
            }
            set
            {
                SetProperty(ref _str_yell_Take_Name, value);
            }
        }
        public string _str_yell_Take_MarkPath = string.Empty;
        public string str_yell_Take_MarkPath
        {
            get
            {
                return _str_yell_Take_MarkPath;
            }
            set
            {
                SetProperty(ref _str_yell_Take_MarkPath, value);
            }
        }
        public string _str_yell_Take_Score = string.Empty;
        public string str_yell_Take_Score
        {
            get
            {
                return _str_yell_Take_Score;
            }
            set
            {
                SetProperty(ref _str_yell_Take_Score, value);
            }
        }

        public string _str_poss_Make_Name = string.Empty;
        public string str_poss_Make_Name
        {
            get
            {
                return _str_poss_Make_Name;
            }
            set
            {
                SetProperty(ref _str_poss_Make_Name, value);
            }
        }
        public string _str_poss_Make_MarkPath = string.Empty;
        public string str_poss_Make_MarkPath
        {
            get
            {
                return _str_poss_Make_MarkPath;
            }
            set
            {
                SetProperty(ref _str_poss_Make_MarkPath, value);
            }
        }
        public string _str_poss_Make_Score = string.Empty;
        public string str_poss_Make_Score
        {
            get
            {
                return _str_poss_Make_Score;
            }
            set
            {
                SetProperty(ref _str_poss_Make_Score, value);
            }
        }
        public string _str_poss_Take_Name = string.Empty;
        public string str_poss_Take_Name
        {
            get
            {
                return _str_poss_Take_Name;
            }
            set
            {
                SetProperty(ref _str_poss_Take_Name, value);
            }
        }
        public string _str_poss_Take_MarkPath = string.Empty;
        public string str_poss_Take_MarkPath
        {
            get
            {
                return _str_poss_Take_MarkPath;
            }
            set
            {
                SetProperty(ref _str_poss_Take_MarkPath, value);
            }
        }
        public string _str_poss_Take_Score = string.Empty;
        public string str_poss_Take_Score
        {
            get
            {
                return _str_poss_Take_Score;
            }
            set
            {
                SetProperty(ref _str_poss_Take_Score, value);
            }
        }

        public string _str_foul_Total_Name = string.Empty;
        public string str_foul_Total_Name
        {
            get
            {
                return _str_foul_Total_Name;
            }
            set
            {
                SetProperty(ref _str_foul_Total_Name, value);
            }
        }
        public string _str_foul_Total_MarkPath = string.Empty;
        public string str_foul_Total_MarkPath
        {
            get
            {
                return _str_foul_Total_MarkPath;
            }
            set
            {
                SetProperty(ref _str_foul_Total_MarkPath, value);
            }
        }
        public string _str_foul_Total_Score = string.Empty;
        public string str_foul_Total_Score
        {
            get
            {
                return _str_foul_Total_Score;
            }
            set
            {
                SetProperty(ref _str_foul_Total_Score, value);
            }
        }
        public string _str_foul_Make_Name = string.Empty;
        public string str_foul_Make_Name
        {
            get
            {
                return _str_foul_Make_Name;
            }
            set
            {
                SetProperty(ref _str_foul_Make_Name, value);
            }
        }
        public string _str_foul_Make_MarkPath = string.Empty;
        public string str_foul_Make_MarkPath
        {
            get
            {
                return _str_foul_Make_MarkPath;
            }
            set
            {
                SetProperty(ref _str_foul_Make_MarkPath, value);
            }
        }
        public string _str_foul_Make_Score = string.Empty;
        public string str_foul_Make_Score
        {
            get
            {
                return _str_foul_Make_Score;
            }
            set
            {
                SetProperty(ref _str_foul_Make_Score, value);
            }
        }
        public string _str_foul_Take_Name = string.Empty;
        public string str_foul_Take_Name
        {
            get
            {
                return _str_foul_Take_Name;
            }
            set
            {
                SetProperty(ref _str_foul_Take_Name, value);
            }
        }
        public string _str_foul_Take_MarkPath = string.Empty;
        public string str_foul_Take_MarkPath
        {
            get
            {
                return _str_foul_Take_MarkPath;
            }
            set
            {
                SetProperty(ref _str_foul_Take_MarkPath, value);
            }
        }
        public string _str_foul_Take_Score = string.Empty;
        public string str_foul_Take_Score
        {
            get
            {
                return _str_foul_Take_Score;
            }
            set
            {
                SetProperty(ref _str_foul_Take_Score, value);
            }
        }

        public string _str_country_flag = string.Empty;
        public string str_country_flag
        {
            get
            {
                return _str_country_flag;
            }
            set
            {
                SetProperty(ref _str_country_flag, value);
            }
        }
        public string _str_league_name = string.Empty;
        public string str_league_name
        {
            get
            {
                return _str_league_name;
            }
            set
            {
                SetProperty(ref _str_league_name, value);
            }
        }

        /*public static async Task<SeasonStatViewModel> Create(int? season_id, String countryflag, String leaguename)
        {
            var myClass = new SeasonStatViewModel(season_id, countryflag, leaguename);
            await myClass.Initialize(season_id, countryflag, leaguename);
            return myClass;
        }*/

        /*private async Task Initialize(int? season_id, string countryflag, string leaguename)
        {
            await FillValue(season_id);
            str_country_flag = countryflag;
            str_league_name = leaguename;
        }*/

        public SeasonStatViewModel(int? season_id, String countryflag, String leaguename)
        {
            FillValue(season_id);
            str_country_flag = countryflag;
            str_league_name = leaguename;
        }

        public async Task FillValue(int? _season_id)
        {
            string url = "http://api.sokkerpro.com/new/getSeasonStats/" + _season_id.ToString();

            JsonRes[] meta_files = null;
            bool is_thread_finished = false;
            new Thread((ThreadStart)(() =>
            {
                int retry = 0;
                while (true)
                {
                    try
                    {
                        var w = new WebClient();
                        string json_data = w.DownloadString(url);
                        meta_files = JsonConvert.DeserializeObject<JsonRes[]>(json_data);
                        break;
                    }
                    catch (Exception exception)
                    {
                        if (retry >= 5)
                            break;
                        Thread.Sleep(2000);
                    }
                    retry++;
                }
                is_thread_finished = true;
            })).Start();

            await Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(1000);
                    if (is_thread_finished)
                        break;
                }
            });

            if (meta_files == null || meta_files.Length == 0)
                return;

            if (meta_files.Length == 1)
            {
                InsertJsonToValues(meta_files[0]);
                return;
            }

            List<JsonRes> lst_meta_files = new List<JsonRes>(meta_files);

            lst_meta_files.Sort((x, y) => GetCorTotal(y).CompareTo(GetCorTotal(x)));
            str_cor_Total_Name = lst_meta_files[0].team_name;
            str_cor_Total_MarkPath = lst_meta_files[0].team_logo;
            str_cor_Total_Score = String.Format("{0:00.0}", GetCorTotal(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetCorMake(y).CompareTo(GetCorMake(x)));
            str_cor_Make_Name = lst_meta_files[0].team_name;
            str_cor_Make_MarkPath = lst_meta_files[0].team_logo;
            str_cor_Make_Score = String.Format("{0:00.0}", GetCorMake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetCorTake(y).CompareTo(GetCorTake(x)));
            str_cor_Take_Name = lst_meta_files[0].team_name;
            str_cor_Take_MarkPath = lst_meta_files[0].team_logo;
            str_cor_Take_Score = String.Format("{0:00.0}", GetCorTake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetGoalTotal(y).CompareTo(GetGoalTotal(x)));
            str_goal_Total_Name = lst_meta_files[0].team_name;
            str_goal_Total_MarkPath = lst_meta_files[0].team_logo;
            str_goal_Total_Score = String.Format("{0:00.0}", GetGoalTotal(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetGoalMake(y).CompareTo(GetGoalMake(x)));
            str_goal_Make_Name = lst_meta_files[0].team_name;
            str_goal_Make_MarkPath = lst_meta_files[0].team_logo;
            str_goal_Make_Score = String.Format("{0:00.0}", GetGoalMake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetGoalTake(y).CompareTo(GetGoalTake(x)));
            str_goal_Take_Name = lst_meta_files[0].team_name;
            str_goal_Take_MarkPath = lst_meta_files[0].team_logo;
            str_goal_Take_Score = String.Format("{0:00.0}", GetGoalTake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetYCardTotal(y).CompareTo(GetYCardTotal(x)));
            str_yell_Total_Name = lst_meta_files[0].team_name;
            str_yell_Total_MarkPath = lst_meta_files[0].team_logo;
            str_yell_Total_Score = String.Format("{0:00.0}", GetYCardTotal(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetYCardMake(y).CompareTo(GetYCardMake(x)));
            str_yell_Make_Name = lst_meta_files[0].team_name;
            str_yell_Make_MarkPath = lst_meta_files[0].team_logo;
            str_yell_Make_Score = String.Format("{0:00.0}", GetYCardMake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetYCardTake(y).CompareTo(GetYCardTake(x)));
            str_yell_Take_Name = lst_meta_files[0].team_name;
            str_yell_Take_MarkPath = lst_meta_files[0].team_logo;
            str_yell_Take_Score = String.Format("{0:00.0}", GetYCardTake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetPossMake(y).CompareTo(GetPossMake(x)));
            str_poss_Make_Name = lst_meta_files[0].team_name;
            str_poss_Make_MarkPath = lst_meta_files[0].team_logo;
            str_poss_Make_Score = String.Format("{0:00.0}", GetPossMake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetPossTake(y).CompareTo(GetPossTake(x)));
            str_poss_Take_Name = lst_meta_files[0].team_name;
            str_poss_Take_MarkPath = lst_meta_files[0].team_logo;
            str_poss_Take_Score = String.Format("{0:00.0}", GetPossTake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetFoulTotal(y).CompareTo(GetFoulTotal(x)));
            str_foul_Total_Name = lst_meta_files[0].team_name;
            str_foul_Total_MarkPath = lst_meta_files[0].team_logo;
            str_foul_Total_Score = String.Format("{0:00.0}", GetFoulTotal(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetFoulMake(y).CompareTo(GetFoulMake(x)));
            str_foul_Make_Name = lst_meta_files[0].team_name;
            str_foul_Make_MarkPath = lst_meta_files[0].team_logo;
            str_foul_Make_Score = String.Format("{0:00.0}", GetFoulMake(lst_meta_files[0]));

            lst_meta_files.Sort((x, y) => GetFoulTake(y).CompareTo(GetFoulTake(x)));
            str_foul_Take_Name = lst_meta_files[0].team_name;
            str_foul_Take_MarkPath = lst_meta_files[0].team_logo;
            str_foul_Take_Score = String.Format("{0:00.0}", GetFoulTake(lst_meta_files[0]));
        }

        private void InsertJsonToValues(JsonRes _meta_file)
        {
            str_cor_Total_Name = _meta_file.team_name;
            str_cor_Total_MarkPath = _meta_file.team_logo;
            str_cor_Total_Score = String.Format("{0:00.0}", GetCorTotal(_meta_file));
            str_cor_Make_Name = _meta_file.team_name;
            str_cor_Make_MarkPath = _meta_file.team_logo;
            str_cor_Make_Score = String.Format("{0:00.0}", GetCorMake(_meta_file));
            str_cor_Take_Name = _meta_file.team_name;
            str_cor_Take_MarkPath = _meta_file.team_logo;
            str_cor_Take_Score = String.Format("{0:00.0}", GetCorTake(_meta_file));


            str_goal_Total_Name = _meta_file.team_name;
            str_goal_Total_MarkPath = _meta_file.team_logo;
            str_goal_Total_Score = String.Format("{0:00.0}", GetGoalTotal(_meta_file));
            str_goal_Make_Name = _meta_file.team_name;
            str_goal_Make_MarkPath = _meta_file.team_logo;
            str_goal_Make_Score = String.Format("{0:00.0}", GetGoalMake(_meta_file));
            str_goal_Take_Name = _meta_file.team_name;
            str_goal_Take_MarkPath = _meta_file.team_logo;
            str_goal_Take_Score = String.Format("{0:00.0}", GetGoalTake(_meta_file));

            str_yell_Total_Name = _meta_file.team_name;
            str_yell_Total_MarkPath = _meta_file.team_logo;
            str_yell_Total_Score = String.Format("{0:00.0}", GetYCardTotal(_meta_file));
            str_yell_Make_Name = _meta_file.team_name;
            str_yell_Make_MarkPath = _meta_file.team_logo;
            str_yell_Make_Score = String.Format("{0:00.0}", GetYCardMake(_meta_file));
            str_yell_Take_Name = _meta_file.team_name;
            str_yell_Take_MarkPath = _meta_file.team_logo;
            str_yell_Take_Score = String.Format("{0:00.0}", GetYCardTake(_meta_file));

            str_poss_Make_Name = _meta_file.team_name;
            str_poss_Make_MarkPath = _meta_file.team_logo;
            str_poss_Make_Score = String.Format("{0:00.0}", GetPossMake(_meta_file));
            str_poss_Take_Name = _meta_file.team_name;
            str_poss_Take_MarkPath = _meta_file.team_logo;
            str_poss_Take_Score = String.Format("{0:00.0}", GetPossTake(_meta_file));

            str_foul_Total_Name = _meta_file.team_name;
            str_foul_Total_MarkPath = _meta_file.team_logo;
            str_foul_Total_Score = String.Format("{0:00.0}", GetFoulTotal(_meta_file));
            str_foul_Make_Name = _meta_file.team_name;
            str_foul_Make_MarkPath = _meta_file.team_logo;
            str_foul_Make_Score = String.Format("{0:00.0}", GetFoulMake(_meta_file));
            str_foul_Take_Name = _meta_file.team_name;
            str_foul_Take_MarkPath = _meta_file.team_logo;
            str_foul_Take_Score = String.Format("{0:00.0}", GetFoulTake(_meta_file));
        }


        private double GetCorTotal(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.corner_ht_make);
            ret += double.Parse(_meta_file.corner_ht_take);
            ret += double.Parse(_meta_file.corner_ft_make);
            ret += double.Parse(_meta_file.corner_ft_take);*/

            ret += _meta_file.corner_ht_make;
            ret += _meta_file.corner_ht_take;
            ret += _meta_file.corner_ft_make;
            ret += _meta_file.corner_ft_take;

            return ret;
        }

        private double GetCorMake(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.corner_ht_make);
            ret += double.Parse(_meta_file.corner_ft_make);*/

            ret += _meta_file.corner_ht_make;
            ret += _meta_file.corner_ft_make;

            return ret;
        }

        private double GetCorTake(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.corner_ht_take);
            ret += double.Parse(_meta_file.corner_ft_take);*/

            ret += _meta_file.corner_ht_take;
            ret += _meta_file.corner_ft_take;

            return ret;
        }

        private double GetGoalTotal(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.goal_ht_make);
            ret += double.Parse(_meta_file.goal_ht_take);
            ret += double.Parse(_meta_file.goal_ft_make);
            ret += double.Parse(_meta_file.goal_ft_take);*/

            ret += _meta_file.goal_ht_make;
            ret += _meta_file.goal_ht_take;
            ret += _meta_file.goal_ft_make;
            ret += _meta_file.goal_ft_take;

            return ret;
        }

        private double GetGoalMake(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.goal_ht_make);
            ret += double.Parse(_meta_file.goal_ft_make);*/

            ret += _meta_file.goal_ht_make;
            ret += _meta_file.goal_ft_make;

            return ret;
        }

        private double GetGoalTake(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.goal_ht_take);
            ret += double.Parse(_meta_file.goal_ft_take);*/

            ret += _meta_file.goal_ht_take;
            ret += _meta_file.goal_ft_take;

            return ret;
        }

        private double GetYCardTotal(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.ycard_ht_make);
            ret += double.Parse(_meta_file.ycard_ht_take);
            ret += double.Parse(_meta_file.ycard_ft_make);
            ret += double.Parse(_meta_file.ycard_ft_take);*/

            ret += _meta_file.ycard_ht_make;
            ret += _meta_file.ycard_ht_take;
            ret += _meta_file.ycard_ft_make;
            ret += _meta_file.ycard_ft_take;

            return ret;
        }

        private double GetYCardMake(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.ycard_ht_make);
            ret += double.Parse(_meta_file.ycard_ft_make);*/

            ret += _meta_file.ycard_ht_make;
            ret += _meta_file.ycard_ft_make;

            return ret;
        }

        private double GetYCardTake(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.ycard_ht_take);
            ret += double.Parse(_meta_file.ycard_ft_take);*/

            ret += _meta_file.ycard_ht_take;
            ret += _meta_file.ycard_ft_take;

            return ret;
        }

        private double GetPossMake(JsonRes _meta_file)
        {
            //double ret = double.Parse(_meta_file.posse_make);
            double ret = _meta_file.posse_make;
            return ret;
        }

        private double GetPossTake(JsonRes _meta_file)
        {
            //double ret = double.Parse(_meta_file.posse_take);
            double ret = _meta_file.posse_take;
            return ret;
        }

        private double GetFoulTotal(JsonRes _meta_file)
        {
            double ret = 0;
            /*ret += double.Parse(_meta_file.foul_make);
            ret += double.Parse(_meta_file.foul_take);*/

            ret += _meta_file.foul_make;
            ret += _meta_file.foul_take;

            return ret;
        }

        private double GetFoulMake(JsonRes _meta_file)
        {
            //double ret = double.Parse(_meta_file.foul_make);
            double ret = _meta_file.foul_make;
            return ret;
        }

        private double GetFoulTake(JsonRes _meta_file)
        {
            //double ret = double.Parse(_meta_file.foul_take);
            double ret = _meta_file.foul_take;
            return ret;
        }
    }

    /*public class JsonRes
    {
        public string season_id { get; set; }
        public string round_name { get; set; }
        public string team_id { get; set; }
        public string team_name { get; set; }
        public string team_logo { get; set; }
        public string corner_ht_make { get; set; }
        public string corner_ht_take { get; set; }
        public string corner_ft_make { get; set; }
        public string corner_ft_take { get; set; }
        public string goal_ht_make { get; set; }
        public string goal_ht_take { get; set; }
        public string goal_ft_make { get; set; }
        public string goal_ft_take { get; set; }
        public string ycard_ht_make { get; set; }
        public string ycard_ht_take { get; set; }
        public string ycard_ft_make { get; set; }
        public string ycard_ft_take { get; set; }
        public string foul_make { get; set; }
        public string foul_take { get; set; }
        public string posse_make { get; set; }
        public string posse_take { get; set; }
        public string corner_make { get; set; }
        public string corner_take { get; set; }
        public string goal_make { get; set; }
        public string goal_take { get; set; }
        public string ycard_make { get; set; }
        public string ycard_take { get; set; }
    }*/

    public class JsonRes
    {
        public int season_id { get; set; }
        public string round_name { get; set; }
        public int team_id { get; set; }
        public string team_name { get; set; }
        public string team_logo { get; set; }
        public float corner_ht_make { get; set; }
        public float corner_ht_take { get; set; }
        public float corner_ft_make { get; set; }
        public float corner_ft_take { get; set; }
        public float goal_ht_make { get; set; }
        public float goal_ht_take { get; set; }
        public float goal_ft_make { get; set; }
        public float goal_ft_take { get; set; }
        public float ycard_ht_make { get; set; }
        public float ycard_ht_take { get; set; }
        public float ycard_ft_make { get; set; }
        public float ycard_ft_take { get; set; }
        public float foul_make { get; set; }
        public float foul_take { get; set; }
        public float posse_make { get; set; }
        public float posse_take { get; set; }
        public float corner_make { get; set; }
        public float corner_take { get; set; }
        public float goal_make { get; set; }
        public float goal_take { get; set; }
        public float ycard_make { get; set; }
        public float ycard_take { get; set; }
    }
}
