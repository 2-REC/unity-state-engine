/*
 Example implementation of "Level" with:
 - minimal implementation of "InitGame"
 - calls to "EndLevel"
 - 2 buttons to simulate success or failure of level (TO BE REMOVED!)
*/

using System.Collections;
using UnityEngine;

public class LevelManager : Level {

    ////////
    public float startDelayValue = 0.0f;
    public float delayValue = 0.0f;
    ////////

    float timer;
    float startDelay = 0.0f;
    float delay = 0.0f;

    bool doingSetup;


    protected override void InitGame() {
        doingSetup = true;

        ////////
        // TODO:
        // - initialise the controllers (eg: Level, Hud, etc.)
        // - determine the "startDelay"
        //...
        startDelay = startDelayValue;
        delay = delayValue;
        ////////

        Invoke("StartLevel", startDelay);
    }

    protected override void EndProcess() {
        ////////
        // Update Game Data that should be saved when finishing the level (win or fail)
        //...
        ////////
    }

    protected override void SaveProcess() {
        ////////
        // Update Game Data that should be saved when saving the level
        //...
        ////////
    }


    void StartLevel() {
        doingSetup = false;

        ////////
        // TODO:
        // Start the controllers.
        //...
        ////////
    }

    IEnumerator Completed() {
        timer = 0;
        while (timer < delay) {
            timer += Time.deltaTime;
            if (timer >= delay) {
                EndLevel(true);
            }
            yield return null;
        }
    }

    IEnumerator Failed() {
        timer = 0;
        while (timer < delay) {
            timer += Time.deltaTime;
            if (timer >= delay) {
                EndLevel(false);
            }
            yield return null;
        }
    }


    public void NotifyWin() {
        ////////
        // TODO:
        // - notify (win) the controllers (if required)
        // - stop the controllers
        // - determine the "delay"
        //...
        ////////

        if (delay == 0.0f) {
            delay = 0.0000001f; // horrible hack!
        }
        StartCoroutine("Completed");
    }

    public void NotifyDeath() {
        ////////
        // TODO:
        // - notify (lose) the controllers (if required)
        // - stop the controllers
        // - determine the "delay"
        //...
        ////////

        if (delay == 0.0f) {
            delay = 0.0000001f; // horrible hack!
        }
        StartCoroutine("Failed");
    }

}
