using System;
using System.Collections.Generic;

public class GameState
{
    /*
        id:
        scene:
        isLevel: if "true", the scene to load depends on the "level" number
            ! - only 1 element can have this property set to "true"!
        next:
        children:
        restartable: if "true" and hve children, will go to "next" state when coming back from a children state
    */
// !!!! ???? TODO: public with direct access, or private with getters/setters ? ???? !!!!
// (or properties?)
    public GameStateId id;
    public String scene;
    public bool isLevel;
    public GameStateId next;
    public List<GameStateId> children;
    public bool restartable;


    public GameState(GameStateId id, String scene, GameStateId next)
    {
        this.id = id;
        this.scene = scene;
        this.next = next;

        this.isLevel = false;
        this.children = null;
        this.restartable = true;
    }

    public void AddChild(GameStateId stateId)
    {
        if (children == null)
        {
            children = new List<GameStateId>();
        }
        children.Add(stateId);
    }

    //needed?
    public void setIsLevel(bool isLevel)
    {
        this.isLevel = isLevel;
    }

    //needed?
    public void setRestartable(bool restartable)
    {
        this.restartable = restartable;
    }

}
