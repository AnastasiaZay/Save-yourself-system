using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapPoint : MonoBehaviour
{
    public static List<TrapPoint> Points = new List<TrapPoint>();


    void Awake()
    {
        Points.Add(this);
    }

    public static void GetRespawn(GameObject Player, int trap_respawn_index = -1)
    {
        int resp_point = trap_respawn_index;
        if (trap_respawn_index == -1)
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