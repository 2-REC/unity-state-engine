using UnityEngine;

public class EndAnimFail : GameStateController {
    public string defaultAnim;

    private string anim;

    public override void HandleMainState() {
        //Debug.Log("EndAnimFail:HandleMainState - currentState: " + gameStateManager.currentStateId);

        anim = GetGameData().GetLevelNode().endAnimFail;
        //Debug.Log("EndAnimFail:Load - endAnimFail: " + anim);
        if ((anim == null) || "".Equals(anim)) {
            anim = defaultAnim;
        }
    }

    public string getAnim() {
        return anim;
    }

}
