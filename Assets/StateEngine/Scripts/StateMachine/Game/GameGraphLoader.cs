using UnityEngine;
using System.Xml;
using System.Collections.Generic;

namespace StateEngine {

    public class GameGraphLoader : IGraphLoader {

        public bool HaveLevelState { get; private set; }


        protected class GameStateData : StateData {
            public bool isLevel;

            public GameStateData()
                    : base() {
                isLevel = false;
            }
        }


        public GameGraphLoader(TextAsset xmlGraph)
                : base(xmlGraph) {
            HaveLevelState = false;
        }

        protected override StateData CreateStateData() {
            return new GameStateData();
        }

        protected override State[] CreateStateArray(int length) {
            GameState[] states = new GameState[length + 1]; //+NONE state
            states[StateIds.NONE] = (GameState)CreateState(new GameStateData());
            return states;
        }

        protected override State CreateState(StateData data) {
            GameState state = new GameState(data.id, data.scene, data.next);
            state.SetRestartable(data.restartable);
            state.SetLeavable(data.leavable);
            state.SetIsLevel(((GameStateData)data).isLevel);
            return state;
        }

        protected override void GetAttributes(StateData data, XmlAttributeCollection attributes) {
            base.GetAttributes(data, attributes);

            XmlAttribute isLevelAttribute = attributes["isLevel"];
            bool isLevel = (isLevelAttribute != null) && ToBool(isLevelAttribute.Value, false);

            if (isLevel)
                HaveLevelState = true;

            ((GameStateData)data).isLevel = isLevel;
        }

        protected override bool CheckAttributes(StateData data) {
            if (!((GameStateData)data).isLevel && ((data.scene == null) || "".Equals(data.scene))) {
                // TODO: Exception
                Debug.Log("Invalid state!");
                return false;
            }
            return true;
        }


        public static Dictionary<int, LevelNode> LoadLevelGraph(TextAsset xmlGameLevels) {
            Dictionary<int, LevelNode> nodes = new();

            XmlDocument xmlDoc = new();
            xmlDoc.LoadXml(xmlGameLevels.text);

            XmlNodeList levelsNodes = xmlDoc.GetElementsByTagName("levels");
            if (levelsNodes.Count != 1)
                // TODO: XmlException?
                throw new XmlException("Invalid XML: A single 'levels' node is required!");

            XmlNodeList levelNodes = levelsNodes[0].ChildNodes;
            // TODO: use instead 'SelectNodes("level")'
            foreach (XmlNode levelNode in levelNodes) {
                int id = -1;
                string scene = null;
                string name = null;
                bool startup = false;

                // TODO: Add 'data' for level data (xml file)
                foreach (XmlAttribute attribute in levelNode.Attributes) {
                    switch (attribute.Name) {
                        case "id":
                            id = int.Parse(attribute.Value);
                            break;
                        case "scene":
                            scene = attribute.Value;
                            break;
                        case "name":
                            name = attribute.Value;
                            break;
                        case "startup":
                            startup = bool.Parse(attribute.Value);
                            break;
                    }
                }

                if ((id == -1) || string.IsNullOrEmpty(scene)) {
                    // TODO: Exception (+separate cases)
                    throw new XmlException("Invalid XML: Missing attributes for 'node'!");
                }

                name ??= id.ToString();

                LevelNode node = new(id, scene, name, startup);

                XmlNode dataNode = levelNode.SelectSingleNode("data");
                if (dataNode != null) {
                    Dictionary<string, string> data = new();
                    foreach (XmlAttribute attribute in dataNode.Attributes) {
                        data[attribute.Name] = attribute.Value;
                    }
                    node.SetData(data);
                }

                XmlNode nextLevelsNode = levelNode.SelectSingleNode("nextLevels");
                if (nextLevelsNode != null) {
                    foreach (XmlNode nextNode in nextLevelsNode.ChildNodes) {
                        XmlAttribute nextId = nextNode.Attributes["id"];
                        int nextIdValue = int.Parse(nextId.Value);
                        node.AddNext(nextIdValue);
                    }
                }

                nodes.Add(id, node);
            }
            return nodes;
        }

    }

}
