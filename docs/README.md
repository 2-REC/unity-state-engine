UNITY STATE ENGINE
==================

# Introduction

State machine implementing a state transition graph to use for a classic game scenes structure.

(add more infos)

Only state and data management system, no game is provided.

Built for Unity 6.


# Usage

To use the "State Graph Engine", a number of steps are required:
* Setup the Unity project
* Design the state graphs
* Define the game data
* Build the graph manager prefabs
* Create the state scenes
* Prepare(?) the game levels
(TODO: ADD LINKS TO EACH SECTION)

(TODO: REWRITE)
Samples are provided with the engine(TODO: LINK SAMPLES), allowing to use the engine straight away, either for testing/exploring, or to use as a base for a new game.


# Project Setup

> **NOTE:** This will be changed when project becomes a package.

The first step consists in creating a Unity project ready to use the engine:
* Download the latest release (or clone the repository for latest changes).
* Extract the downloaded repository to a temporary directory.
* Create a new Unity project.\
	Any type of project can be created depending on the desired game type. The state engine is independent of the project type.
* Copy the `StateEngine` directory from the extracted repository to the project's `Assets` folder.
* Set the scripts execution order.\
	Some scripts need to be executed early at the start of the application for proper initializations.
	This can be managed by changing the script execution order:
	* Go to `Project Settings` -> `Script Execution Order`
	* Add `GlobalManager` before `Default Time`
	* Add `GameManager` before `Default Time` (just after `GlobalManager`)

	![Project Settings - Script Execution Order](./docs/images/script_order.jpg "Script Execution Order")


# State Graphs

The state engine manages state transitions based on **graphs**(TODO: LINK "State Graphs -> Graphs").

Graphs are composed of **states**(TODO: LINK "State Graphs -> States") and **transitions**(TODO: LINK "State Graphs -> Transitions") between these states.

...
To implement game logic, **actions**(TODO: LINK ACTIONS) must be implemented and executed in some states...


The graphs are managed by **graph managers**(TODO: LINK "Graph Managers").


## Graphs

A game typically contains 2 graphs:
* **Global Graph**(TODO: LINK "Graphs -> Global Graph"): Starting point of the application, allowing to start play sessions.
* **Game Graph**(TODO: LINK "Graphs -> Game Graph"): The game itself, handling a play session.

The main difference between the 2 graph types is that a game graph handles game levels, through a specific "*level*" state.

Tehnically, a game can have more than 2 graphs (e.g. several global and/or game graphs), though there is no obvious use for it.

> **NOTE:** It is possible to have only a game graph.
This however requires a specific initialization, which would normally be made by the global graph (TODO: LINK to section with 'initGame')

Graphs are defined in XML files.
They can be created manually or by using the [unity-state-engine-graphview](https://github.com/2-REC/unity-state-engine-graphview) tool.


### Global Graph

Graph used at the global level, when launching the application.

The global graph manages the high-level structure of the game.
It serves as the starting point for the application, and manages the starting of play sessions.

In order to have a working game, the handling of essential **actions** (TODO: LINK STATE CONTROLLER) is required in a global graph:
* `NewGame`: Start a new game.
* `Quit`: Leave the application.

Additionally, other common global actions include:
* `LoadGame`: Load a previously saved game.
* `Continue`: Continue the current game.

All the actions can be handled in a single state, or can be handled independently in different states.
Typically, a global graph will have a main state (presenting a main menu), with transitions to other states, each handling a specific action.

A global graph can also contain other states with specific roles, such as:
* Studio logo screen
* Introduction video sequence
* Game settings screen
* Game credits
* etc.

An example of a global graph is presented in the sample projects(TODO: LINK "Samples").


### Game Graph

Graph used at the game level, when starting or loading a play session.

The graph manages the game itself, coordinating the game levels.

A game graph must have a special "*level*" state, which will hold the core playable elements of the game (TODO: LINK LEVEL STATE).

Another state common to many games is a "*map*" state.
This state is optional, but is useful for presenting a level selection screen or a view of the game world, managing the **level tree**(TODO: LINK LEVEL TREE).

Other game specific states can be included in the graph, such as:
* Pre- and post-level cut scene
* Level briefing and debriefing
* Game Over screen
* etc.

As for the global graph, handling essential actions is required in a game graph:
* `StartLevel`: Start a new level.
* `EndLevel`: Finish the current level (with success or failure).
* `CheckGameComplete`: Determine if the game is over by reaching its end objective.
* `CheckGameOver`: Determine if the game is over after failing all available attempts.
* `CheckContinue`: Determine if the game can be resumed from a previous state after all attempts have failed.
* `UseContinue`: Resume the game from a previous state.
* `QuitGame`: Leave the game.

Additionally, other common global actions include:
* `QuitLevel`: Leave the current level.
* `SaveGame`: Save the game in its current state.

The `EndLevel` and `QuitLevel` actions can only be handled in the "*level*" state directly.\
All the other actions can be handled independently in any state.

Actions are described in more details in ...(TODO: LINK STATE CONTROLLER).

An example of a game graph is presented in the sample projects(TODO: LINK "Samples").


## States

A state corresponds to a Unity scene, which gets loaded when transitionning to that state.

States are defined by a `<state>` node, taking a set of attributes:
* `id`: A unique identifier for the state, which can be used to reference the state in other states.\
	This id is also used internally by the engine.
* `scene`: The name of the scene corresponding to the state (name without extension).\
	Scene names must be unique in order to avoid ambiguity and potential issues. A scene can however be shared between several states.
* `restartable`[*]: A boolean specifying if when coming back from a child state (**TODO:** see transitions), the state is restarted instead of transitionning to its "next" state.\
	By default, a state is not restartbale, in which case the attribute can be omitted.
* `next`[*]: The `id` of the "next" state in the graph execution.\
	When a state has finished its execution, an automatic transition can be triggered to this other state.\
	The attribute can be omitted, in which case the "next" transition will switch to the parent state.
* `leavable`[*]: A boolean specifying if the engine can exit the current graph from this state.\
	By default, a state is not leavable, in which case the attribute can be omitted.\
	**NOTE:** Nothing prevents the aplication from leaving the graph from any state, but this allows some control from within the graph execution.

[*]: More information about these attributes is provided in "transitions" (TODO: LINK TRANSITIONS)

Here is an example of a game state definition:
```xml
<state id="MAP" scene="Map" restartable="true" leavable="true" next="LEVEL">
```


### Level State

A game graph must contain a "*level*" state, where the actual gameplay takes place.

The "*level*" state will generally be shared between all game levels, thus a single scene cannot be associated to it, as opposed to the other states.

For this reason, the "*level*" state in a game graph must have a specific `isLevel` attribute set:
```xml
<state id="LEVEL" isLevel="true" />
```
When the graph manager finds a state with this attribute set, it ignores the state's `scene` attribute (which is thus useless for that state and can be omitted from its definition).

Scenes associated to the levels are defined in the "level tree"(TODO: LINK LEVEL TREE).

In general, a game graph will only have one "*level*" state, but more than one are allowed if different state flows are desired for some levels.
Examples of game graphs with multiple "*level*" states are presented in the annexes(?)(TODO: LINK TO ANNEXES?).


## Transitions

Graphs generally have a starting state, but this is not mandatory: a game graph can for example start from a different state when beginning a new game or when continuing a previously saved game.\
The first state found in the graph definition will be considered as the initial one, but a graph execution can be started from any state by loading its corresponding scene first.

When a state finishes its execution, it can trigger a transition through the graph manager to switch to another state.

Different types of transitions are managed, depending on how they've been defined:
* *next* transition
* *child* transition
* *leave* transition

Transitions are triggered using **actions**, which correspond to calling specific methods in the graph manager.\
Actions are defined in **state controllers**(TODO: LINK STATE CONTROLLER).


### Next

A transition to the *next* state occurs when a state has finished its execution, and is triggered automatically by the graph manager.

As seen in the state's definition (**TODO** link?), a *next* transition is defined using the `next` attribute.

An example of a state with a *next* transition is as follows:
```xml
<state id="LOGO" scene="Logo" next="INTRO">
```

If no *next* state is specified, an implicit transition to the *parent* state is triggered when the state's executionn has ended (if there is no *parent* state, the application quits).

A *next* transition is triggered by calling the state controller's `End` method.

A *parent* state is a state for which a list of *child* transitions are defined.


### Child

In some cases, it is desired after a state's execution to come back to a previous state.

*child* transitions allow the graph manager to automatically switch back to their *parent* state once their execution has ended.
If the *parent* state's `restartable` attribute is set, it executes itself again, else a transition to its *next* state is triggered.

*child* transitions are defined by adding a `<children>` node inside the state's `<state>` node, containing a `<child>` node for each transition.

An example of a state with *child* nodes is as follows:
```xml
<state id="MENU" scene="Menu" next="QUIT">
	<children>
		<child id="NEW_GAME"/>
		<child id="OPTIONS"/>
	</children>
</state>
```

A *child* transition is triggered by calling the state controller's `LoadChildState` method, with the child state name as parameter.


### Leave

To exit a graph, one can simply load another scene.\
However, this is not advised, as it would not properly stop the graph manager and could create unexpected behavior.

To properly leave a graph, a specific command must be used, which will make sure everything is cleaned before leaving.

To add a bit of control, it is not allowed to leave the graph from any state: a state must have its `leavable` attribute set to allow the transition outside of the graph.

The transition is triggered by calling the state controller's `Leave` method, with the target scene name as parameter (or no parameter to leave the application).


# Data

The engine provides an easy way to manage data to be used by the application.
Global data and game data can be defined, and will be available for use from within the associated graph.

Global data generally comprises the main game settings, and game data represents the ingame values such as the number of lives, points, etc.


## Definition

For the game data, game specific **data fields**(TODO: LINK GAME DATA FIELDS) can to be defined in an XML file.

> **NOTE:** Currently an XML file is only handled for game data and not global data. Global data can still be defined and used in code, but is not managed automatically as the game data.

The XML file must follow a specific structure, essentially divided in 2 parts: *common* fields as well as fields associated to a specific *difficulty level*.


### Difficulty Levels

A common feature in games is the ability to choose between difficulty levels.

The engine allows to define as many difficulty levels as desired (at least 1).

A difficulty level is defined in the XML file by adding a `<difficulty>` node, defining the data associated to that difficulty level.
Generally each difficulty node will contain the same fields, holding different values.

For data fields that don't depend on the difficulty level, a `<common>` block can be used.
TODO: OK if no 'common' block? => Change code if needed!
(write about it...)

### Lives & Continues

TODO: rewrite
Almost any game is based around common basic components:
- **levels**, which can be won or lost (success or failure)
- **lives**, specifying the number of attempts available to complete a goal or a level
- **continues**, specifying the number of times the player can resume the game after having lost all their lives or failing an objective, instead of being forced to start completely over.

The state engine automatically handles these essential game elements, and 
already manages game fields for the number of lives and continues.

When a player fails an objective (to be defined by the game), they lose a life.\
When all lives are lost, a continue can be used to retry (resetting the number of lives).\
When all continues have been used, the game is over.

The number of lives and continues are generally associated to difficulty levels, and can be added as direct attributes to the `<difficulty>` nodes, named respectively `lives` and `continues`:
```xml
<difficulty lives="5" continues="2" />
```
They can also be defined the same way as the other game data fields(TODO: LINK GAME FIELDS), using the "LIVES" and "CONTINUES" identifiers.

The 2 fields can also be omitted, and default values will be used (1 life, 0 continues).


### Game Data Fields

Lives and continues can be enough for some games, but generally other data fields are needed (common ones such as health, points, etc.).

Additional game specific data fields can be defined and used where needed in the engine.

To add game data fields to the XML file, the `<field>` node is used.
They can be added in the `<common>` node or in the `<difficulty>` nodes.

Here is an example of a basic game data file adding data for health and points management:
```xml
<values>
    <common>
        <field name="POINTS" value="0" />
    </common>
    <difficulty lives="5" continues="2">
        <field name="HEALTH" value="100" />
    </difficulty>
    <difficulty lives="3" continues="1">
        <field name="HEALTH" value="50" />
    </difficulty>
    <difficulty lives="1" continues="0">
        <field name="HEALTH" value="25" />
    </difficulty>
</values>
```

Depending on which difficulty level is set when starting the game, the player will have different numbers of lives and continues.\
The starting health value also varies depending on the difficulty level, whereas points starting value is the same for all difficulties.

Example files are provided in the samples(TODO: LINK SAMPLES).


## Data Managers

Data fields are handled by **session managers**, providing ways to load and save the fields persistent values. Persistent values are kept between states.

The data fields can then be managed by **data managers**, controlling when to modify or restore the fields values.
This requires data manager scripts to be created. Base abstract classes are provided for global and game data respectively.

These managers are controlled be **graph managers**.

To summarize:
* Graph managers handle session and data managers.
* Session managers manage data persistent values.
* Data managers manage data current values, via orverridden scripts.


### Game Data

To manage the game data fields from within the engine, a game data manager script must be created.

The script should be an implementation of the `IGameDataManager` abstract class (defined in the script with the same name), overriding its abstract methods to call methods from the associated session manager.\
The methods to override are:
* `LoadSpecifics`
* `CommitChangesSpecifics`
* `ResetLifeData`
* `ResetContinueData`

For each game data field defined in the XML file:
* Declare the game field as a public property:
	```csharp
	public int points { get; set; } = 0;
	```
	The field current value can then be managed through the property from anywhere via the data manager using:
	```csharp
	GetGameData().points
	```
* Load its persisted value in `LoadSpecifics`:
	```csharp
	points = gameSessionManager.GetField("POINTS");
	```
* Save its value in `CommitChangesSpecifics`:
	```csharp
	gameSessionManager.SetField("POINTS", points);
	```
	The fields set in this method will typically be saved between levels.
	If a field value doesn't need to be persisted between levels, it should not be added here.
* Load the initial value in `ResetLifeData` or `ResetContinueData` (or both), depending if the field is related to a life or a continue:
	```csharp
	points = gameSessionManager.GetInitialField("POINTS");;
	```
	`ResetLifeData` will be called when losing a life, and `ResetContinueData` will be called when using a continue.

> **NOTE:** The creation of the script could be automated by parsing the XNL file and automatically adding the commands associated to each data field. This might be implemented in future versions of the project.

To save the data fields current values and make them persistent for other states, the data manager `CommitChanges` method can be used.
The method can be called from anywhere by getting access to the game data manager:
```csharp
GetGameData().CommitChanges();
```
For example, some values should be saved when ending a level (when winning and/or losing).

If want to manage fields independently, the session manager methods can be called directly:
```csharp
int GameSessionManager.Instance.GetField(string name);
void GameSessionManager.Instance.SetField(string name, int value);
```

### Global Data

Similarly, to manage the global data fields from within the engine, a global data manager script must be created.

The script should be an implementation of the `IGlobalDataManager` abstract class (defined in the script with the same name), overriding its abstract methods:
* `LoadSpecifics`
* `CommitChangesSpecifics`

> **NOTE:** There is currently no handling of an XML file to define the global data fields.

Then, for each global data field:
* Declare the global field as a public property.
* Load its persisted value in `LoadSpecifics`.
* Save its value in `CommitChangesSpecifics`.


# Graph Managers

The main components of the state engine are the **graph managers**.
They manage the states and the transitions between them, as well as the data.\
A graph manager game object must to be present in each of its state scenes(TODO: LINK SCENES).

A global graph manager is composed of:
* A `GlobalManager` script component, with the following properties:
	* A `GlobalStateManager` prefab.\
		This game object will be shared between all the states of the graph as a unique instance.
	* A `GlobalDataManager` prefab.
	* A `GlobalStateGraph` text asset.
	* A `GameData` text asset.\
		The XML file containing the game specific data fields definitions has to be attached to both global and game graph managers through their `Game Data` property. The same file must be set for both managers.
* A `GlobalStateControler` script component.\
	This component is specific to each state, and can be replaced if desired in any state.[*]

A game graph manager is composed of:
* A `GameManager` script component, with the following properties:
	* A `GameStateManager` prefab.\
		This game object will be shared between all the states of the graph as a unique instance.
	* A `GameDataManager` prefab.
	* An optional `GlobalDataManager` prefab, if access to the global data is needed.
	* A `GameStateGraph` text asset.
	* A `GameData` text asset.\
		The XML file containing the game specific data fields definitions has to be attached to both global and game graph managers through their `Game Data` property. The same file must be set for both managers.
	* A `GameLevels` text asset.
* A `GameStateController` script component.\
	This component is specific to each state, and can be replaced if desired in any state.[*]

[*]: The default state controller scripts (`GlobalStateControler` or `GameStateController`) are enough for basic states and common actions. However, if additional operations or actions are needed for a state, the script can be replaced by a new script overriding the class in the instantiated prefab in that specific state's scene.

Once set up, graph managers should be saved as prefabs, and reused in every state scene of their associated graph. The same prefab has to be used in every graph scene, with only the state controller component eventually replaced by an overridden script in specific scenes.

Pre-built prefabs are provided for the graph managers to facilitate the setup, only requiring their properties to be set.
However, if desired, new ones can easily be built from scratch (or could be part of other game objects, though this is not recommended).

Alternatively, ready to use prefabs are also provided in the samples(TODO: LINK SAMPLES - eg: GlobalManagerStarter).


## Global Manager Prefab

A number of steps are required to build a global graph manager.

**Prerequisites:** Before creating the game object, make sure to have the following components available (see previous sections on how to create them, or use the ones provided in the project samples):
* A global data manager script implementing `IGlobalDataManager` (e.g.: `GlobalDataManager.cs`).
* A global state graph XML file (e.g.: `global_states.xml`).
* A game data XML file (e.g.: `values.xml`).\
	The global graph manager needs to have access to the game data definitions, as some of them are required when starting or loading a game. No game data manager script is needed here though.

Once the prerequisite components are available, the game object can be created:
1. Create a **global data manager** prefab.
	* Either create an empty object and add a `GlobalDataManager` script component, or drag and drop the script in the *Hierarchy* panel.
	* Save the prefab. Name it `GlobalDataManager` for example.
	* Delete the instantiated prefab in the *Hierarchy* panel.
2. Create the **global graph manager** prefab:
	* Instantiate the provided `GlobalManager` prefab.\
		The prefab should already have a `GlobalStateManager` prefab set for its `Global State Manager` porperty, as well as a `GlobalStateController` script component attached to it.[*]
	* Set the `GlobalManager` prefab properties:
		* Set the `GlobalDataManager` prefab for the `Global Data Manager` property (the prefab itself, **NOT** an instance).
		* Set the `global_states` XML file for the `Global States Graph` property.
		* Set the `values` XML file for the `Game Data` property.
	* Save the prefab (as a variant, or replacing the original), and delete it in the *Hierarchy* panel.

	![Global Manager](./docs/images/globalmanager.jpg "Global Manager")

[*]: The `GlobalStateController` component is specific to each state, and can be replaced by an overridden script in the instantiated prefab of any state if desired (TODO: see below - link?).


## Game Manager Prefab

As for the global graph manager, a number of steps are required to build a game graph manager.

**Prerequisites:** Before creating the game object, make sure to have the following components available (see previous sections on how to create them, or use the ones provided in the project samples):
* A game data manager script implementing `IGameDataManager` (e.g.: `GameDataManager.cs`).
* A game state graph XML file (e.g.: `game_states.xml`).
* Optionally a global data manager script implementing `IGlobalDataManager` (e.g.: `GlobalDataManager.cs`).
* A game data XML file (e.g.: `values.xml`).

Once the prerequisite components are available, the game object can be created:
1. Create a **game data manager** prefab.
	* Either create an empty object and add a `GameDataManager` script component, or drag and drop the script in the *Hierarchy* panel.
	* Save the prefab. Name it `GameDataManager` for example.
	* Delete the instantiated prefab in the *Hierarchy* panel.
2. Optionally create a **global data manager** prefab from the script with the same name.
	* Either create an empty object and add a `GlobalDataManager` script component, or drag and drop the script in the *Hierarchy* panel.
	* Save the prefab. Name it `GlobalDataManager` for example.
	* Delete the instantiated prefab in the *Hierarchy* panel.
3. Create the **game graph manager** prefab:
	* Instantiate the provided `GameManager` prefab.\
		The prefab should already have a `GameStateManager` prefab set for its `Game State Manager` porperty, as well as a `GameStateController` script component attached to it.[*]
	* Set the `GameManager` prefab properties:
		* Set the `GameDataManager` prefab for the `Game Data Manager` property (the prefab itself, **NOT** an instance).
		* Optionally check the `Use Global Data Manager` checkbox and set the `GlobalDataManager` prefab for the `Global Data Manager` property.
		* Set the `game_states` XML file for the `Game States Graph` property.
		* Set the `values` XML file for the `Game Data` property.
		* Set the `levels` XML file for the `Game Levels` property.
	* Save the prefab (as a variant, or replacing the original), and delete it in the *Hierarchy* panel.

	![Game Manager](./docs/images/gamemanager.jpg "Game Manager")

[*]: The `GameStateController` component is specific to each state, and can be replaced by an overridden script in the instantiated prefab of any state if desired (TODO: see below - link?).


# Scenes

Every state requires an associated scene.
A scene can however be shared between several states.

Scenes must satisfy the following requirements:
* The scene name must be the same as the one defined in the corresponding state graph (`scene` attribute of a `<state>`).
* The scene must contain a graph manager:
	* `GlobalManager` prefab for global states.
	* `GameManager` prefab for game states.
* The scene must be added to the project's scene list (in build profiles).

**NOTE:** The starting scene (first one in the project's build settings) should be the one associated to the state defined as entry point in the global state graph.

Additionally, depending on the state's attributes and transitions, certain actions(TODO: LINK STATE CONTROLLER) are expected to be executed in certain conditions.
This is managed by overriding and replacing the **state controller**(TODO: LINK STATE CONTROLLER) script component in the graph manager for the specific state.


## State Controller

The state controller in a graph manager handles the state specific logic and transitions.

As described earlier(TODO: LINK TRANSITIONS), transitions are triggered using actions.
An action is a method defined in a state controller script, which can be called from within a state.

Basic transition actions are directly available in the default `GlobalStateController|GameStateController` script component attached to the graph manager:
* `End`: Trigger transition to the *next* state.
* `LoadChildState`: Trigger transition to a *child* state.\
	The method takes a state name as parameter.\
	The specified state must be defined as a *child* state in the graph, else an error will be raised.
* `Leave`: Trigger transition to another scene, leaving the current graph.\
	The method takes a scene name as parameter.

More actions can be defined for a state by overriding the state controller script and replacing it in the graph manager instance.

> **NOTE:** The `StateController` script component should be overridden in the graph manager prefab **instance** in the scene, not in the prefab itself (unless changes should be available for every state in the graph).

!["State Controller component of Graph Manager"](./docs/images/gamemanager-statecontroller.jpg "State Controller component of Graph Manager")

Additionally, specific state initializations can be done by overriding the `HandleMainState` method.

Example of a game state override, using the player's number of lives:
```csharp
public class LevelState : GameStateController {
    protected int lives;

    public override void HandleMainState() {
        lives = GetGameData().GetLives();

        // other initializations...
    }

    // more state specific methods...

}
```


### Global Actions

...specific global states
handle main menu to start and load game sessions.
can also have game options, credits, etc.
(TODO: redundant...? -> should not repeat to many times)

TODO:...
specific global actions
... override `GlobalStateController` script and add specific methods:

* `NewGame`: Start a new play session.\
	Steps:
	* Create a new game session with a specified difficulty level.\
		Difficulty levels are defined and handled by data managers(TODO:LINK DATA).\
		The difficulty level is provided as an input integer parameter to the method (starting from 0).
	* Leave the graph (transitionning to a state in the game graph).
	```csharp
	public void StartGame(int difficulty) {
		GameSessionManager.Instance.NewGame(difficulty);
		Leave("<GAME_SCENE>");
	}
	```
	where `<GAME_SCENE>` is the name of the game state's scene to load.
* `LoadGame`: Load a previously saved game.\
	Steps:
	* Determine saved game to load.\
		The saved game identifier can be provided as parameter to the method.
	* Load a game session with the saved game identifier.
	* Leave the graph if the loading was successful (transitionning to a state in the game graph).\
		This will indirectly set the current level by calling the game session manager's method `SetLevel`.
	```csharp
	public void StartGame(string filename) {
		if(GameSessionManager.Instance.LoadGame(filename)) {
			Leave("<GAME_SCENE>");
		}
	}
	```
	where `<GAME_SCENE>` is the name of the game state's scene to load.
* `Quit`: Leave the graph and close the application.\
	No specific method is required for this action, as the `Leave` method can be called directly without parameter (no scene name).\
	However, other operations could be done, such as saving the current game (using the game session manager's `SaveGame` method).
* `Continue`: Go back to the current or last loaded game session.\
	This can be done by directly calling the `Leave` method with the desired game state's scene name.\
	To make sure a game session is actually loaded, a check on the current level can be added (if no level is set, the game will start from the first level).
	```csharp
	public void Continue() {
		if (GameSessionManager.Instance.GetLevel() != -1) {
			Leave("<GAME_SCENE>");
		}
	}
	```
	where `<GAME_SCENE>` is the name of the game state's scene to load.

> **NOTE:** The states implementing these actions **must have their `leavable` attribute set**, to be allowed to leave the current global graph when calling `Leave`.


#### GLobal Actions in Game Graph

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
!!!! TODO !!!!
if no global graph, ...
can use GlobalDataManager in game graph, ...(see Global Manager)
+ explain purpose and usage...
REWRITE FROM:
- in game states
    - possibility to "load global" (to init the game data, loaded when coming from global graph)
allow to not have global graph
+ also allowing to test a game state/scene without to start from global graph.

by calling NewGame or LoadGame

NOT RELATED (!?)
(TODO: check if put details here or leave in file...)
        "InitGame" script in "Tests/Game" (see script for use details).
)
!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!


### Game Actions

TODO:...
specific game states
... override `GameStateController` script and add specific methods for additional actions.


as seen previously,
there are some common expected actions(TODO: LINK?) which can be executed in any state of the game graph.
Some actions however must be handled by a "*level*" state (TODO: LINK LEVEL), which will have its own state controller: the **Level Controller**(TODO: LINK LEVEL CONTROLLER).


...some game specific actions:

* `StartLevel`: Start a level.\
    Typically in a "map" state if exists.\
	Steps:
	* Set the current level by specifying its number.\
	* Trigger the transition, for example to a `child` state.
	```csharp
	public void StartLevel(int level) {
		GetGameData().SetLevel(level);
		LoadChildState("<GAME_STATE>");
	}
	```
	where `<GAME_SCENE>` is the name of the game state to transit to.

- Check Game Complete: In last state related to level in "success" branch (or default if no success/fail branches).
    => In "level", if transition 'WIN_STOP_TRANSITION_STATE' (auto generated)
    bool GetGameData().IsGameComplete()

- Check Game Over: In last state related to level in "fail" branch.
    bool GetGameData().IsGameOver()

- Check Continue: After "Check Game Over".
    bool GetGameData().CanContinue()

!!!! TODO: mention where????
=> the 3 previous functions (IsGameComplete, IsGameOver, CanContinue) implement basic/common behavior:
- IsGameComplete: check if all levels have been completed.
- IsGameOver: check still have lives
- CanContinue: check still have continues
generally it is what we want and expect, however it can be changed if deisred (the game could be completed or over following a specific event...)
can be overridden (in game data manager) if want game specific behavior.


- Use Continue
    int GetGameData().LoseContinue()
        + do it here (+call CanContinue):
            gameData.SetLevel(-1)
            and in post level states, use 'latestLevel' to get the level (as current level is now '-1').

- Save
    GameSessionManager.Instance.SaveGame(str|int?)

* `QuitGame`: Leave the game graph and switch to the global graph.
	```csharp
    public void QuitGame() {
        Leave("<GLOBAL_SCENE>");
    }
	```
	where `<GLOBAL_SCENE>` is the name of the global state's scene to load.


# Levels

Levels are managed by a specific state in the game graph: the "*level*" state.

Generally, a game graph will only have 1 "*level*" state, managing all the levels, but it is possible to have more than 1 if specific transitions are desired for some levels.
(TODO: describe/show example - here or in annexes?)

The order and hierachy of levels in the game are defined by a **level tree**(TODO: LINK LEVEL TREE).

Each level must have an associated **level scene**(TODO: LINK LEVEL SCENES), which is also specified in the level tree.

Levels are expected to execute a number of actions, requiring a specific state controller script: the **Level Controller**(TODO: LINK LEVEL CONTROLLER).


## Level Tree

TODO: explain level tree stuff (xml, level files, etc.)

- 'levels.xml'
	each level has an associated scene
	but a same scene can be shared by several levels.
	=> `Game Levels` in game manager prefab


- scene file

- data (optional)
TODO: adapt (string, string)
	=> can be anything, depending on the level ~implementation.
		typically 'high-level' description files, providing links to other resources associated to the level.
		eg tilemap files, animation sequences, dialogs, etc.
	(These files can thus in turn point to other files)

- ...?


levels.xml:
```xml
<levels>
	<level id="<LEVEL_ID>" name="<LEVEL_NAME>" scene="<LEVEL_SCENE>" startup="<LEVEL_STARTUP>">
	<level id="1" name="Level 1" scene="Level1" startup="true">
		<data
			key1="value1"
			key2="value2"
			...
		/>
		<nextLevels>
			<nextLevel id="<LEVEL_ID>"/>
			...
		</nextLevels>
	</level>
	...
</levels>
```

	Params:
	- id: unique (int)
	- name: arbitrary (string)
	- scene: scene file name (string)
	- startup: available at game start (bool)
		=> optional, default: "true"
	- data: dictionary
		=> optional, default: empty dict
	- nextLevels
		=> optional, default: empty list

	Example:
		<LEVEL_ID>		1
		<LEVEL_NAME>		Level 1
		<LEVEL_SCENE>		Level1
		<LEVEL_STARTUP>		true


### Map State

...
optional but common state

presents all the levels (from level tree)
can be a simple list of levels, but can also be more complex like an interactive map or even a fully playable overworld.

generate map using methods from the game data manager
for example, can determine available levels depending on the current level and the already completed levels using `GetAvailableLevels`.

A "*map*" state should have its own state controller, and at least handle the `StartLevel` and `QuitGame` actions.


TODO:
IF NO MAP STATE (eg directly LEVEL state),
must handle SetLevel + GetNextLevels or GetAvailableLevels...


## Level Scenes
TODO...

scene names defined in 'levels.xml'

(TODO: CHECK FOR LEVEL NAMES! - NOT ANYMORE THE CASE (?)
OLD?    ! - Except for the level scenes, which must be "level_<nb>", where <nb> corresponds to the numbers used in the map scene.<br>
)

(...as mentionned previously,
a level must have an associated level scene.
but several (or all) levels can also share a single scene.

=> content can vary on current level, or could be exactly the same...
)


In order to be functional, a level scene must contain a game manager game oobject(TODO: LINK GAME MANAGER?), with a specific state controller script handling the level's specific actions: the **Level Controller**(TODO: LINK LEVEL CONTROLLER).


## Level Controller

TODO:...

The `EndLevel` and `QuitLevel` actions must be handled in the "*level*" state directly,
generally differentiate success and failure.




Implementation examples of game specific actions directly related to levels:

* `EndLevelSuccess`:\
	Should be done in a "*level*" state.\
	Steps:
	* Update the current level's status to commpleted.
	* Save changes to game data.
	* Make additional operations.
	* Trigger the transition, for example to a `child` state.
	```csharp
	protected void EndLevelSuccess(bool success) {
		GetGameData().SetLevelCompleted();
		GetGameData().CommitChanges();

		// other success specific operations...

		LoadChildState("<GAME_STATE>");
	}
	```
* `EndLevelFail`:\
	Should be done in a "*level*" state.\
	Steps:
	* Update the game data b yremoving a player's life.
	* Save changes to game data.
	* Make additional operations.
	* Trigger the transition, for example to a `child` state.
	```csharp
	protected void EndLevelFail(bool success) {
		GetGameData().LoseLife();
		GetGameData().CommitChanges();

		// other success specific operations...

		LoadChildState("<GAME_STATE>");
	}
	```
* `QuitLevel`: Exit the current level.\
	Should be done in a "*level*" state.\
	```csharp
    public void QuitLevel() {
		LoadChildState("<GAME_STATE>");
    }
	```
	where `<GAME_STATE>` is the name of the game state to switch to, typically a "map" state.

this allows basic level actions handling.


as seen previously, other game actions can be handled in other states, but can also be handled in the "*level*" state.
A more complex and generic state controller can be implemented if want to handle all the game specific actions in the same method.
for example, the level's end can be handled in different ways:
- end without differentiating success or failure
- end with success or failure
Additionally, when failing a level, it can be handled in different ways:
- failing
- failing with lives left
- failing with no lives left
the last case can be handled even further depending if still have continues or not.

(((
TODO: here?
=> move to samples (provide the project in samples)
Ready level script that can serve as base.

Must set fields according to graph:
(pretty crappy, and could be automated by the graphview tool - maybe later...)
- Fields when ending a level successfully:
	- WIN_TRANSITION_STATE: Global transition when succeeding a level.
	- if not set, other less generic transitions can be used:
		- WIN_NEXT_TRANSITION_STATE: Transition when succeeding a level, and there are still levels after (the game is not finished).
		- WIN_END_TRANSITION_STATE: Transition when succeeding a level, but there are no more levels after (the game is finished).
- Fields when failing a level:
	- LOSE_TRANSITION_STATE: Global transition when failing a level.
	- if not set, other less generic transitions can be used:
		- LOSE_GAME_OVER_TRANSITION_STATE: Transition when failing a level, and the game is over (lost all lives).
		- if not set, other less generic transitions can be used:
			- LOSE_END_TRANSITION_STATE: Transition when failing a level, and the game is over (no more continues).
			- LOSE_CONTINUE_TRANSITION_STATE: Transition when failing a level, the game is over, but there is still a continue.
		- LOSE_RETRY_TRANSITION_STATE: Transition when failing a level, but there are still lives.
- Fields when quitting a level (before success or failure):
	- QUIT_TRANSITION_STATE: Global transition when quitting a level.
	- if not set, other less generic transitions can be used:
		- QUIT_LEVEL_TRANSITION_STATE: Transition when quitting a level, and want to stay in the game graph (e.g.: going back to the map).
		- QUIT_GAME_TRANSITION_STATE: Transition when quitting a level, and want to leave the game graph (e.g.: going back to main menu).

Accepted values (strings):
- state name: Corresponds to a call to `LoadChildState`, transitionning to a child state (transition must exist in graph).
- "<next>": Corresponds to a call to `End`, transitionning to the next state or else back to its parent state.
- "<quit>": Corresponds to a call to `Leave`, exiting the current graph and transitionning to the specified scene. (TODO!)

Default empty values, meaning the transition is ignored.
~However, most specialized ones still make a transition:
- `End` (go to next state or back to parent):
	- WIN_NEXT_TRANSITION_STATE
	- WIN_END_TRANSITION_STATE
	- LOSE_END_TRANSITION_STATE
	- LOSE_CONTINUE_TRANSITION_STATE
	- LOSE_RETRY_TRANSITION_STATE
	- QUIT_LEVEL_TRANSITION_STATE
- `Leave` (exit graph and go to 'leave graph' state):
	! - TODO: CHANGE! Must handle multiple 'leave graph' states!
	- QUIT_GAME_TRANSITION_STATE
)))


# Samples

!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
TODO: CHANGE ALL

?
	## Basic (Starter?)
		### Global
			#### Data
				xml + manager
			#### State Graph
		### Game
			#### Data
				xml + manager
			#### State Graph
			#### Levels
	## Full (Ready?)
		### Global
			#### Data
				xml + manager
			#### State Graph
			#### Scenes & State Controllers
		### Game
			#### Data
				xml + manager
			#### State Graph
			#### Scenes & State Controllers
			#### Levels


!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

Ready to use graphs and managers...
+full project
...

## Global

### State Graph

The default graph for global states can be represented the following way:
!["Default Global Graph"](./docs/images/global_graph.png "Default Global Graph")
(TODO: redo image + add border or transparent bkg)

where:
* **LOGO**: The initial state when starting the application. Display the studio logo.\
	Transition to the *next* state ("**INTRO**").
* **INTRO**: Display the game introduction video.\
	Transition to the *next* state ("**MENU**").
* **MENU**: Handle the game's main menu, allowing to:
	* Start a new game (transition to child state "**NEW_GAME**").
	* Continue the current game (if available, transition to the state specified for `exitScene`, which should be in the game graph).
	* Load a previously saved game (transition to child state "**LOAD_GAME**").
	* Go to the options screen (transition to child state: "**OPTIONS**").
	* See the game's credits (transition to "**CREDITS**").
	* Quit the game (transition to the next state: "**QUIT**").
* **NEW_GAME**: Start a new game, with the possibility to handle different difficulty levels.\
	Transition to the state specified for `exitScene` (which should be in the game graph), or back to its parent ("**MENU**").
* **LOAD_GAME**: Load a previously saved game.\
	Transition to the state specified for `exitScene` (which should be in the game graph), or back to its parent ("**MENU**").
* **OPTIONS**: Handle the game options screen.\
	Transition back to its parent ("**MENU**").
* **CREDITS**: Display the game's credits screen.\
	Transition back to its parent ("**MENU**").
* **QUIT**: Quit the game.\
	Terminate the application.

### Scenes & State Controllers
(TODO: title OK?)

(TODO: detail scripts)


## Game

### State Graph

Default graph for game states:
TODO: add screenshot of default graph (from graphview project)!

TODO: explain better...
in this graph:
TODO: do as for global graph
- GAME_INTRO
	next="MAP"
- MAP
	children
		BEGIN_ANIM
		QUIT
- BEGIN_ANIM
	next="BRIEFING
- BRIEFING
	LEVEL
- LEVEL
	children
		END_ANIM
		END_ANIM_FAIL
		QUIT
- END_ANIM
	next="DEBRIEFING"
- DEBRIEFING
	children
		GAME_END
- END_ANIM_FAIL
	next="DEBRIEFING_FAIL
- DEBRIEFING_FAIL
	children
		GAME_OVER
- GAME_END
	CREDITS
- CREDITS
	QUIT
- GAME_OVER
	next="CONTINUE"
	children
		QUIT
- CONTINUE
	children
		QUIT
- QUIT


### Scenes & State Controllers
(TODO: title OK?)

(TODO: detail scripts)


(TODO: mention? where?
automatic transitions:
A "Timer" (script) can be added (global or not depending on graph) if want the state to be left after a period of time
)


TODO: WHERE?
> **NOTE:** For scene names parameters, the custom property `SceneProperty` can be added to the script, and the scene name obtained from its `name` attribute.



-------------------------------------
OLD: check or delete...

Optional:
(version 2):
    - can add the "LevelManager" script (version 2), a "LevelController" script to the manager, and a "UI Canvas" prefab (containing a "HudController" script) to the scene, for generic game stuff (health, points, lives)<br>
    - for testing purpose, the prefab object "UI Tests Canvas" can be added to the "UI Canvas" object (as child), and the "LevelManager" object instance must be set in the script public parameters. It will add 4 buttons for the following actions: win level, lose level, get hit, add points.<br>


----------------------

TODO: REMOVE! (?)

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
