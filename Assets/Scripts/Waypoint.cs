using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint[] connectedWaypoints = new Waypoint[1];
    public Transform leftTurn;
    public Transform rightTurn;
    public bool isIntersection = false;
}
