using UnityEngine;
using System.Xml;
using System;
using System.Collections.Generic;

public class GameGraphLoader : IGraphLoader {

    private bool haveLevel;


    protected class GameStateData : StateData {
        public bool isLevel;

        public GameStateData()
                : base() {
            isLevel = false;
        }
    }


    public GameGraphLoader(string filename)
            : base(filename) {
        haveLevel = false;
    }

    protected override StateData CreateStateData() {
        return new GameStateData();
    }

    protected override State[] CreateStateArray(int length) {
        GameState[] states = new GameState[length + 1]; //+NONE state
//Debug.Log("nb states: " + length);
        states[StateIds.NONE] = (GameState)CreateState(new GameStateData());
        return states;
    }

    protected override State CreateState(StateData data) {
        GameState state = new GameState(data.id, data.scene, data.next);
        state.SetRestartable(data.restartable);
        state.SetIsLevel(((GameStateData)data).isLevel);
        return state;
    }

    protected override void GetAttributes(StateData data, XmlAttributeCollection attributes) {
        base.GetAttributes(data, attributes);

        bool isLevel = false;

        foreach (XmlAttribute attribute in attributes) {
            if (attribute.Name.Equals("isLevel")) {
                isLevel = ToBool(attribute.Value, false);
            }
        }

        if (isLevel) {
            if (haveLevel) {
                throw new Exception("Invalid state: Can't have more than 1 state with 'isLevel' set to 'true'!");
            }
            haveLevel = true;
        }

        ((GameStateData)data).isLevel = isLevel;
    }

    protected override bool CheckAttributes(StateData data) {
        if (!((GameStateData)data).isLevel && ((data.scene == null) || "".Equals(data.scene))) {
            Debug.Log("Invalid state!");
            return false;
        }
        return true;
    }


    public static Dictionary<int, LevelNode> LoadLevelGraph(string filename) {
        Dictionary<int, LevelNode> nodes = new Dictionary<int, LevelNode>();

        TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);
        XmlNodeList levels = xmlDoc.GetElementsByTagName("level");

        foreach (XmlNode level in levels) {
            int id = -1;
            string scene = null;
            string name = null;
            string beginAnim = null;
            string endAnim = null;
            string endAnimFail = null;
            bool startup = false;

            foreach (XmlAttribute attribute in level.Attributes) {
                if (attribute.Name.Equals("id")) {
                    id = int.Parse(attribute.Value);
                }
                else if (attribute.Name.Equals("scene")) {
                    scene = attribute.Value;
                }
                else if (attribute.Name.Equals("name")) {
                    name = attribute.Value;
                }
                else if (attribute.Name.Equals("beginAnim")) {
                    beginAnim = attribute.Value;
                }
                else if (attribute.Name.Equals("endAnim")) {
                    endAnim = attribute.Value;
                }
                else if (attribute.Name.Equals("endAnimFail")) {
                    endAnimFail = attribute.Value;
                }
                else if (attribute.Name.Equals("startup")) {
                    startup = Boolean.Parse(attribute.Value);
                }
            }

            if ((id == -1) || (scene == null) || "".Equals(scene)) {
                Debug.Log("Invalid level node!");
                continue;
            }

            if (name == null) {
                name = id.ToString();
            }

            LevelNode node = new LevelNode(id, scene, name, beginAnim, endAnim, endAnimFail, startup);


            foreach (XmlNode childList in level.ChildNodes) {
                if (childList.Name == "next") {
                    foreach (XmlNode child in childList.ChildNodes) {
                        foreach (XmlAttribute attribute in child.Attributes) {
                            if (attribute.Name.Equals("id")) {
                                int value = int.Parse(attribute.Value);
                                if (value != -1) {
                                    node.AddNext(value);
                                }
                                break;
                            }
                        }
                    }
                    break;
                }
            }
            nodes.Add(id, node);
        }
        return nodes;
    }

}
