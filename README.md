==THIS VERSION IS OBSOLETE - USE https://github.com/2-REC/unity-state-engine-basic==

+ replace this with newer inwork version ("ADDITIVE")

---

UNITY STATE ENGINE
==================

State machine implementing a state transition graph to use for a classic game scenes structure.

(**TODO:** add more infos)

Only state and data management system, no game is provided.

Details about each element can be found in the [project's documentation](./docs/README.md).

(TODO: REWRITE)
Samples are provided(TODO: LINK SAMPLES - make other document?), allowing to use the engine straight away, either for testing/exploring, or to use as a base for a new game.

This document describes how to create a basic game using the engine, based on the ... sample(TODO: LINK in 'code'?).


# Usage

The "*State Graph Engine*" project was built and tested in Unity 6.

To use the engine, a number of steps are required:
* Setup the [**Unity Project**](#project-setup)
* Design the [**State Graphs**](#state-graphs)
* Define the game [**Data**](#data)
* Build the [**Graph Managers**](#graph-managers)
* Create the [**States**](#states)
* Prepare the game [**Levels**](#levels)


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

The game will contain 2 graphs: a global graph and a game graph.

Graphs are defined in XML files.
They can be created manually or by using the [unity-state-engine-graphview](https://github.com/2-REC/unity-state-engine-graphview) tool.


## Global Graph

...
=> explain default global graph (xml)
very basic graph, with minimal functionality.
- menu
- newgame
- quit

global_states.xml
    eg in 'resources' folder
    => at least 1 state, to start a new game, and to quit the graph/application


## Game Graph

...
=> explain default game graph (xml)

game_states.xml
    eg in 'resources' folder
    => at least a level state...
    if no map state: ...(setlevel, etc.)

LEVEL
SUCCESS
FAILURE
GAME_END
GAME_OVER
CONTINUE
QUIT_GAME

(no map state, directly in level state)


# Data

(...?)
(no global data...)


## Game Data

...
=> explain default game data (xml)

!!!!
add health + points in StateEngine6_TEST

- create data
	xml file (game data)
		eg in 'resources' folder
		values.xml
	scripts
		eg in 'Scripts' folder
		GlobalDataManager.cs
		GameDataManager.cs
	=> explain simple data


## Data Managers

script + prefabs (global + game)

- create data managers
	prefabs
		eg in 'Prefabs' folder
		GlobalDataManager
		GameDataManager


# Graph Managers

...
Build the 2 graph managers prefabs.
Save the new prefabs in a new `Prefabs` folder for example.


## Global Manager

Create the Global Manager prefab:

1. Create a **global data manager** prefab.
	* Either create an empty object and add a `GlobalDataManager` script component, or drag and drop the script in the *Hierarchy* panel.
	* Save the prefab . Name it `GlobalDataManager` for example.
	* Delete the instantiated prefab in the *Hierarchy* panel.
2. Create the **global graph manager** prefab:
	* Instantiate the provided `GlobalManager` prefab.\
		The prefab should already have a `GlobalStateManager` prefab set for its `Global State Manager` porperty, as well as a `GlobalStateController` script component attached to it.\
		The `GlobalStateController` component in instantiated prefabs will be replaced by an overridden script for global states requiring specific actions or behavior.
	* Set the `GlobalManager` prefab properties:
		* Set the `GlobalDataManager` prefab for the `Global Data Manager` property (the prefab itself, **NOT** an instance).
		* Set the `global_states` XML file for the `Global States Graph` property.
		* Set the `values` XML file for the `Game Data` property.
	* Save the prefab (as a variant), and delete it in the *Hierarchy* panel.

	![Global Manager](./docs/images/globalmanager.jpg "Global Manager")


## Game Manager

Create the Game Manager prefab:

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
		The prefab should already have a `GameStateManager` prefab set for its `Game State Manager` porperty, as well as a `GameStateController` script component attached to it.\
		The `GameStateController` component in instantiated prefabs will be replaced by an overridden script for game states requiring specific actions or behavior.
	* Set the `GameManager` prefab properties:
		* Set the `GameDataManager` prefab for the `Game Data Manager` property (the prefab itself, **NOT** an instance).
		* Optionally check the `Use Global Data Manager` checkbox and set the `GlobalDataManager` prefab for the `Global Data Manager` property.
		* Set the `game_states` XML file for the `Game States Graph` property.
		* Set the `values` XML file for the `Game Data` property.
		* Set the `levels` XML file for the `Game Levels` property.[*]
	* Save the prefab (as a variant), and delete it in the *Hierarchy* panel.

	![Game Manager](./docs/images/gamemanager.jpg "Game Manager")

[*]: The **level tree** is not yet defined at this point, so an empty file can be used (it will be edited later when [creating the levels](TODO: LINK LEVELS)).
**TODO:** check if OK to have no level defined at runtime?


# States

TODO: might make more sense to make section for each state (+main split global/game))?

- create states
	=> for each state
	- scene
		(create, add graph manager, etc.)
	(- state controller) => optional
		scripts
			global => override 'GlobalStateController'
			game => override 'GameStateController'
	- other specific resources/data...


## Scenes

- create scene with name + build settings (! - first one!)
- instantiate graph manager
- if necessary override state controller


## State Controllers

...


### Global States

...
actions...


### Game States

...
actions...


# Levels

- create levels (+ update XML)
	- scenes (1 per level or shared)
	- whatever the game requires...

## Level Tree

...
- create level tree
	xml file
		eg in 'resources' folder
		levels.xml
	=> can be completed later (as probably don't know all levels yet), but should at least already provide an empty file (easier for later, simply edit the file).
	=> show basic tree (3 levels) (as in StateEngine6_TEST)

TODO: change example?
levels.xml:
```xml
<?xml version="1.0" encoding="utf-8"?>
<levels>
	<level id="1" name="Level 1" scene="Level1" startup="true">
		<data
			beginAnim="..."
			endAnim="..."
			test="testvalue"
		/>
		<nextLevels>
			<nextLevel id="2"/>
		</nextLevels>
	</level>
	<level id="2" name="Level 2" scene="Level2">
		<nextLevels>
			<nextLevel id="3"/>
		</nextLevels>
	</level>
	<level id="3" name="Level 3" scene="Level3"/>
</levels>

```


## Level Controller

... (shared)


## Level Scenes

...
1 per level
(can make template, with instanced game graph manager and level controller - overridden state controller)
