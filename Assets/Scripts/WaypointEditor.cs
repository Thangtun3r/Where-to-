using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]

public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        if((gizmoType & GizmoType.Selected) != 0)
        {
            Gizmos.color = Color.yellow;
        }

        else
        {
            Gizmos.color = Color.yellow * 0.5f;        
        }

        Gizmos.DrawSphere(waypoint.transform.position, 0.2f);

        Gizmos.color = Color.white;

        if(waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(waypoint.transform.position, waypoint.previousWaypoint.transform.position);
        }

        if(waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(waypoint.transform.position, waypoint.nextWaypoint.transform.position);
        }

    }
}
