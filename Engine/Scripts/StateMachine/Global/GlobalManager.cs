using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public GlobalStateManager globalStateManager;
    public GlobalDataManager globalDataManager;
//    public GameObject soundManager;


    void Awake()
    {
        if (!GlobalStateManager.IsInstance())
        {
            Instantiate(globalStateManager);
        }

/*
        if (SoundManager.instance == null)
        {
            Instantiate(soundManager);
        }
*/

        GlobalStateManager stateManager = GlobalStateManager.Instance;
        if (stateManager.GetGlobalData() == null) {
            GlobalDataManager globalData = Instantiate(globalDataManager);
            stateManager.SetGlobalData(globalData);
            GlobalSessionManager.Init();
            stateManager.GetGlobalData().Load();

// !!!! ???? TODO: OK HERE ? ???? !!!!
GameSessionManager.Load();
        }
    }

}
