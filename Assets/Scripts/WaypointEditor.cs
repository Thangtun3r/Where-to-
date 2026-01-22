using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]

static public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        Gizmos.DrawSphere(waypoint.transform.position, 0.2f);

        Gizmos.color = Color.white;

        // if(waypoint.nextWaypoint != null)
        // {
        //     Gizmos.color = Color.cyan;
        //     Gizmos.DrawLine(waypoint.transform.position, waypoint.nextWaypoint.transform.position);
        // }

        // if(waypoint.previousWaypoint != null)
        // {
        //     Gizmos.color = Color.cyan;
        //     Gizmos.DrawLine(waypoint.transform.position, waypoint.previousWaypoint.transform.position);
        // }

        if(waypoint.isIntersection)
        {
            if(waypoint.leftTurn != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoint.transform.position, waypoint.leftTurn.position);
            }

            if(waypoint.rightTurn != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(waypoint.transform.position, waypoint.rightTurn.position);
            }
        }
        
    }
}
