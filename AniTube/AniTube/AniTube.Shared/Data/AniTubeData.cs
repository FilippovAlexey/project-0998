using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AniTube.Models;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Serialization;
using Windows.Storage;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using Windows.UI.Xaml.Controls;

namespace AniTube.Data
{
    public class AniTubeData
    {
        public static ObservableCollection<Playlist> playListCollection;
        public static Dictionary<string, ObservableCollection<Film>> filmDict; 
        private const string filmLocalDataPath = "FilmData.xml";
        private const string playlistLocalDataPath = "PlaylistData.xml";
        private const string filmDictLocalDataPath = "FilmDictData.xml";


        public static async void LoadLocalData()
        {
            playListCollection = await StorageIO.ReadObjectFromXmlFileAsync<ObservableCollection<Playlist>>(playlistLocalDataPath);
        }

        public static async void SaveLocalData()
        {
           await SavePlaylist(filmDictLocalDataPath);
        }

        public static async Task<ObservableCollection<Playlist>> LoadData()
        {
            var storageFolder = ApplicationData.Current.LocalFolder;
            var file = await storageFolder.GetFileAsync("FilmData.json");
            ObservableCollection<Playlist> temp = new ObservableCollection<Playlist>();
            ObservableCollection<Playlist> temp2 = new ObservableCollection<Playlist>();
            var storageFolder2 = ApplicationData.Current.LocalFolder;
            string fileContent2;
            var file2 = await storageFolder2.GetFileAsync("PlaylistData.json");
            using (StreamReader sRead2 = new StreamReader(await file2.OpenStreamForReadAsync()))
                fileContent2 = sRead2.ReadToEnd();
            temp = JsonConvert.DeserializeObject<ObservableCollection<Playlist>>(fileContent2);
            foreach(Playlist p in temp)
            {
                //temp2.Add(new Playlist(p.id, p.genres, p.otherNames, p.isFavorite, p.publishedDate, p.popularityIndex, p.imagePath, p.description, p.title));
            }
            return temp;
        }

        //public static  ObservableCollection<Film> LoadFilmList(string id)
        //{
        //    //ObservableCollection<Film> temp = new ObservableCollection<Film>();      
        //    //ObservableCollection<Film> tempReturn = new ObservableCollection<Film>();
        //    //string fileContent;
        //    //StorageFile file =  ApplicationData.Current.LocalFolder.GetFileAsync("FilmData.json").AsTask().Result;
        //    //fileContent = Windows.Storage.FileIO.ReadTextAsync(file).AsTask().Result;
        //    //temp = JsonConvert.DeserializeObject<ObservableCollection<Film>>(fileContent);
        //    //foreach (Film f in temp)
        //    //{
        //    //    if (f.playlistId == id)
        //    //    {

        //    //        tempReturn.Add(f);
        //    //    }
        //    //}
        //   // playListCollection[playListCollection.IndexOf()]
        //    return tempReturn;
        //}

        public static void SetFavorites(Playlist pl)
        {
            int index = playListCollection.IndexOf(pl);
            if (playListCollection[index] != null)
            {
                if (playListCollection[index].IsFavorite)
                {
                    playListCollection[index].IsFavorite = false;
                }
                else
                {
                    playListCollection[index].IsFavorite = true;
                }
            }
        }

        public static void SetLastPlayed(Film film, ObservableCollection<Film> filmListCollection)
        {
            filmListCollection[filmListCollection.IndexOf(film)].LastPlayed = DateTime.Now;
        }

        public static void SetSeekPosition(Film film, TimeSpan position, ObservableCollection<Film> filmListCollection)
        {
            filmListCollection[filmListCollection.IndexOf(film)].SeekPosition = position;
        }

        public static void SaveFilmList(Playlist playlist, ObservableCollection<Film> filmList)
        {
            Playlist temp = playListCollection[playListCollection.IndexOf(playlist)];
            temp.filmsList = filmList;
        }

        public static async Task SavePlaylist(string filePath)
        {
            await StorageIO.SaveObjectToXmlAsync<ObservableCollection<Playlist>>(playListCollection, playlistLocalDataPath);
        }

        public static Film SelectLastShowedFilm(ObservableCollection<Film> filmlist)
        {
            Film max = filmlist[0];
            foreach(Film f in filmlist)
            {
                if(f.LastPlayed>max.LastPlayed)
                {
                    max = f;
                }
            }
            return max;
        }

        public static ObservableCollection<Playlist> Filtrate(string keyWord, List<CheckBox> genresList, string category)
        {
            int counter = 0;
            ObservableCollection<Playlist> temp = new ObservableCollection<Playlist>();
            if (keyWord == null)
                keyWord = "";
            foreach (Playlist pl in AniTubeData.playListCollection)
            {
                if (pl.title.ToLower().Contains(keyWord.ToLower()) || pl.otherNames.ToLower().Contains(keyWord.ToLower()))
                {
                    foreach (CheckBox cb in genresList)
                    {
                        if(cb.IsChecked == true)
                        {
                            counter++;
                        }
                    }

                    if(counter == 0)
                    {
                        if (CheckCategory(pl, category))
                        {
                            temp.Add(pl);
                        }
                    }

                    else
                    {
                        foreach(CheckBox cb in genresList)
                        {
                            if(cb.IsChecked == true && pl.genres.Contains(cb.Content.ToString()))
                            {
                                if (CheckCategory(pl, category))
                                {
                                    temp.Add(pl);
                                }
                            }
                        }
                    }
                }
            }
            return temp;
        }

        private static bool CheckCategory(Playlist pl, string category)
        {
            bool result = false;
            if (category == "All")
            {
                result = true;
            }
            else if (category == "Dubbed")
            {
                if (pl.title.Contains("(Dub)"))
                {
                    result = true;
                }
            }
            else if (category == "Subbed")
            {
                if (pl.title.Contains("(Sub)"))
                {
                    result = true;
                }
            }
            else if (category == "Most popular")
            {
                if (pl.popularityIndex >= 1000)
                {
                    result = true;
                }
            }
            else if (category == "Recently updated")
            {
                if (pl.publishedDate >= DateTime.Now.AddDays(-7))
                {
                    result = true;
                }
            }
            else if (category == "Favorites")
            {
                result = pl.IsFavorite;
            }
            else result = false;
            return result;
        }
    }
}
