using System.Collections.Generic;

namespace StateEngine {

    public class LevelNode {

        public int Id { get; private set; }
        public string Scene { get; private set; }
        public string Name { get; private set; }
        public bool Startup { get; private set; }
        public List<int> Next { get; private set; }

        public Dictionary<string, string> Data { get; private set; } = new();

        public bool Completed { get; set; }


        public LevelNode(int id, string scene, string name) {
            Id = id;
            Scene = scene;
            Name = name;

            Startup = false;
            Next = null;
            Completed = false;
        }

        public LevelNode(int id, string scene, string name, bool startup) {
            Id = id;
            Scene = scene;
            Name = name;
            Startup = startup;

            Next = null;
            Completed = false;
        }

        public void AddNext(int nextId) {
            Next ??= new List<int>();
            Next.Add(nextId);
        }

        public void SetData(Dictionary<string, string> data) {
            Data = data;
        }

    }

}
