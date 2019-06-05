using System.Collections.Generic;

//TODO: Should be "IState" abstract class, and derived to "GlobalState"

public class State {

    public int Id { get; private set; }
    public string Scene { get; private set; }
    public int Next { get; private set; }
    public List<int> Children { get; private set; }
    public bool Restartable { get; private set; }


    public State(int id, string scene, int next) {
        Id = id;
        Scene = scene;
        Next = next;

        Children = null;
        Restartable = true;
    }

    public void AddChild(int stateId) {
        if (Children == null) {
            Children = new List<int>();
        }
        Children.Add(stateId);
    }

    public void SetRestartable(bool restartable) {
        Restartable = restartable;
    }

}
