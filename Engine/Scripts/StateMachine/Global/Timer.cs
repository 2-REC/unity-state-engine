/*
 Script to simulate a timeout and leave the current state/scene.
*/

using UnityEngine;

public class Timer : MonoBehaviour {

    public GlobalStateController ctrl;
    public float delay = 3f;

    void Start() {
        if (ctrl == null) {
            Debug.Log("Timer: No GlobalStateController set => Ignoring script");
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
