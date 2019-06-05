using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class DifficultyData
{
    private const string VALUES_XML = "Xml/values";
    private const int DEFAULT_LIVES = 1;
    private const int DEFAULT_CONTINUES = 0;

    List<DifficultyValues> difficultyValues;

    public class DifficultyValues
    {
        public int lives;
        public int continues;
        public Dictionary<string, int> fields = null;

        // Default values if nothing is provided.
        public DifficultyValues()
        {
            lives = DEFAULT_LIVES;
            continues = DEFAULT_CONTINUES;
        }

        public DifficultyValues(int lives, int continues, Dictionary<string, int> fields)
        {
            this.lives = lives;
            this.continues = continues;
            this.fields = fields;
        }
    }


    public DifficultyData()
    {
        difficultyValues = Load();
    }

    public DifficultyValues GetValues(int difficulty)
    {
        return difficultyValues[difficulty];
    }


    private static List<DifficultyValues> Load()
    {
        List<DifficultyValues> values = LoadDifficultyValues(VALUES_XML);
        if (values.Count == 0)
        {
            values.Add(new DifficultyData.DifficultyValues());
        }
        return values;
    }

    private static List<DifficultyValues> LoadDifficultyValues(string filename)
    {
        List<DifficultyValues> values = new List<DifficultyValues>();
        TextAsset xmlFile = (TextAsset)Resources.Load(filename, typeof(TextAsset));
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlFile.text);

        // get common values (if any)
        Dictionary<string, int> commonFields = new Dictionary<string, int>();
        XmlNodeList commons = xmlDoc.GetElementsByTagName("common");
        foreach (XmlNode node in commons)
        {
            GetFields(node, commonFields);
        }

        // get difficulty specific values
        XmlNodeList nodes = xmlDoc.GetElementsByTagName("difficulty");
        foreach (XmlNode node in nodes)
        {
            int lives = DEFAULT_LIVES;
            int continues = DEFAULT_CONTINUES;
            Dictionary<string, int> fields = new Dictionary<string, int>(commonFields);

            foreach (XmlAttribute attribute in node.Attributes)
            {
                if (attribute.Name.Equals("lives"))
                {
                     int.TryParse(attribute.Value, out lives);
                }
                else if (attribute.Name.Equals("continues"))
                {
                    int.TryParse(attribute.Value, out continues);
                }
            }

            // get other values (if any)
            GetFields(node, fields);

            values.Add(new DifficultyValues(lives, continues, fields));
        }

        return values;
    }

    private static void GetFields(XmlNode node, Dictionary<string, int> fields)
    {
        if (node.HasChildNodes)
        {
            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.Equals("field"))
                {
                    string name = "";
                    int value = 0;
                    bool valueSet = false;
                    foreach (XmlAttribute attribute in child.Attributes)
                    {
                        if (attribute.Name.Equals("name"))
                        {
                            name = attribute.Value;
                        }
                        else if (attribute.Name.Equals("value"))
                        {
                            valueSet = int.TryParse(attribute.Value, out value);
                        }
                    }
                    if (!name.Equals("") && valueSet)
                    {
                        fields[name] = value; // this syntax allows changing the value of existing keys
                    }
                }
            }
        }
    }

}
