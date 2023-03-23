using I18NPortable;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SokkerPro.Models
{
    public class LiveAlert
    {
        public int country_id { get; set; }
        public string league_name { get; set; }
        public int fixture_id { get; set; }
        public int game_time { get; set; }
        public int alert_type { get; set; }
        public string title { get; set; }
        public string english_content { get; set; }
        public string portuguese_content { get; set; }
        public DateTime created_at { get; set; }
        public string desc
        {
            get
            {
                if (I18N.Current.Locale == "pt")
                    return portuguese_content;
                return english_content;
            }
        }

        public string TypeStr
        {
            get
            {
                if (I18N.Current.Locale == "pt")
                {
                    if (alert_type == 1)
                        return "ALERTA DE GOLS ";
                    if (alert_type == 2)
                        return "MOMENTO DO JOGO";
                    if (alert_type == 3)
                        return "CANTOS";
                    return "";
                }
                if (alert_type == 1)
                    return "GOL ALERT";
                if (alert_type == 2)
                    return "GAME MOMENT";
                if (alert_type == 3)
                    return "CORNER";
                return "";
            }
        }

        public string CountryFlag
        {
            get
            {
                return App.BACKEND_URL + "/assets/flags/" + country_id + ".png";
            }
        }

        public string Time
        {
            get
            {

                if (I18N.Current.Locale == "pt")
                {
                    if (game_time <= 45)
                        return "1º Parte " + game_time + "'";
                    return "2º Parte " + game_time + "'";
                }
                if (game_time <= 45)
                    return "1º Half " + game_time + "'";
                return "2º Half " + game_time + "'";
            }
        }

        public string TimeAgo
        {
            get
            {
                TimeSpan span = DateTime.UtcNow - created_at;
                span = span.Subtract(new TimeSpan(2, 0, 0));
                if (span.TotalMinutes < 1)
                    return Math.Floor(span.TotalSeconds) + " Seconds Ago";
                if (span.TotalHours < 1)
                    return Math.Floor(span.TotalMinutes) + " Minutes Ago";
                if (span.TotalDays < 1)
                    return Math.Floor(span.TotalHours) + " Hours Ago";
                return Math.Floor(span.TotalDays) + " Days Ago";
            }
        }
    }
}
