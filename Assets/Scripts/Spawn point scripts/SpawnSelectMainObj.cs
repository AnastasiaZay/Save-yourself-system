using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class SpawnSelectMainObj : MonoBehaviour
{
    public static SpawnPoint PrevSelectedSpawnObj;

    [Button]
    private void SaveAndReturn()
    {
        Debug.Log(PrevSelectedSpawnObj);
        string collectionName = PrevSelectedSpawnObj.CollectionName;
        int chldCount = transform.childCount;
        List<Vector3> points = new List<Vector3>();
        for (int i = 0; i < chldCount; i++)
        {
            points.Add(transform.GetChild(i).gameObject.transform.position);
        }
        SpawnPoint.SavePointsCollection(collectionName, points);
        DestroyImmediate(gameObject);
    }

    [Button]
    private void AddPoint()
    {
        var obj = new GameObject();
        obj.transform.parent = transform;
    }

    private void OnDestroy()
    {
        Selection.activeGameObject = PrevSelectedSpawnObj?.gameObject;
        PrevSelectedSpawnObj = null;
        SpawnPoint.SetEditMode(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        int chldCount = transform.childCount;

        for (int i = 0; i < chldCount; i++)
        {
            Gizmos.DrawSphere(transform.GetChild(i).position, 0.5f);
        }
    }

}