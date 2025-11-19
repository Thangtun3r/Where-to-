using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DrivingPOVManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> POVCams;

    [Range(0.0f, 0.1f)]
    public float edgeThickness = 0.02f;
    private int screenW;

    [SerializeField] private int currentCam = 2;

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

        // mouse touch edge -> update currentCam and use it as an idex to switch to correspoding object in POVCams list
        if (leftEdge && currentCam < POVCams.Count - 1 && !cineBrain.IsBlending)
        {
            currentCam++;
            SwitchToCurrentCam();
        }
        else if (rightEdge && currentCam > 0 && !cineBrain.IsBlending)
        {
            currentCam--;
            SwitchToCurrentCam();
        }
    }

    private void SwitchToCurrentCam()
    {
        // Lower all cam prio
        foreach (var p in POVCams)
        {
            p.Priority = 5;
        }

        // Boost currentCam prio
        POVCams[currentCam].Priority = 10;
    }
}