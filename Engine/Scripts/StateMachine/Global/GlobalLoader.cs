using UnityEngine;
using System.Xml;
using System;
using System.Collections.Generic;

public static class GlobalLoader : object
{

    private static GlobalStateId ToGlobalEnum(String value)
    {
        try
        {
            return (GlobalStateId)System.Enum.Parse(typeof(GlobalStateId), value);
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("ToGlobalEnum: Can't convert {0} to enum.", value);
            return GlobalStateId.NONE;
        }
    }

    private static bool ToBool(String value)
    {
        try
        {
            return System.Boolean.Parse(value);
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("ToBool: Can't convert {0} to bool.", value);
            return false; //?
        }
    }


    public static GlobalState[] LoadGlobalStateGraph(String filename)
    {
        GlobalState[] globalStates = new GlobalState[Enum.GetNames(typeof(GlobalStateId)).Length];
        globalStates[(int)GlobalStateId.NONE] = new GlobalState(GlobalStateId.NONE, null, GlobalStateId.NONE);

        TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);
        XmlNodeList states = xmlDoc.GetElementsByTagName("state");

        foreach (XmlNode state in states)
        {
            GlobalStateId id = GlobalStateId.NONE;
            String scene = null;
            GlobalStateId next = GlobalStateId.NONE;
            bool restartable = true;

            foreach (XmlAttribute attribute in state.Attributes)
            {
                if (attribute.Name.Equals("id"))
                {
                    id = ToGlobalEnum(attribute.Value);
                }
                else if (attribute.Name.Equals("scene"))
                {
                    scene = attribute.Value;
                }
                else if (attribute.Name.Equals("next"))
                {
                    next = ToGlobalEnum(attribute.Value);
                }
                else if (attribute.Name.Equals("restartable"))
                {
                    restartable = ToBool(attribute.Value);
                }
            }

            if (id == GlobalStateId.NONE || (scene == null || "".Equals(scene)))
            {
                Debug.Log("Invalide state!");
                continue;
            }

            GlobalState globalState = new GlobalState(id, scene, next);
            globalState.setRestartable(restartable);


            foreach (XmlNode childList in state.ChildNodes)
            {
                if (childList.Name == "children")
                {
                    foreach (XmlNode child in childList.ChildNodes)
                    {
                        foreach (XmlAttribute attribute in child.Attributes)
                        {
                            if (attribute.Name.Equals("id"))
                            {
                                GlobalStateId value = ToGlobalEnum(attribute.Value);
                                if (value != GlobalStateId.NONE)
                                {
                                    globalState.AddChild(value);
                                }
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            globalStates[(int)id] = globalState;
        }

// !!!! TODO: check that all states have been allocated !!!!
//...

        return globalStates;
    }

}
