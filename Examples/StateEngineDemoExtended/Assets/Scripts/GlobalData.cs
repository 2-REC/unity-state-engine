using UnityEngine;

public class GlobalData : IGlobalDataManager {

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
