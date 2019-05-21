using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameStateManager gameStateManager;
//    public GlobalDataManager globalDataManager;
    public GameDataManager gameDataManager;
//    public GameObject soundManager;


    void Awake()
    {
        if (!GameStateManager.IsInstance())
        {
            Instantiate(gameStateManager);
        }

/*
        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }
*/

        GameStateManager stateManager = GameStateManager.Instance;
/*
        if (stateManager.GetGlobalData() == null) {
            GlobalDataManager globalData = Instantiate(globalDataManager);
            stateManager.SetGlobalData(globalData);
//?            GlobalSessionManager.Init();
            stateManager.GetGlobalData().Load();
        }
*/
        if (stateManager.GetGameData() == null) {
            GameDataManager gameData = Instantiate(gameDataManager);
            stateManager.SetGameData(gameData);
            stateManager.GetGameData().Load();
        }
    }

}
