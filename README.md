# UNITY STATE ENGINE

State machine implementing a state transition graph to use for a classic game scenes structure.<br>
... (detail default states/graph, how to change, how it works, files required and generated, scenes, etc.)<br>


## SETUP

Copy the "Engine" directory in the "Assets" folder of the Unity project.<br>


# EXAMPLES

## STATE ENGINE DEMO

Unity project showcasing how to use the state engine in its most basic way.<br>
It is the minimal implementation required in order to use the engine.<br>

It can be used as a template for the creation of a new game.<br>

Before using the Unity project, the engine must be added to the project.<br>
Look at the section "Adding the engine" for details.<br>



## STATE ENGINE DEMO EXTENDED

Unity project showcasing how to use the state engine.<br>
It implements a game controller with basic game mechanics, such as points, health and a simple UI allowing to simulate game events.<br>

# ADDING THE ENGINE

There are 2 ways to add the engine to a Unity project:<br>

1. Copy the content of the "Engine" directory to a new "StateEngine" folder in the "Assets" folder of the Unity project.<br>

2. Run the script "set_links.bat" in "Examples" to create symbolic links to the engine (and thus not working with a copy)<br>
  => This is useful if working with Git (...)<br>
  ! - The script must be run with admin rights! (and is only for Windows - Linux script coming soon)<br>
