using System;
using UnityEngine;

public class Point : MonoBehaviour
{
    public Vector3 Position { get; set;}

    private void Awake()
    {
        Position = transform.position;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 0.4f);
    }
}