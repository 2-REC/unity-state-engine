using UnityEngine;
using System.Xml;
using System;

public abstract class IGraphLoader : object {

    private readonly string filename;

    protected class StateData {
        public int id;
        public string scene;
        public int next;
        public bool restartable;

        public StateData() {
            id = StateIds.NONE;
            scene = null;
            next = StateIds.NONE;
            restartable = true;
        }
    }


    protected static bool ToBool(string value, bool def) {
        try {
            return bool.Parse(value);
        }
        catch (Exception) {
            Debug.LogErrorFormat("ToBool: Can't convert {0} to bool.", value);
            return def;
        }
    }


    public IGraphLoader(string filename) {
        this.filename = filename;
    }

    public State[] LoadStateGraph() {
        TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);
        XmlNodeList stateNodes = xmlDoc.GetElementsByTagName("state");

        // First parse to get names of states
        StateIds.Reset();
        foreach (XmlNode stateNode in stateNodes) {
            StateIds.Add(stateNode.Attributes["id"].Value);
        }

        State[] states = GetStates(stateNodes);

//TODO: check that all states have been allocated
//...

        return states;
    }

    protected State[] GetStates(XmlNodeList stateNodes) {
        State[] states = CreateStateArray(stateNodes.Count);

        foreach (XmlNode stateNode in stateNodes) {
            StateData data = CreateStateData();
            GetAttributes(data, stateNode.Attributes);

            if (data.id == StateIds.NONE) {
                Debug.LogError("Invalid state: an ID is required! - ignoring");
                return null;
            }

            CheckAttributes(data);

            State state = CreateState(data);

            AddChildren(state, stateNode.ChildNodes);

            states[data.id] = state;
        }
        return states;
    }

    protected virtual StateData CreateStateData() {
        return new StateData();
    }

    protected virtual State[] CreateStateArray(int length) {
        State[] states = new State[length + 1]; //+NONE state
//Debug.Log("nb states: " + length);
        states[StateIds.NONE] = CreateState(new StateData());
        return states;
    }

    protected virtual State CreateState(StateData data) {
        State state = new State(data.id, data.scene, data.next);
        state.SetRestartable(data.restartable);
        return state;
    }

    protected virtual void GetAttributes(StateData data, XmlAttributeCollection attributes) {
        foreach (XmlAttribute attribute in attributes) {
            if (attribute.Name.Equals("id")) {
                data.id = StateIds.Index(attribute.Value);
            }
            else if (attribute.Name.Equals("scene")) {
                data.scene = attribute.Value;
            }
            else if (attribute.Name.Equals("next")) {
                data.next = StateIds.Index(attribute.Value);
            }
            else if (attribute.Name.Equals("restartable")) {
                data.restartable = ToBool(attribute.Value, data.restartable);
            }
        }
    }

    protected void AddChildren(State state, XmlNodeList childNodes) {
        foreach (XmlNode childList in childNodes) {
            if (childList.Name == "children") {
                foreach (XmlNode child in childList.ChildNodes) {
                    foreach (XmlAttribute attribute in child.Attributes) {
                        if (attribute.Name.Equals("id")) {
                            int value = StateIds.Index(attribute.Value);
                            if (value != StateIds.NONE) {
                                state.AddChild(value);
                            }
                            break;
                        }
                    }
                }
                break;
            }
        }
    }


    protected abstract bool CheckAttributes(StateData data);

}
