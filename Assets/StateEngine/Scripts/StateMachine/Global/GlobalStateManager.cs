using UnityEngine;

public class GlobalStateManager : IStateManager {

    /*
    //TODO: set as editor field, saved in prefab
    public static string GRAPH_XML = "Xml/global_states";
    */
    public static TextAsset xmlGraph;

    public static GlobalStateManager Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject("GlobalStateManager");
                instance = go.AddComponent<GlobalStateManager>();
                DontDestroyOnLoad(instance);
                //instance.Load(new GlobalGraphLoader(GRAPH_XML));
                instance.Load(new GlobalGraphLoader(xmlGraph));
            }
            return (GlobalStateManager)instance;
        }
    }

}
