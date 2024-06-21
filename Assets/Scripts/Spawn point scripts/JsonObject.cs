using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "json", menuName = "prefabs/json", order = 0)]
public class JsonObject : ScriptableObject
{
    public string JsonData;
    protected static string _pathToCollections = "Data/JsonObjects/";

    public static bool CreateJsonObject(string name, string json)
    {
        var obj = Resources.Load<JsonObject>($"{_pathToCollections}{name}");
        if (obj != null)
        {
            Debug.Log("created");

            AssetDatabase.DeleteAsset($"Assets/Resources/{_pathToCollections}{name}.asset");
            JsonObject jsonobj_new = ScriptableObject.CreateInstance<JsonObject>();
            jsonobj_new.JsonData = json;
            AssetDatabase.CreateAsset(jsonobj_new, $"Assets/Resources/{_pathToCollections}{name}.asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return false;
        }

#if UNITY_EDITOR
        JsonObject jsonobj = ScriptableObject.CreateInstance<JsonObject>();
        jsonobj.JsonData = json;
        AssetDatabase.CreateAsset(jsonobj, $"Assets/Resources/{_pathToCollections}{name}.asset");
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
        return true;
    }

    public static JsonObject GetJsonObject(string name)
    {
        var obj = Resources.Load<JsonObject>($"{_pathToCollections}{name}");

        return obj;
    }

    public string GetJson()
    {
        return JsonData;
    }

    public void SetJson(string jsonData)
    {
        JsonData = jsonData;
    }
}
