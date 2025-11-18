using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrivingPOVManager : MonoBehaviour
{
    public CinemachineVirtualCamera camFront;
    public CinemachineVirtualCamera camLeft;
    public CinemachineVirtualCamera camRight;
    public CinemachineVirtualCamera camBack;

    [Range(0.0f, 0.3f)]
    public float edgeThickness = 0.05f;

    private int screenW;
    private int screenH;

    void Start()
    {
        screenW = Screen.width;
        screenH = Screen.height;
    }

    void Update()
    {
        Vector3 m = Input.mousePosition;

        bool leftEdge = m.x < screenW * edgeThickness;
        bool rightEdge = m.x > screenW * (1 - edgeThickness);
        bool topEdge = m.y > screenH * (1 - edgeThickness);
        bool bottomEdge = m.y < screenH * edgeThickness;

        // horizontal edges
        if (leftEdge) SwitchTo(camLeft);
        else if (rightEdge) SwitchTo(camRight);

        // vertical edges (optional)
        else if (topEdge) SwitchTo(camFront);
        else if (bottomEdge) SwitchTo(camBack);
    }

    void SwitchTo(CinemachineVirtualCamera cam)
    {
        // Lower all priorities
        camFront.Priority = 5;
        camLeft.Priority = 5;
        camRight.Priority = 5;
        camBack.Priority = 5;

        // Boost this one
        cam.Priority = 10;
    }
}