public class GlobalData : IGlobalDataManager {

    //////// GAME - BEGIN
    //...
    //////// GAME - END


    protected override void LoadSpecifics() {
        //////// GAME - BEGIN
        //... = GlobalSessionManager.GetField("...");
        //...
        //////// GAME - END
    }

    protected override void CommitChangesSpecifics() {
        //////// GAME - BEGIN
        //GlobalSessionManager.SetField("...", ...);
        //...
        //////// GAME - END
    }

}
