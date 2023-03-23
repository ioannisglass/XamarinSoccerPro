using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.Models
{
    public class Statistic
    {
        public string type { get; set; }
        public string home { get; set; }
        public string away { get; set; }

        public Xamarin.Forms.Rectangle home_width
        {
            get
            {
                int h = Int32.Parse(home.TrimEnd(new char[] { '%', ' ' }));
                int a = Int32.Parse(away.TrimEnd(new char[] { '%', ' ' }));
                if (h + a == 0)
                    return new Xamarin.Forms.Rectangle();
                return new Xamarin.Forms.Rectangle(1, 0, h * 1.0 / (h + a), 1);
            }
        }
        public Xamarin.Forms.Color home_color
        {
            get
            {
                int h = Int32.Parse(home.TrimEnd(new char[] { '%', ' ' }));
                int a = Int32.Parse(away.TrimEnd(new char[] { '%', ' ' }));
                if (h < a)
                    return new Xamarin.Forms.Color(0.18, 0.18, 0.18);
                else
                    //return new Xamarin.Forms.Color(0.9, 0.27, 0.27);
                    return new Xamarin.Forms.Color(0, 0.8, 0.6);
            }
        }
        public Xamarin.Forms.Rectangle away_width
        {
            get
            {
                int h = Int32.Parse(home.TrimEnd(new char[] { '%', ' ' }));
                int a = Int32.Parse(away.TrimEnd(new char[] { '%', ' ' }));
                if (h + a == 0)
                    return new Xamarin.Forms.Rectangle();
                return new Xamarin.Forms.Rectangle(0, 0, a * 1.0 / (h + a), 1);
            }
        }
        public Xamarin.Forms.Color away_color
        {
            get
            {
                int h = Int32.Parse(home.TrimEnd(new char[] { '%', ' ' }));
                int a = Int32.Parse(away.TrimEnd(new char[] { '%', ' ' }));
                if (a < h)
                    return new Xamarin.Forms.Color(0.18, 0.18, 0.18);
                else
                    return new Xamarin.Forms.Color(0, 0.8, 0.6);
            }
        }
    }
}
