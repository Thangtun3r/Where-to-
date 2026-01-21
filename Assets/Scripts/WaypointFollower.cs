using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnSpeed = 1f;

    public Transform previousWaypoint;
    public Transform nextWaypoint;

    [SerializeField] private int turnDesire = 1;
    private bool stop = false;

    private Vector3 direction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            turnDesire = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            turnDesire = 2;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            turnDesire = 1;
        }

        //Calculate distance and set waypoint
        float distance = Vector3.Distance(this.transform.position, nextWaypoint.position);
        if (distance <= 0.5)
        {
            Waypoint nextWaypointScript = nextWaypoint.gameObject.GetComponent<Waypoint>();

            if (nextWaypointScript.isIntersection != true)
            {
                nextWaypoint = nextWaypointScript.nextWaypoint.gameObject.transform;
                stop = false;
            }

            else if(nextWaypointScript.isIntersection == true)
            {
                //Turn desire 0 = left desire ; 1 = no desire  ; 2 = right desire
                switch (turnDesire)
                {
                    case 0:
                        nextWaypoint = nextWaypointScript.leftTurn.gameObject.transform;
                        stop = false;
                        turnDesire = 1;
                        break;

                    case 1:
                        stop = true;
                        break;

                    case 2:
                        nextWaypoint = nextWaypointScript.rightTurn.gameObject.transform;
                        stop = false;
                        turnDesire = 1;
                        break;
                }
            }
        }

        else
        {
            CalculateDirection();
            MoveTowardsWaypoint();
        }
    }

    private void CalculateDirection()
    {
        direction = new Vector3(nextWaypoint.position.x - this.transform.position.x,
                                nextWaypoint.position.y - this.transform.position.y,
                                nextWaypoint.position.z - this.transform.position.z).normalized;
    }

    private void MoveTowardsWaypoint()
    {
        float actualTurn = 0;
        float actualSpeed = 0;

        if (stop)
        {
            actualTurn = 0;
            actualSpeed = 0;
        }

        if (!stop)
        {
            actualTurn = turnSpeed;
            actualSpeed = moveSpeed;
        }

        Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, actualTurn * Time.deltaTime);

        transform.position += this.transform.forward * actualSpeed * Time.deltaTime;
    }
}
