using System;
using System.Collections.Generic;

public class GlobalState
{
    //?
    public GlobalStateId id;
    public String scene;
    public GlobalStateId next;
    public List<GlobalStateId> children;
    public bool restartable;


    public GlobalState(GlobalStateId id, String scene, GlobalStateId next)
    {
        this.id = id;
        this.scene = scene;
        this.next = next;
        this.children = null;
        this.restartable = true;
    }

    public void AddChild(GlobalStateId stateId)
    {
        if (children == null)
        {
            children = new List<GlobalStateId>();
        }
        children.Add(stateId);
    }

    public void setRestartable(bool restartable)
    {
        this.restartable = restartable;
    }

}
