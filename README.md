# UNITY STATE ENGINE

State machine implementing a state transition graph to use for a classic game scenes structure.<br>


# USING THE ENGINE

## ADD THE ENGINE IN PROJECT

There are 2 ways to add the engine to a Unity project:<br>

1. Copy the content of the "Engine" directory to a new "StateEngine" folder in the "Assets" folder of the Unity project.<br>

2. Run the script "set_links.bat" in "Examples" to create symbolic links to the engine (and thus not working with a copy)<br>
  => This is useful if working with Git (...)<br>
  ! - The script must be run with admin rights! (and is only for Windows - Linux script coming soon)<br>

## USE THE ENGINE

...
How to use:<br>

(TODO: OK HERE?)<br>
- Implement "GlobalManager" & create a prefab
- Set script order for "GlobalManager" before "default"!
    <br>(to be sure it is executed before "StateController")
- Implement "GameManager" & create a prefab
- Set script order for "GameManager" before "default" (just after "GlobalManager")!
    <br>(to be sure it is executed before "StateController")
<br>?!-TODO: check:
- Idem with "LevelManager" (StateController last derived level)
    <br>(for version 2 => move below?)


- State graphs are defined in "<b>Resources/Xml/game_states.xml</b>" & "<b>Resources/Xml/global_states.xml</b>"<br>
    => The files don't need to be modified, unless some specific states should be added or removed.<br>

- The name of the different scenes can be set in the 2 graph files.<br>
TODO: CHECK FOR LEVEL NAMES!
    ! - Except for the level scenes, which must be "level_<nb>", where <nb> corresponds to the numbers used in the map scene.<br>

- For each state, a scene must be created, which satisfies:<br>
    - Its name as defined in the corresponding state graph<br>
    - It must contain a graph management object:<br>
        - Create an empty object, eg: "StateManager"<br>
        - Add a "GlobalManager" (script) with an attached "GlobalStateManager" prefab object<br>
            => This object will be shared between all states as a unique instance.<br>
TODO: WHY?!?
            (Except for the "NewGame" scene, that doesn't need one)<br>
        - Add a "GlobalStateControler" (script) and specify its corresponding state, or a script derived from "GlobalStateControler" with the corresponding state name (or param?)<br>
TODO: OK?!
            => This object is specific to each state.<br>
        - A "Timer" (script) can be added (global or not depending on graph) if want the state to be left after a period of time<br>
    - It must be added to the project build settings<br>

- The starting scene must be the one defined as first in the global state graph<br>
    => The "Logo" scene if the graphs were not modified.<br>

- The desired game data variables must be defined and used where needed:<br>
    - "DifficultyData" & "Xml/values.xml": for fields depending on the difficulty level<br>
    - SessionManager, GameData, etc.: getters and setters for fields to be used in game<br>
    - Typical fields can be known (and potentially displayed) by specific states (by calling "GameData" getters):<br>
        - "DebriefingFail": number of lives remaining<br>
        - "GameOver": number of continues remaining (+points? ...)<br>
        - "Debriefing": points?, ...<br>
        - "Level": lives remaining, points, current life/energy, ...<br>
    - Values need to be saved between levels in SessionManager (when win, lose, both, ...)<br>
        - Values updated by calling GameData setters, and persisted by calling SessionManager setters<br>
            => Decide which fields are saved persistently and in which case (win/lose/both).<br>
    - ...TODO: continue...(?)<br>


Optional:
(version 2):
    - can add the "LevelManager" script (version 2), a "LevelController" script to the manager, and a "UI Canvas" prefab (containing a "HudController" script) to the scene, for generic game stuff (health, points, lives)<br>
    - for testing purpose, the prefab object "UI Tests Canvas" can be added to the "UI Canvas" object (as child), and the "LevelManager" object instance must be set in the script public parameters. It will add 4 buttons for the following actions: win level, lose level, get hit, add points.<br>



- in game states
    - possibility to "load global" (to init the game data, loaded when coming from global graph)
        <br>=> allowing to test a game state/scene without to start from global graph.
<br>(TODO: check if put details here or leave in file...)
        <br>"InitGame" script in "Tests/Game" (see script for use details).


# EXAMPLES

## STATE ENGINE DEMO

Unity project showcasing how to use the state engine in its most basic way.<br>
It is the minimal implementation required in order to use the engine.<br>

It can be used as a template for the creation of a new game.<br>

Before using the Unity project, the engine must be added to the project.<br>
Look at the section "Adding The Engine" for details.<br>


## STATE ENGINE DEMO EXTENDED

Unity project showcasing how to use the state engine.<br>
It implements a game controller with basic game mechanics, such as points, health and a simple UI allowing to simulate game events.<br>

Before using the Unity project, the engine must be added to the project.<br>
Look at the section "Adding The Engine" for details.<br>
