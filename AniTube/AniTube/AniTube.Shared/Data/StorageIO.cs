using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Storage;

namespace AniTube.Data
{
    public class StorageIO
    {
        public static async Task SaveToLocalFolder(string data, string filename)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, data);
        }

        public static async Task<T> ReadObjectFromXmlFileAsync<T>(string filename)
        {
            T objectFromXml = default(T);
            var serializer = new XmlSerializer(typeof(T));
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.GetFileAsync(filename);
            Stream stream = await file.OpenStreamForReadAsync();
            objectFromXml = (T)serializer.Deserialize(stream);
            stream.Dispose();
            return objectFromXml;
        }

        public static async Task SaveObjectToXmlAsync<T>(T objectToSave, string filename)
        {
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
                Stream stream = await file.OpenStreamForWriteAsync();

                using (stream)
                {
                    serializer.Serialize(stream, objectToSave);
                }
                stream.Dispose();
            }
            catch(Exception e)
            { 
            }
        }
    }
}
