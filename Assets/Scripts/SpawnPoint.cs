using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour // Точки спавна
{
    public static List<SpawnPoint> Points = new List<SpawnPoint>();


    void Awake()
    {
        Points.Add(this);
    }

    public static void GetRespawn(GameObject Player, int respawn_index = -1)
    {
        int resp_point = respawn_index;
        if (respawn_index == -1)
        {
            
            resp_point = Random.Range(0, Points.Count);
        }

        Player.transform.position = Points[resp_point].transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.25f); 
    }
}
