using System.IO;
using UnityEngine;

namespace Larva.Menu.Tools
{
    public static class JSONDataLoadSaver<T>
    {
        public static T Load(string path)
        {
            string fullPath = Application.streamingAssetsPath + path;

            T result;

            if (!File.Exists(fullPath))
            {
                string FileJSON = JsonUtility.ToJson("");
                File.WriteAllText(fullPath, FileJSON);
            }

            string tempJSON = File.ReadAllText(fullPath);
            result = JsonUtility.FromJson<T>(tempJSON);

            return result;
        }
        public static void SaveData(T data, string path)
        {
            string fullPath = Application.streamingAssetsPath + path;
            string FileJSON = JsonUtility.ToJson(data);
            File.WriteAllText(fullPath, FileJSON);
        }
    }
}