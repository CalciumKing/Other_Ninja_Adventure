using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Waypoint))]
public class WaypointEditor : Editor
{
    Waypoint waypointTarget => target as Waypoint;
    private void OnSceneGUI()
    {
        if (waypointTarget.Points.Length <= 0) return;
        Handles.color = Color.red;
        for (int i = 0; i < waypointTarget.Points.Length; i++)
        {
            EditorGUI.BeginChangeCheck();
            Vector3 currentPoint = waypointTarget.EntitiyPosition + waypointTarget.Points[i];
            Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, 0.5f, Vector3.one, Handles.SphereHandleCap);

            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 18;
            text.normal.textColor = Color.black;
            Vector3 textPos = new Vector3(.2f, -.2f);

            if(EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Free Move");
                waypointTarget.Points[i] = newPosition - waypointTarget.EntitiyPosition;
            }
        }
    }
}