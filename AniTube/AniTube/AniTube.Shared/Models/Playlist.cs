using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Xml.Serialization;

namespace AniTube.Models
{
    public class Playlist : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string genres { get; set; }
        public string otherNames { get; set; }
        public bool isFavorite;
        public DateTime publishedDate { get; set; }
        public int popularityIndex { get; set; }
        public string imagePath { get; set; }
        public string description { get; set; }
        public string title { get; set; }
        public ObservableCollection<Film> filmsList { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public Playlist()
        {

        }

        //public Playlist(string id, string genres, string otherNames, bool isFavorite, DateTime publishedDate, int popularityIndex, string imagePath, string description, string title)
        //{
        //    this.id = id;
        //    this.genres = genres;
        //    this.otherNames = otherNames;
        //    this.isFavorite = isFavorite;
        //    this.publishedDate = publishedDate;
        //    this.popularityIndex = popularityIndex;
        //    this.imagePath = imagePath;
        //    this.description = description;
        //    this.title = title;
        //}

        public bool IsFavorite
        {
            get
            {
                return isFavorite;
            }
            set
            {
                isFavorite = value;
                NotifyPropertyChanged("IsFavoriteVisibility");
            }
        }

        public string IsFavoriteVisibility
        {
            get
            {
                if(isFavorite)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
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
