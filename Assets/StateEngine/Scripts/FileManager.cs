using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class FileManager {

    public static void Save(string filename, Dictionary<string, int> fields) {
        string path = Path.Combine(Application.persistentDataPath, filename) + ".json";
        Debug.Log("SAVE PATH: " + path);
        if (!string.IsNullOrEmpty(path)) {

            SaveData savedData = new SaveData();
            foreach (KeyValuePair<string, int> item in fields) {
                Debug.Log("SAVE PAIR: " + item.Key + ", " + item.Value);
                SaveDataItem dataItem = new SaveDataItem(item.Key, item.Value);
                savedData.items.Add(dataItem);
            }

            string jsonData = JsonUtility.ToJson(savedData);
            File.WriteAllText(path, jsonData);
        }
    }

    public static bool Load(string filename, Dictionary<string, int> fields) {
        string path = Path.Combine(Application.persistentDataPath, filename) + ".json";
        Debug.Log("LOAD PATH: " + path);
        if (File.Exists(path)) {
            string jsonData = File.ReadAllText(path);
            SaveData loadedData = JsonUtility.FromJson<SaveData>(jsonData);

            for (int i = 0; i < loadedData.items.Count; ++i) {
                fields.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            ////////
            foreach (KeyValuePair<string, int> item in fields) {
                Debug.Log("LOAD PAIR: " + item.Key + ", " + item.Value);
            }
            ////////
        } else {
            Debug.LogWarning("Cannot find file \" " + path + "\"!");
            return false;
        }

        return true;
    }

    public static bool Exists(string filename) {
        string path = Path.Combine(Application.persistentDataPath, filename) + ".json";
        return File.Exists(path);
    }

    public static void Delete(string filename) {
        string path = Path.Combine(Application.persistentDataPath, filename) + ".json";
        File.Delete(path);
    }

}
