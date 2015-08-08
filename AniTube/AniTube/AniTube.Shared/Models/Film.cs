using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace AniTube.Models
{
    public class Film : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string playlistId { get; set; }
        public DateTime publishedDate { get; set; }
        private TimeSpan _seekPosition;
        private DateTime lastPlayed;
        public string episodeSdUrl { get; set; }
        public string episodeHdUrl { get; set; }
        private double progressPercent;
        public string imagePath { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public string count { get; set; }
        public string title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime LastPlayed
        {
            get
            {
                return lastPlayed;
            }
            set
            {
                lastPlayed = value;
                NotifyPropertyChanged("LastPlayed");
            }
        }

        public double ProgressPercent
        {
            get
            {
                return progressPercent;
            }
            set
            {
                progressPercent = value;
                NotifyPropertyChanged("ProgressPercent");
            }

        }

        public TimeSpan SeekPosition
        {
            get
            {
                return _seekPosition;
            }
            set
            {
                _seekPosition = value;
                NotifyPropertyChanged("SeekPosition");
            }
        }


        private void NotifyPropertyChanged(string updateFavoriteVisibility)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(updateFavoriteVisibility));
            }
        }
    }
}
