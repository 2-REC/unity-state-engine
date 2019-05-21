using UnityEngine;

public class GlobalData : GlobalDataManager {

//...


    protected override void LoadSpecifics() {
//        ... = GlobalSessionManager.GetField("...");
//...
    }

    protected override void CommitChangesSpecifics() {
//        GlobalSessionManager.SetField("...", ...);
//...
    }

}
