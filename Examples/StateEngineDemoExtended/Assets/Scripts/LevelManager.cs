/*
 Example implementation of "Level" with:
 - implementation of "InitGame"
 - calls to "EndLevel"
 - basic HUD management
 - basic level controller management
 - 2 buttons to simulate success or failure of level (TO BE REMOVED!)

 Remark: The links with the Hud controller & level controller can be done
  differently depending on the scene's strcture/hierarchy.
*/

using System.Collections;
using UnityEngine;

public class LevelManager : Level {

    //////// GAME - BEGIN
    int health;
    int points;
    //////// GAME - END

    public float flashSpeed = 5.0f;
    public Color flashColour = new Color(1.0f, 0.0f, 0.0f, 0.1f);


    LevelController levelController;
    HudController hudController;

    float timer;
    float startDelay = 0.0f;
    float delay = 0.0f;

//TODO: public? (as property with private "set"?)
    bool doingSetup;


    protected override void InitGame() {
        doingSetup = true;

        //////// GAME - BEGIN
        GameData gameData = (GameData)GetGameData(); 
        health = gameData.GetHealth();
        points = gameData.GetPoints();
        //////// GAME - END

        levelController = (LevelController)FindObjectOfType(typeof(LevelController));
        hudController = levelController.getHudController();

        //////// GAME - BEGIN
        hudController.SetLives(lives);
        hudController.SetPoints(points);
        hudController.InitLifebar(health, gameData.GetInitialHealth());
        //////// GAME - END

        startDelay = levelController.InitLevel();
        Invoke("StartLevel", startDelay);
    }

    protected override void EndProcess() {

        //////// GAME - BEGIN
        // Update Game Data that should be saved when finishing the level (win or fail)
        //...
        ((GameData)GetGameData()).SetPoints(points);
        //////// GAME - END
    }

    protected override void SaveProcess() {
        //////// GAME - BEGIN
        // Update Game Data that should be saved when saving the level
        //...
        GameData gameData = (GameData)GetGameData(); 
        gameData.SetHealth(health);
        gameData.SetPoints(points);
        //////// GAME - END
    }


    void StartLevel() {
        doingSetup = false;

        levelController.StartLevel();
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
        levelController.StopLevel();
        delay = levelController.NotifyWin();

        if (delay == 0.0f) {
            delay = 0.0000001f; // horrible hack!
        }
        StartCoroutine("Completed");
    }

    public void NotifyDeath() {
        //////// GAME - BEGIN
        hudController.SetLives(lives - 1);
        //////// GAME - END

        levelController.StopLevel();
        delay = levelController.NotifyLose();

        if (delay == 0.0f) {
            delay = 0.0000001f; // horrible hack!
        }
        StartCoroutine("Failed");
    }

    //////// GAME - BEGIN
    public void NotifyHit(int damage) {
        health -= damage;
        if (health < 0) {
            health = 0;
        }

        hudController.Flash(flashColour, flashSpeed);
        hudController.SetLifebar(health);

        if (health == 0) {
            NotifyDeath();
        }
    }
    //////// GAME - END

//TODO: Can add bonus stuff ...
/*
    public void NotifyHeal(int life) {
    }
*/

    //////// GAME - BEGIN
    public void AddPoints(int amount) {
        points += amount;
        hudController.SetPoints(points);
    }
    //////// GAME - END

    //////// GAME - BEGIN
//TODO: Keep? Here? Do same for other fields?
    public int GetHealth() {
        return health;
    }
    //////// GAME - END


    public override void Pause() {
//?        hudController.Pause();
//?        levelController.Pause();
    }

    public override void Unpause() {
//?        hudController.Unpause();
//?        levelController.Unpause();
    }

    public void Quit() {
//TODO: OK, anything else to do?
// (eg: stop controller? Free stuff? ...?)
        QuitGame();
    }

}
