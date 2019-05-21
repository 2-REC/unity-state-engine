using UnityEngine;
using System.Xml;
using System;
using System.Collections.Generic;

public static class GameLoader : object
{
    private static GameStateId ToGameEnum(String value)
    {
        try
        {
            return (GameStateId)System.Enum.Parse(typeof(GameStateId), value);
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("ToGameEnum: Can't convert {0} to enum.", value);
            return GameStateId.NONE;
        }
    }

    private static bool ToBool(String value, bool def)
    {
        try
        {
            return System.Boolean.Parse(value);
        }
        catch (System.Exception)
        {
            Debug.LogErrorFormat("ToBool: Can't convert {0} to bool.", value);
            return def;
        }
    }


    public static GameState[] LoadGameStateGraph(String filename)
    {
        GameState[] gameStates = new GameState[Enum.GetNames(typeof(GameStateId)).Length];
        gameStates[(int)GameStateId.NONE] = new GameState(GameStateId.NONE, null, GameStateId.NONE);

        TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);
        XmlNodeList states = xmlDoc.GetElementsByTagName("state");

        foreach (XmlNode state in states)
        {
            bool haveLevel = false;

            GameStateId id = GameStateId.NONE;
            String scene = null;
            GameStateId next = GameStateId.NONE;
            bool isLevel = false;
            bool restartable = true;

            foreach (XmlAttribute attribute in state.Attributes)
            {
                if (attribute.Name.Equals("id"))
                {
                    id = ToGameEnum(attribute.Value);
                }
                else if (attribute.Name.Equals("scene"))
                {
                    scene = attribute.Value;
                }
                else if (attribute.Name.Equals("next"))
                {
                    next = ToGameEnum(attribute.Value);
                }
                else if (attribute.Name.Equals("isLevel"))
                {
                    isLevel = ToBool(attribute.Value, false);
                }
                else if (attribute.Name.Equals("restartable"))
                {
                    restartable = ToBool(attribute.Value, true);
                }
            }

            if ((id == GameStateId.NONE) ||
                    (!isLevel && ((scene == null) || "".Equals(scene))))
            {
                Debug.LogError("Invalid state!");
                continue;
            }
            if (isLevel)
            {
                if (haveLevel)
                {
                    Debug.LogError("Invalid state: Can't have more than 1 state with 'isLevel' at 'true'!");
                    continue;
                }
                haveLevel = true;
            }


            GameState gameState = new GameState(id, scene, next);
            gameState.setIsLevel(isLevel);
            gameState.setRestartable(restartable);


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
                                GameStateId value = ToGameEnum(attribute.Value);
                                if (value != GameStateId.NONE)
                                {
                                    gameState.AddChild(value);
                                }
                                break;
                            }
                        }
                    }
                    break;
                }
            }

            gameStates[(int)id] = gameState;
        }

// !!!! TODO: check graph validity !!!!
// (used states are allocated, etc.)

        return gameStates;
    }

    public static Dictionary<int, LevelNode> LoadLevelGraph(String filename)
    {
        Dictionary<int, LevelNode> nodes = new Dictionary<int, LevelNode>();

        TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);
        XmlNodeList levels = xmlDoc.GetElementsByTagName("level");

        foreach (XmlNode level in levels)
        {
            int id = -1;
            string scene = null;
            string name = null;
            string beginAnim = null;
            string endAnim = null;
            string endAnimFail = null;
            bool startup = false;

            foreach (XmlAttribute attribute in level.Attributes)
            {
                if (attribute.Name.Equals("id"))
                {
                    id = int.Parse(attribute.Value);
                }
                else if (attribute.Name.Equals("scene"))
                {
                    scene = attribute.Value;
                }
                else if (attribute.Name.Equals("name"))
                {
                    name = attribute.Value;
                }
                else if (attribute.Name.Equals("beginAnim"))
                {
                    beginAnim = attribute.Value;
                }
                else if (attribute.Name.Equals("endAnim"))
                {
                    endAnim = attribute.Value;
                }
                else if (attribute.Name.Equals("endAnimFail"))
                {
                    endAnimFail = attribute.Value;
                }
                else if (attribute.Name.Equals("startup"))
                {
                    startup = Boolean.Parse(attribute.Value);
                }
            }

            if (id == -1 || (scene == null || "".Equals(scene)))
            {
                Debug.Log("Invalid level node!");
                continue;
            }

            if (name == null)
            {
                name = id.ToString();
            }

            LevelNode node = new LevelNode(id, scene, name, beginAnim, endAnim, endAnimFail, startup);


            foreach (XmlNode childList in level.ChildNodes)
            {
                if (childList.Name == "next")
                {
                    foreach (XmlNode child in childList.ChildNodes)
                    {
                        foreach (XmlAttribute attribute in child.Attributes)
                        {
                            if (attribute.Name.Equals("id"))
                            {
                                int value = int.Parse(attribute.Value);
                                if (value != -1)
                                {
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
