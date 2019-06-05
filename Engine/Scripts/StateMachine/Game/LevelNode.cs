using System.Collections.Generic;

public class LevelNode : object {

//TODO: set as properties
// => SOME FIELDS NEED TO BE READ-ONLY !!!! { get; private set; }
    public int id;
    public string scene;
    public string name;
    public string beginAnim;
    public string endAnim;
    public string endAnimFail;
    public bool startup;
    public List<int> next = null;
    public bool completed = false;

    public LevelNode(int id, string scene, string name)
    {
        this.id = id;
        this.scene = scene;
        this.name = name;
    }

    public LevelNode(int id, string scene, string name, string beginAnim, string endAnim, string endAnimFail, bool startup) {
        this.id = id;
        this.scene = scene;
        this.name = name;
        this.beginAnim = beginAnim;
        this.endAnim = endAnim;
        this.endAnimFail = endAnimFail;
        this.startup = startup;
    }

    public void AddNext(int nextId) {
        if (next == null) {
            next = new List<int>();
        }
        next.Add(nextId);
    }

}
