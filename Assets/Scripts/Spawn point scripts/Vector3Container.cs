using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct Vector3Container
{
    public List<Vector3> vertices;

    public Vector3Container(List<Vector3> vertices)
    {
        this.vertices = vertices;
    }
}