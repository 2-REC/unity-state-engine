/*
If want to use Global data in game, uncomment the "GLOBAL_IN_GAME" blocks.
*/

using UnityEngine;

public class GameStateManager : IStateManager {

//TODO: get from settings/config...?
public static string GRAPH_XML = "Xml/game_states";

//////// GLOBAL_IN_GAME - BEGIN
//    private IGlobalDataManager globalDataManager = null;
//////// GLOBAL_IN_GAME - END


    public static GameStateManager Instance {
        get {
            if (instance == null) {
                GameObject go = new GameObject("GameStateManager");
                instance = go.AddComponent<GameStateManager>();
                DontDestroyOnLoad(instance);
//                instance.Load();
//                instance.Load("Xml/game_states");
                instance.Load(new GameGraphLoader(GRAPH_XML));
            }
            return (GameStateManager)instance;
        }
    }

    protected override string GetSceneName(State state) {
        GameState gameState = (GameState)state;
        if (gameState.IsLevel) {
            string effectiveScene = ((IGameDataManager)dataManager).GetSceneName();
            if ((effectiveScene != null) && !"".Equals(effectiveScene)) {
                return effectiveScene;
            }
        }
        return state.Scene;
    }

//////// GLOBAL_IN_GAME - BEGIN
/*
    public void SetGlobalDataManager(IGlobalDataManager globalDataManager) {
        if (this.globalDataManager == null) {
            this.globalDataManager = globalDataManager;
        }
    }

    public IGlobalDataManager GetGlobalDataManager() {
        return globalDataManager;
    }
*/
//////// GLOBAL_IN_GAME - END

}
