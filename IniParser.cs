using System.Text;

namespace RF5_FastMining;

public class IniParser
{
    class TypeSet
    {
        public bool boolValue;
        public int intValue;
        public float floatValue;
        public string stringValue;

        public TypeSet(bool boolValue)
        {
            this.boolValue = boolValue;
            intValue = boolValue ? 1 : 0;
            floatValue = boolValue ? 1.0f : 0.0f;
            stringValue = boolValue ? "true" : "false";
        }

        public TypeSet(int intValue)
        {
            boolValue = intValue > 0;
            this.intValue = intValue;
            floatValue = intValue;
            stringValue = intValue.ToString();
        }

        public TypeSet(float floatValue)
        {
            boolValue = floatValue > 0;
            intValue = (int)floatValue;
            this.floatValue = floatValue;
            stringValue = floatValue.ToString();
        }

        public TypeSet(string stringValue)
        {
            boolValue = stringValue.Length > 0;
            int.TryParse(stringValue, out intValue);
            float.TryParse(stringValue, out floatValue);
            this.stringValue = stringValue;
        }
    }

    private Dictionary<string, Dictionary<string, TypeSet>> data = new Dictionary<string, Dictionary<string, TypeSet>>();

    public IniParser()
    {
    }

    public IniParser(string fileName)
    {
        string line = "";
        string category = "";
        using (StreamReader file = new StreamReader(fileName, Encoding.GetEncoding("utf-8")))
        {
            while ((line = file.ReadLine()) != null)
            {
                line = line.Trim();
                int delimiter = line.IndexOf(';');
                if (delimiter > -1)
                    line = line.Substring(0, delimiter).Trim();
                if (line.Length <= 0)
                    continue;

                if (line.StartsWith("[") && line.EndsWith("]"))
                {
                    category = line.Substring(1, line.Length - 2);
                    //Main.Logger.LogInfo(string.Format("category {0}", category));
                }
                else
                {
                    delimiter = line.IndexOf('=');
                    if (delimiter > -1)
                    {
                        string key = line.Substring(0, delimiter).Trim();
                        string value = line.Substring(delimiter + 1);
                        //Main.Logger.LogInfo(string.Format("category {0} key {1} value {2}", category, key, value));

                        if (!data.ContainsKey(category))
                            data.Add(category, new Dictionary<string, TypeSet>());
                        if (!data[category].ContainsKey(key))
                            data[category].Add(key, ParseValue(value));
                    }
                }
            }
        }
    }

    private TypeSet ParseValue(string value)
    {
        if (value.Length <= 0)
            return null;

        if (string.Compare(value, "true", true) == 0)
            return new TypeSet(true);
        if (string.Compare(value, "false", true) == 0)
            return new TypeSet(false);

        float fvalue;
        if (float.TryParse(value, out fvalue))
            return new TypeSet(fvalue);

        int ivalue;
        if (int.TryParse(value, out ivalue))
            return new TypeSet(ivalue);

        return new TypeSet(value);
    }

    public int GetInt(string category, string name, int defaultValue)
    {
        if (!data.ContainsKey(category) || !data[category].ContainsKey(name))
            return defaultValue;
        return data[category][name].intValue;
    }

    public float GetFloat(string category, string name, float defaultValue)
    {
        if (!data.ContainsKey(category) || !data[category].ContainsKey(name))
            return defaultValue;
        return data[category][name].floatValue;
    }

    public bool GetBool(string category, string name, bool defaultValue)
    {
        if (!data.ContainsKey(category) || !data[category].ContainsKey(name))
            return defaultValue;
        return data[category][name].boolValue;
    }

    public string GetString(string category, string name, string defaultValue)
    {
        if (!data.ContainsKey(category) || !data[category].ContainsKey(name))
            return defaultValue;
        return data[category][name].stringValue;
    }
}
