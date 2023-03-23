using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.Models
{
    public class OnePrediction
    {
        public string label { get; set; }
        public string value { get; set; }
        public double percent;
        public Xamarin.Forms.Rectangle width
        {
            get
            {
                return new Xamarin.Forms.Rectangle(0, 0, percent, 1);
            }
        }
        public Xamarin.Forms.Color color
        {
            get
            {
                if (percent < 0.5)
                    return Xamarin.Forms.Color.Red;
                return Xamarin.Forms.Color.Green;
            }
        }
    }
    public class Prediction
    {
        public string name { get; set; }
        public List<OnePrediction> items { get; set; } = new List<OnePrediction>();

    }
}
