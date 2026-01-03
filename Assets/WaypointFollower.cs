using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float turnSpeed = 1f;

    private Transform nextWaypoint;
    private Transform previousWaypoint;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        CalculateDirection();
    }

    // Update is called once per frame
    void Update()
    {
        RotateTowardsDirection();
        MoveFoward();
    }

    private void CalculateDirection()
    {
        Transform current;

        if (previousWaypoint == null)
        {
            current = this.transform;
        }

        else
        {
            current = previousWaypoint;
        }

        direction = new Vector3(nextWaypoint.position.x - current.position.x,
                                nextWaypoint.position.y - current.position.y,
                                nextWaypoint.position.z - current.position.z).normalized;
    }

    private void RotateTowardsDirection()
    {
        Quaternion targetRot = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, turnSpeed * Time.deltaTime);
    }

    private void MoveFoward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
