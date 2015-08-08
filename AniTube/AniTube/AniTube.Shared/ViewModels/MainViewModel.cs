using AniTube.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using AniTube.Data;
using Windows.Storage;
using Newtonsoft.Json;
using System.IO;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Windows.UI.Xaml.Controls;

namespace AniTube.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        private Playlist plSelected;
        private INavigationService navigationService;
        private List<CheckBox> genresList;
        public List<CheckBox> GenresList
        {
            get
            {
                return genresList;
            }
            set
            {
                genresList = value;
                RaisePropertyChanged(() => GenresList);
            }
        }

        public RelayCommand<Playlist> ItemSelectedCommand { get; set; }
        public RelayCommand ButtonHDCommand { get; set; }
        public RelayCommand ButtonSDCommand { get; set; }
        public RelayCommand ButtonSetFavorite { get; set; }
        public RelayCommand FilterCommand { get; set; }
        public RelayCommand GenresPopupCommand { get; set; }

        private ListViewItem categorySelectedItem;
        public ListViewItem CategorySelectedItem
        {
            get
            {
                return categorySelectedItem;
            }
            set
            {
                categorySelectedItem = value;
                RaisePropertyChanged(() => CategorySelectedItem);
            }
        }

        private string keyWord;
        public string KeyWord
        {
            get
            {
                return keyWord;
            }
            set
            {
                keyWord = value;
            }
        }

        private ObservableCollection<Playlist> showedPl;
        public ObservableCollection<Playlist> PlCollection
        {
            get
            {
                return showedPl;
            }
            set
            {
                showedPl = value;
                RaisePropertyChanged(() => PlCollection);
            }
        }

        private string toFavoritesVisibility;
        public string ToFavoritesVisibility
        {
            get
            {
                return toFavoritesVisibility;
            }
            set
            {
                toFavoritesVisibility = value;
                RaisePropertyChanged(() => ToFavoritesVisibility);
            }
        }

        private string outFromFavoritesVisibility;
        public string OutFromFavoritesVisibility
        {
            get
            {
                return outFromFavoritesVisibility;
            }
            set
            {
                outFromFavoritesVisibility = value;
                RaisePropertyChanged(() => OutFromFavoritesVisibility);
            }
        }

        public string Title
        {
            get { return plSelected.title; }
            set
            {
                plSelected.title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        public string ImagePath
        {
            get { return plSelected.imagePath; }
            set
            {
                plSelected.imagePath = value;
                RaisePropertyChanged(() => ImagePath);
            }
        }

        public string OtherNames
        {
            get { return plSelected.otherNames; }
            set
            {
                plSelected.otherNames = "Other names: " + value;
                RaisePropertyChanged(() => OtherNames);
            }
        }

        public string Description
        {
            get { return plSelected.description; }
            set
            {
                plSelected.description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private bool _isOpen;
        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                if (_isOpen == value) return;
                _isOpen = value;
                RaisePropertyChanged("IsOpen");
            }
        }

        private bool _genresIsOpen;
        public bool GenresIsOpen
        {
            get { return _genresIsOpen; }
            set
            {
                _genresIsOpen = value;
                RaisePropertyChanged("GenresIsOpen");
            }
        }



        public MainViewModel()
        {

            plSelected = new Playlist();
            PlCollection = AniTubeData.playListCollection;
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();
            SetGenres();

            GenresPopupCommand = new RelayCommand(() =>
            {
                GenresIsOpen = true;
            });

            FilterCommand = new RelayCommand(() =>
            {
                PlCollection = AniTubeData.Filtrate(KeyWord, GenresList, CategorySelectedItem.Content.ToString());
            });

            ItemSelectedCommand = new RelayCommand<Playlist>((playlist) =>
            {
                plSelected = playlist;
                SetPopupValue(playlist);
                ButtonFavoritesVisibility(playlist.isFavorite);
                IsOpen = true;
            });

            ButtonSetFavorite = new RelayCommand(() =>
            {
                AniTubeData.SetFavorites(plSelected);
                if (plSelected.isFavorite)
                {
                    ButtonFavoritesVisibility(true);
                }
                else
                {
                    ButtonFavoritesVisibility(false);
                }
            });

            ButtonHDCommand = new RelayCommand(() =>
            {
                navigationService.Navigate(typeof(PlaylistView));
                Messenger.Default.Send(true, "videoQuality");
                Messenger.Default.Send(plSelected, "plToken");
                IsOpen = false;
            });
            ButtonSDCommand = new RelayCommand(() =>
            {
                navigationService.Navigate(typeof(PlaylistView));
                Messenger.Default.Send(false, "videoQuality");
                Messenger.Default.Send(plSelected, "plToken");
                IsOpen = false;
            });
        }

        private void ButtonFavoritesVisibility(bool value)
        {
            if (value)
            {
                ToFavoritesVisibility = "Collapsed";
                OutFromFavoritesVisibility = "Visible";
            }
            else
            {
                ToFavoritesVisibility = "Visible";
                OutFromFavoritesVisibility = "Collapsed";
            }
        }

        public void Saving(Playlist plt)
        {
            plSelected = plt;
        }


        private void SetPopupValue(Playlist pl)
        {
            Title = pl.title;
            ImagePath = pl.imagePath;
            OtherNames = pl.otherNames;
            Description = pl.description;
        }

        private void SetGenres()
        {
            GenresList = new List<CheckBox>();
            CheckBox cb = new CheckBox();
            cb.Content = "Action";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Adventure";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Cars";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Cartoon";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Comedy";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Dementia";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Demons";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Drama";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Ecchi";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Fantasy";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Game";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Harlem";
            GenresList.Add(cb);

            cb = new CheckBox();
            cb.Content = "Historical";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Horror";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Josei";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Kids";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Magic";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "MartialArts";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Mecha";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Military";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Movie";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Music";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Mystery";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "ONA";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "OVA";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Parody";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Police";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Psychological";
            GenresList.Add(cb);

            cb = new CheckBox();
            cb.Content = "Romance";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Samurai";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "School";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "SciFi";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Seinen";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Shoujo";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "ShoujoAi";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Shounen";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "ShounenAi";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "SliceOfLife";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Space";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Special";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Sports";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Supernatural";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Superpower";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Thriller";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Vampire";
            GenresList.Add(cb);
            cb = new CheckBox();
            cb.Content = "Yuri";
            GenresList.Add(cb);
        }
    }
}
