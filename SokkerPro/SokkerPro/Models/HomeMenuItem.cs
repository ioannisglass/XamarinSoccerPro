using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace SokkerPro.Models
{
    public class HomeMenuItem : ObservableCollection<Fixture>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int? SeasonId { get; set; } = 0;
        public string CountryName { get; set; }
        public String CountryFlag { get; set; }
        public int League_Id { get; set; } = 0;
        public string LeagueName { get; set; }

        public bool _isExpanded;
        public bool IsExpanded
        {
            get
            {
                return _isExpanded;
            }
            set
            {
                _isExpanded = value;
                OnPropertyChanged("ActionImage");

            }
        }
        public string ActionImage
        {
            get
            {
                if (IsExpanded)
                    return "collapse.png";
                return "expand.png";
            }
        }

        public Command ExpandCommand { get; set; }
        public Command SeasonStatCommand { get; set; }

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        public int liveCnt;
        public int LiveCnt
        {
            get
            {
                return liveCnt;
            }
            set
            {
                liveCnt = value;
                OnPropertyChanged("LiveCnt");
            }
        }
        public int TotalCnt { get; set; }

        public ObservableCollection<Fixture> resource = new ObservableCollection<Fixture>();

        public ObservableCollection<Fixture> matches => this;

    }
}
