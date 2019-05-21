/*
 Script to simulate a timeout and leave the current state/scene.
*/

using UnityEngine;

public class TimerGame : MonoBehaviour {

    public GameStateController ctrl;
    public float delay = 3f;

    void Start() {
        if (ctrl == null) {
            Debug.Log("Timer: No GameStateController set => Ignoring script");
            return;
        }
        if (delay > 0) {
            Invoke("End", delay);
        }
    }

    void End() {
        ctrl.End();
    }

}
