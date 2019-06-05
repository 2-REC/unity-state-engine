using System.Collections.Generic;

public class LevelNode : object {

    public int Id { get; private set; }
    public string Scene { get; private set; }
    public string Name { get; private set; }
    public List<int> Next { get; private set; }

    public string BeginAnim { get; private set; }
    public string EndAnim { get; private set; }
    public string EndAnimFail { get; private set; }
    public bool Startup { get; private set; }

    public bool completed;


    public LevelNode(int id, string scene, string name) {
        Id = id;
        Scene = scene;
        Name = name;

        Startup = false;
        Next = null;
        completed = false;
    }

    public LevelNode(int id, string scene, string name, string beginAnim, string endAnim, string endAnimFail, bool startup) {
        Id = id;
        Scene = scene;
        Name = name;
        BeginAnim = beginAnim;
        EndAnim = endAnim;
        EndAnimFail = endAnimFail;
        Startup = startup;

        Next = null;
        completed = false;
    }

    public void AddNext(int nextId) {
        if (Next == null) {
            Next = new List<int>();
        }
        Next.Add(nextId);
    }

}
