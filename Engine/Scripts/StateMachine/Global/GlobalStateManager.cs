using UnityEngine;

public class GlobalStateManager : IStateManager {

//TODO: get from settings/config...?
public static string GRAPH_XML = "Xml/global_states";


    public static GlobalStateManager Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject("GlobalStateManager");
                instance = go.AddComponent<GlobalStateManager>();
                DontDestroyOnLoad(instance);
//                instance.Load();
//                instance.Load("Xml/global_states");
                instance.Load(new GlobalGraphLoader(GRAPH_XML));
            }
            return (GlobalStateManager)instance;
        }
    }

}
