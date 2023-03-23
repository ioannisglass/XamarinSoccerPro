using System;
using System.Collections.Generic;
using System.Text;

namespace SokkerPro.Models
{
    public class Tip
    {
        public int id { get; set; }
        public string content { get; set; }
        public DateTime uploaddate {get; set;}
        public int type { get; set; }

        public string date
        {
            get
            {
                return uploaddate.ToString("dd MMM | HH:mm");
            }
        }
    }
}
