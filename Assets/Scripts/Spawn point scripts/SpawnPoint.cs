using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public static Dictionary<string, List<Vector3>> Points = new();
    public static Dictionary<string, List<bool>> UsedPoint = new();
    private static bool _isEdited = false;

    public string CollectionName;
    public bool StartAuto;

    private void Start()
    {
        if (!StartAuto) return;
        RandomSpawn(true);
    }

    [Button]
    private void EditCollection()
    {
        if (name == "") return;
        if (_isEdited == true) return;

        var points = GetPointsCollectionInRuntime(CollectionName);

        if (points == null)
        {
            return;
        }

        var obj = new GameObject("SpawnPointCollection", typeof(SpawnSelectMainObj));
        SpawnSelectMainObj.PrevSelectedSpawnObj = this;
        for (int i = points.Count - 1; i >= 0; i--)
        {
            GameObject ch_obj = new GameObject($"point_{i}");
            ch_obj.transform.parent = obj.transform;
            ch_obj.transform.position = points[i];
        }

        SetEditMode(true);
    }

    [Button]
    private void CreateCollection()
    {
        CreatePointsCollection(CollectionName);
    }

    public void RandomSpawn(bool takePoint = false)
    {
        var points = GetPointsCollectionInRuntime(CollectionName);
        if (points == null) return;

        var boolList = UsedPoint[CollectionName];
        Debug.Log(boolList.Count);
        List<int> freePoints = new List<int>();
        for (int i = 0; i < boolList.Count; i++)
        {
            if (boolList[i] == false)
            {
                freePoints.Add(i);
            }
        }
        if (freePoints.Count == 0) return;

        int index = freePoints[Random.Range(0, freePoints.Count)];
        transform.position = Points[CollectionName][index];

        if (takePoint)
        {
            Debug.Log(index);
            UsedPoint[CollectionName][index] = true;
        }
    }
    public void RandomSpawnRange(int maxPoints, bool takePoint = false)
    {
        var points = GetPointsCollectionInRuntime(CollectionName).GetRange(0, maxPoints);
        if (points == null) return;

        var boolList = UsedPoint[CollectionName];
        List<int> freePoints = new List<int>();
        for (int i = 0; i < points.Count; i++)
        {
            if (boolList[i] == false)
            {
                freePoints.Add(i);
            }
        }
        if (freePoints.Count == 0) return;

        int index = freePoints[Random.Range(0, freePoints.Count)];
        transform.position = Points[CollectionName][index];

        if (takePoint)
        {
            UsedPoint[CollectionName][index] = true;
        }
    }
    public void SpawnOnIndexPoint(int index, bool takePoint = false)
    {
        var points = GetPointsCollectionInRuntime(CollectionName);
        if (points == null) return;

        var boolList = UsedPoint[CollectionName];
        if (boolList[index] == true) return;
        if (index < 0 || index >= points.Count) return;

        transform.position = Points[CollectionName][index];

        if (takePoint)
        {
            UsedPoint[CollectionName][index] = true;
        }
    }

    public static void RandomizeCollection(string name)
    {
        GetPointsCollectionInRuntime(name);

        Points.TryGetValue(name, out var points);

        int n = points.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            Vector3 value = points[k];
            points[k] = points[n];
            points[n] = value;
        }

        Points[name] = points;
    }

    public static void DerandomizeCollection(string name)
    {
        var jsonObj = JsonObject.GetJsonObject(name);
        if (jsonObj == null)
        {
            Debug.LogWarning("Коллекции не существует");
            return;
        }
        var points = JsonUtility.FromJson<Vector3Container>(jsonObj.GetJson()).vertices;
        if (!Points.TryAdd(name, points))
        {
            Points[name] = points;
        }
    }

    public static List<Vector3> GetPointsCollectionInRuntime(string name)
    {
        if (name == "") return null;

        if (Points.ContainsKey(name) && Application.isPlaying)
        {
            UsedPoint.TryAdd(name, (new bool[Points[name].Count]).ToList());
            return Points[name];
        }

        var jsonObj = JsonObject.GetJsonObject(name);
        if (jsonObj == null)
        {
            Debug.LogWarning("Коллекции не существует");
            return null;
        }
        List<Vector3> points = JsonUtility.FromJson<Vector3Container>(jsonObj.GetJson()).vertices;
        if (!Points.TryAdd(name, points))
        {
            Points[name] = points;
        }
        UsedPoint.TryAdd(name, (new bool[points.Count]).ToList());

        return points;
    }
    public static void SavePointsCollection(string name, List<Vector3> points)
    {
        if (name == "") return;

        var jsonObj = JsonObject.GetJsonObject(name);
        if (jsonObj == null)
        {
            Debug.LogWarning("Коллекции не существует");
            return;
        }

        Debug.Log(JsonUtility.ToJson(new Vector3Container(points)));
        JsonObject.CreateJsonObject(name, JsonUtility.ToJson(new Vector3Container(points)));
    }
    public static void CreatePointsCollection(string name)
    {
        if (_isEdited == true) return;
        if (name == "") return;

        var jsonObj = JsonObject.GetJsonObject(name);
        if (jsonObj != null)
        {
            Debug.LogWarning("Коллекция уже существует");
            return;
        }

#if UNITY_EDITOR
        JsonObject.CreateJsonObject(name, JsonUtility.ToJson(new List<Vector3>()));
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
#endif
    }
    public static void SetEditMode(bool state)
    {
        _isEdited = state;
    }
}
