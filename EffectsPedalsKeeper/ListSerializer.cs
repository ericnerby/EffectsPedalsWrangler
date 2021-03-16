using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace EffectsPedalsKeeper
{
    public static class ListSerializer
    {
        public static JsonSerializerSettings JsonOptions = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented
        };

        public static void SerializeList<T>(string fileName, List<T> source)
        {
            using (StreamWriter file = File.CreateText(@fileName))
            {
                JsonSerializer serializer = JsonSerializer.Create(JsonOptions);
                serializer.Serialize(file, source);
            }
        }

        public static bool DeserializeList<T>(string fileName, List<T> destination)
        {
            if (File.Exists(fileName))
            {
                using (StreamReader file = File.OpenText(@fileName))
                {
                    JsonSerializer serializer = JsonSerializer.Create(JsonOptions);
                    var itemsToAdd = (List<T>)serializer.Deserialize(file, typeof(List<T>));
                    destination.AddRange(itemsToAdd);
                }
                return true;
            }
            return false;
        }
    }
}
