using System.Collections.Generic;

namespace StateEngine {

    [System.Serializable]
    public class SaveData {
        public List<SaveDataItem> items;

        //?
        public SaveData() {
            items = new List<SaveDataItem>();
        }

    }

    [System.Serializable]
    public class SaveDataItem {
        public string key;
        public int value;

        public SaveDataItem(string key, int value) {
            this.key = key;
            this.value = value;
        }

    }

}
