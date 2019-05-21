using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class FileManager {

    public static void Save(string filename, Dictionary<string, int> fields) {

        string path = Path.Combine(Application.streamingAssetsPath, filename) + ".json";
Debug.Log ("SAVE PATH: " + path);
        if (!string.IsNullOrEmpty(path)) {

            GameSaveData savedData = new GameSaveData();
            foreach (KeyValuePair<string, int> item in fields) {
Debug.Log ("PAIR: " + item.Key + ", " + item.Value);
                GameSaveDataItem dataItem = new GameSaveDataItem(item.Key, item.Value);
                savedData.items.Add(dataItem);
            }

            string jsonData = JsonUtility.ToJson(savedData);
            File.WriteAllText(path, jsonData);
        }
    }

    public static bool Load(string filename, Dictionary<string, int> fields) {

        string path = Path.Combine(Application.streamingAssetsPath, filename) + ".json";
        if (File.Exists(path)) {
            string jsonData = File.ReadAllText(path);
            GameSaveData loadedData = JsonUtility.FromJson<GameSaveData>(jsonData);

            for (int i = 0; i < loadedData.items.Count; ++i) {
                fields.Add(loadedData.items[i].key, loadedData.items[i].value);   
            }

////////
Debug.Log ("Data loaded, dictionary contains: " + fields.Count + " entries");
foreach (KeyValuePair<string, int> item in fields) {
    Debug.Log ("PAIR: " + item.Key + ", " + item.Value);
}
////////
        } else {
            Debug.LogError ("Cannot find file \" " + path + "\"!");
            return false;
        }

        return true;
    }

    public static void Delete(string filename) {
        string path = Path.Combine(Application.streamingAssetsPath, filename) + ".json";
        File.Delete(path);
    }

}
