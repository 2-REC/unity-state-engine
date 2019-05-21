using UnityEngine;

public class BeginAnim : GameStateController {

    public string defaultAnim;

    private string anim;

    public override void HandleMainState() {
        //Debug.Log("BeginAnim:HandleMainState - currentState: " + gameStateManager.currentStateId);

        anim = GetGameData().GetLevelNode().beginAnim;
        //Debug.Log("BeginAnim:Load - beginAnim: " + anim);
        if ((anim == null) || "".Equals(anim)) {
            anim = defaultAnim;
        }
    }

    public string getAnim() {
        return anim;
    }

}
