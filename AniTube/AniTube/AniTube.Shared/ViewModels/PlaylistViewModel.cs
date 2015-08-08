using AniTube.Data;
using AniTube.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AniTube.ViewModels
{
    public class PlaylistViewModel : ViewModelBase
    {
        private INavigationService navigationService;
        private ObservableCollection<Film> _filmList;
        public ObservableCollection<Film> FilmList
        {
            get
            {
                return _filmList;
            }
            set
            {
                _filmList = value;
            }
        }
        public RelayCommand<Film> ItemSelectedCommand { get; set; }
        public RelayCommand ButtonSetFavorite { get; set; }
        public RelayCommand IsFullScreenCommand { get; set; }
        public RelayCommand PositionChangedCommand { get; set; }
        public RelayCommand BackButtonCommand { get; set; }
       // public RelayCommand PalyerMediaOpenedCommand { get; set; }

        private Film selectedFilm { get; set; }
        public Film SelectedFilm
        {
            get
            {
                return selectedFilm;
            }
            set
            {
                selectedFilm = value;
                RaisePropertyChanged(()=> SelectedFilm);
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

        private bool isHD;
        public bool IsHD
        {
            get
            {
                return isHD;
            }
            set
            {
                isHD = value;
                RaisePropertyChanged(() => PlayerMediaQuality);
            }
        }

        private string _videoSource;
        public string VideoSource
        {
            get
            {
                return _videoSource;
            }
            set
            {
                _videoSource = value;
                RaisePropertyChanged(() => VideoSource);
            }
        }

        private Playlist pl;
        public Playlist PlSelected
        {
            get
            {
                return pl;
            }
            set
            {
                pl = value;
            }
        }

        private bool playerFullScreen;
        public bool PlayerFullScren
        {
            get
            {
                return playerFullScreen;
            }
            set
            {
                playerFullScreen = value;
            }
        }

        private int playerRow = 1;
        public int PlayerRow
        {
            get
            {
                return playerRow;
            }
            set
            {
                playerRow = value;
                RaisePropertyChanged(() => PlayerRow);
            }
        }

        private int playerColumn = 1;
        public int PlayerColumn
        {
            get
            {
                return playerColumn;
            }
            set
            {
                playerColumn = value;
                RaisePropertyChanged(() => PlayerColumn);
            }
        }

        public string Title
        {
            get
            {
                return PlSelected.title;
            }
        }

        private TimeSpan playerPosition;
        public TimeSpan PlayerPosition
        {
            get
            {
                return playerPosition;
            }
            set
            {
                playerPosition = value;
                RaisePropertyChanged(() => PlayerPosition);
            }
        }

        private TimeSpan playerEndTime;
        public TimeSpan PlayerEndTime
        {
            get
            {
                return playerEndTime;
            }
            set
            {
                playerEndTime = value;
                RaisePropertyChanged(() => PlayerEndTime);
            }
        }

        private TimeSpan playerStartupPosition;
        public TimeSpan PlayerStartupPosition
        {
            get
            {
                return playerStartupPosition;
            }
            set
            {
                playerStartupPosition = value;
                RaisePropertyChanged(() => PlayerStartupPosition);
            }
        }

        public string PlayerMediaQuality
        {
            get
            {
                if (IsHD)
                {
                    return "HighDefinition";
                }
                else
                {
                    return "StandardDefinition";
                }
            }
        }

        public PlaylistViewModel()
        {
            navigationService = SimpleIoc.Default.GetInstance<INavigationService>();

            //PalyerMediaOpenedCommand = new RelayCommand(() =>
            //{
            //    int i = 5;
            //});

            BackButtonCommand = new RelayCommand(() =>
            {
                AniTubeData.SetSeekPosition(SelectedFilm, PlayerPosition, FilmList);
                AniTubeData.SaveFilmList(PlSelected, FilmList);
                navigationService.GoBack();
            });

            PositionChangedCommand = new RelayCommand(() =>
            {
                FilmList[FilmList.IndexOf(SelectedFilm)].ProgressPercent = Math.Round((PlayerPosition.TotalSeconds)
                    / Convert.ToSingle(PlayerEndTime.TotalSeconds) * 100, 2);
            });

            IsFullScreenCommand = new RelayCommand(() =>
            {
                if (PlayerFullScren)
                {
                    PlayerFullScren = false;
                }
                else
                {
                    PlayerFullScren = true;
                }
                SetPlayerFullScreen();
            });

            ButtonSetFavorite = new RelayCommand(() =>
            {
                AniTubeData.SetFavorites(PlSelected);
                ButtonFavoritesVisibility(PlSelected.isFavorite);
            });

            Messenger.Default.Register<bool>(this, "videoQuality", (message) =>
            {
                IsHD = message;
            });

            Messenger.Default.Register<Playlist>(this, "plToken", (message) =>
                {
                    PlSelected = message;
                    LoadPageData();
                });
            ItemSelectedCommand = new RelayCommand<Film>((film) =>
            {
                AniTubeData.SetSeekPosition(SelectedFilm, PlayerPosition, FilmList);
                SelectedFilm = film;
                SetVidoSource();
                AniTubeData.SetLastPlayed(SelectedFilm, FilmList);
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

        private void SetPlayerFullScreen()
        {
            if (PlayerFullScren)
            {
                PlayerRow = 0;
                PlayerColumn = 0;
            }
            else
            {
                PlayerColumn = 1;
                PlayerRow = 1;
            }
        }

        private void LoadPageData()
        {
            FilmList = PlSelected.filmsList;
            SelectedFilm = AniTubeData.SelectLastShowedFilm(FilmList);
            SetVidoSource();
            ButtonFavoritesVisibility(PlSelected.isFavorite);
        }

        private void SetVidoSource()
        {
            PlayerStartupPosition = new TimeSpan(0, 0,59);
            if (IsHD)
            {
                VideoSource = SelectedFilm.episodeHdUrl;
            }
            else
            {
                VideoSource = SelectedFilm.episodeSdUrl;
            }
            
        }
    }
}
