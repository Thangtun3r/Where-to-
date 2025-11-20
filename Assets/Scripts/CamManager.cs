using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> CamAngles;

    [Range(0.0f, 0.1f)]
    public float edgeThickness = 0.02f;
    private int screenW;

    [SerializeField] private int currentAngle = 2;

    public CinemachineBrain cineBrain;

    void Start()
    {
        //Determine where screen edges would be on set resolution
        screenW = Screen.width;
    }

    void Update()
    {
        Vector3 m = Input.mousePosition;

        bool rightEdge = m.x < screenW * edgeThickness;
        bool leftEdge = m.x > screenW * (1 - edgeThickness);

        // mouse touch edge -> update currentAngle and use it as an idex to switch to correspoding entry in CamAngles list
        if (leftEdge && currentAngle < CamAngles.Count - 1 && !cineBrain.IsBlending)
        {
            currentAngle++;
            SwitchToCurrentCam();
        }
        else if (rightEdge && currentAngle > 0 && !cineBrain.IsBlending)
        {
            currentAngle--;
            SwitchToCurrentCam();
        }
    }

    private void SwitchToCurrentCam()
    {
        // Lower all cam prio
        foreach (var p in CamAngles)
        {
            p.Priority = 5;
        }

        // Boost currentCam prio
        CamAngles[currentAngle].Priority = 10;
    }
}