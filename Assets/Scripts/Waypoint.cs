using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;
    public Waypoint previousWaypoint;
    public Transform leftTurn;
    public Transform rightTurn;

    public bool isIntersection = false;
}
