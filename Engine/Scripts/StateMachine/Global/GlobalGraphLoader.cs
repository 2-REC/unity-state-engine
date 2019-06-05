using UnityEngine;

public class GlobalGraphLoader : IGraphLoader {

    public GlobalGraphLoader(string filename)
            : base(filename) {
    }

    protected override bool CheckAttributes(StateData data) {
        if ((data.scene == null) || "".Equals(data.scene)) {
            Debug.Log("Invalid state!");
            return false;
        }
        return true;
    }

}
