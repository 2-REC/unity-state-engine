using System.Collections.Generic;

[System.Serializable]
public class GameSaveData {
//    public GameSaveDataItem[] items;
    public List<GameSaveDataItem> items;

//?
    public GameSaveData() {
        items = new List<GameSaveDataItem>();
    }

}

[System.Serializable]
public class GameSaveDataItem {
    public string key;
    public int value;

    public GameSaveDataItem(string key, int value) {
        this.key = key;
        this.value = value;
    }

}
