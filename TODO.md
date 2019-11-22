
# TODO

## BUG FIXES / TESTS
- [ ] Fix bug with continue: - CHECK IF STILL THERE!
    - If choose "no" (and exit game), when come back with "continue", we're back with the last life ...
    <br>=> Don't allow to continue if chosen "no" (delete data)!
- [ ] "Fix" debriefing stuff: Need to determine when it's last level (MODIFY "IsGameComplete").
    <br>=> EITHER:
    - reach a level with no "next" (=null)
    - all the levels are "completed" (=true)
- [ ] Check why calling "CommitChanges" in "DebriefingFail", but not in "Debriefing".
- [ ] (?) Might not be a good thing to call "PlayerPrefs" in "SessionManager.Reset"
    <br>=> If want to keep data persistent through different games (eg: highest scores, etc.)
- [ ] "GameData.Leave" calls "SessionManager.Save" to save the data when leaving the game graph.
    - This can be a problem as data is not saved before, so in case of crash (or voluntary "kill" of the application) the data will not be saved (which can be seen both as a bug or as a way to cheat)
    <br>=> OK to keep as is?


## DOCUMENTATION
- [ ] Make documentation
    <br>=> Use, objects/projects, tests, etc.
- [ ] Write README.md


## CLEANING

### SCRIPTS
- [ ] Remove/fix TODOs
- [ ] Add comments in "scripts/GameData" of V1
- [ ] Remove useless "using" in scripts
- [ ] Unify
    - [ ] Coding style (uniformise)
    - [ ] Variables/prefabs/resources names
- [ ] Add/remove comments
- [ ] Remove/comment logs
- [ ] ...



## ENHANCEMENTS

### ORGANISATION
- [ ] Files in "Resources/Xml/" should not be common
    <br>=> Move out of engine (& set templates/examples)


### SCRIPTS
- General:
    - [ ] For calls to "LoadChildState", use enum of defined children instead of strings
        <br>=> How?
    - [ ] See how to manage init timer and game timer properly
        <br>=> NOT as in RAD!
    - [ ] "continueScene" should be enums instead of strings! (?)
- Data:
    - [ ] Serialize data for save/load & options/ingame files<br>
        https://gamedevelopment.tutsplus.com/tutorials/how-to-save-and-load-your-players-progress-in-unity--cms-20934
    - [ ] Use binary files for save/load & options/ingame files
        <br>=> Look at:<br>
        https://answers.unity.com/questions/1219643/how-to-store-game-variables.html
- LevelManager:
    - [ ] Add listeners management:
        - AddListener(listener, eventType) => Called by listeners
        - NotifyListeners() => Called when an event occurs
        <br>=> Use this for "Start" listeners (when "doingSetup" is done)
    - [ ] Set "HudController" as public field
    - [ ] Remove "HUD" reference from "LevelController" and do everything in "LevelManager"
        <br>=> Pass variables from "LevelController" to "LevelManager"<br>
        ! - No need to get variables from "GameData" from "LevelController" ("LevelController" and "HudController" are independent)
- HudController:
    - [ ] Don't override fade delay with flash speed
        <br>=> Use different variables
    - [ ] Set "timeStep" as a variable
- Level:
    - [ ] (?) Add "Pause", "Unpause" & "Quit" to "Level.cs"? (as abstract)
    - [ ] (?) "StartLevel" in "LevelManager" should override method from "Level"?
        <br>=> Add abstract method in "Level.cs"?
    - [ ] (?) Add "NotifyHit" in "Level.cs" (common?)
    - [ ] (?) Add "NotifyHeal" in "Level.cs" (common?)



### PORTABILITY
- [ ] Add symbolic links script for Linux ("set_links")


## EXTRA TOOLS
LATER:
- [ ] Add a graph editor for the state transitions
- [ ] ...
