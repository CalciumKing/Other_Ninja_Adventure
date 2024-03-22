using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] Vector3[] points;
    public Vector3[] Points => points;
    public Vector3 EntitiyPosition { get; set; }
    bool gameStarted;
    private void Start()
    {
        EntitiyPosition = transform.position;
        gameStarted = true;
    }
    public Vector3 GetPosition(int index)
    {
        return EntitiyPosition + points[index];
    }
    private void OnDrawGizmos()
    {
        if(!gameStarted && transform.hasChanged)
        {
            EntitiyPosition = transform.position;
        }
    }
}