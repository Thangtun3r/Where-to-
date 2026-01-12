using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem ("Tool/Waypoint Editor")]

    public static void Open()
    {
        GetWindow<WaypointManagerWindow>();
    }

    public Transform waypointRoot;

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null)
        {
            EditorGUILayout.HelpBox("Root transform must be assigned", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            DrawButtons();
            EditorGUILayout.EndVertical();

        }

        obj.ApplyModifiedProperties();
    }

    private void DrawButtons()
    {
        if(GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }

        if (GUILayout.Button("Create Left Turn"))
        {
            CreateLeftTurn();
        }

        if (GUILayout.Button("Create Right Turn"))
        {
            CreateRightTurn();
        }
    }

    private void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1)
        {
            SetPreviousWaypoint(waypoint);
        }

        Selection.activeObject = waypoint.gameObject;
    }

    private void CreateLeftTurn()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1)
        {
            SetPreviousWaypoint(waypoint);

            waypoint.previousWaypoint.leftTurn = waypoint.transform;
            waypoint.previousWaypoint.isIntersection = true;
        }

        Selection.activeObject = waypoint.gameObject;
    }

    private void CreateRightTurn()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointRoot.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointRoot, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if (waypointRoot.childCount > 1)
        {
            SetPreviousWaypoint(waypoint);

            waypoint.previousWaypoint.rightTurn = waypoint.transform;
            waypoint.previousWaypoint.isIntersection = true;
        }

        Selection.activeObject = waypoint.gameObject;
    }

    //-------------------------------------------------------------------------------------

    private void SetPreviousWaypoint(Waypoint waypoint)
    {
        waypoint.previousWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
        waypoint.previousWaypoint.nextWaypoint = waypoint;

        waypoint.transform.position = waypoint.previousWaypoint.transform.position;
        waypoint.transform.forward = waypoint.previousWaypoint.transform.forward;
    }
}
