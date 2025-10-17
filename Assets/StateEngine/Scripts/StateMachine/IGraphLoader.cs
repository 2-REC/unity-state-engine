using UnityEngine;
using System.Xml;
using System;

public abstract class IGraphLoader : object {

    //private readonly string filename;
    private readonly TextAsset xmlGraph;

    protected class StateData {
        public int id;
        public string scene;
        public int next;
        public bool restartable;
        public bool leavable;

        public StateData() {
            id = StateIds.NONE;
            scene = null;
            next = StateIds.NONE;
            restartable = false;
            leavable = false;
        }
    }


    protected static bool ToBool(string value, bool def) {
        try {
            return bool.Parse(value);
        } catch (Exception) {
            Debug.LogErrorFormat("ToBool: Can't convert {0} to bool.", value);
            return def;
        }
    }

/*
    public IGraphLoader(string filename) {
        this.filename = filename;
    }
*/
    public IGraphLoader(TextAsset xmlGraph) {
        this.xmlGraph = xmlGraph;
    }

    public State[] LoadStateGraph() {
        //TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlGraph.text);

        XmlNodeList statesNodes = xmlDoc.GetElementsByTagName("states");
        if (statesNodes.Count != 1)
            // TODO: XmlException?
            throw new XmlException("Invalid XML: A single 'states' node is required!");

        XmlNodeList stateNodes = statesNodes[0].ChildNodes;

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
            // TODO: merge both methods?
            StateData data = CreateStateData();
            GetAttributes(data, stateNode.Attributes);

            if (data.id == StateIds.NONE) {
                Debug.LogError("Invalid state: an ID is required! - ignoring");
                return null;
            }

            // TODO: exception instead (inside method)?
            if (!CheckAttributes(data))
                continue;

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
        State[] states = new State[length + 1]; // + NONE state
        states[StateIds.NONE] = CreateState(new StateData());
        return states;
    }

    protected virtual State CreateState(StateData data) {
        State state = new State(data.id, data.scene, data.next);
        state.SetRestartable(data.restartable);
        state.SetLeavable(data.leavable);
        return state;
    }

    protected virtual void GetAttributes(StateData data, XmlAttributeCollection attributes) {
        foreach (XmlAttribute attribute in attributes) {
            // TODO: replace with 'switch'
            if (attribute.Name.Equals("id")) {
                data.id = StateIds.Index(attribute.Value);
            } else if (attribute.Name.Equals("scene")) {
                data.scene = attribute.Value;
            } else if (attribute.Name.Equals("next")) {
                data.next = StateIds.Index(attribute.Value);
            } else if (attribute.Name.Equals("restartable")) {
                data.restartable = ToBool(attribute.Value, data.restartable);
            } else if (attribute.Name.Equals("leavable")) {
                data.leavable = ToBool(attribute.Value, data.leavable);
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
