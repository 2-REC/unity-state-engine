using UnityEngine;

public class EndAnim : GameStateController {
    public string defaultAnim;

    private string anim;

    public override void HandleMainState() {
        //Debug.Log("EndAnim:HandleMainState - currentState: " + gameStateManager.currentStateId);

        anim = GetGameData().GetLevelNode().endAnim;
        //Debug.Log("EndAnim:Load - endAnim: " + anim);
        if ((anim == null) || "".Equals(anim)) {
            anim = defaultAnim;
        }
    }

    public string getAnim() {
        return anim;
    }

}
