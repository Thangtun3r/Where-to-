using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Yarn;
using Yarn.Unity;

public class CamManager : MonoBehaviour
{
    public List<CinemachineVirtualCamera> CamAngles;

    [Range(0.0f, 0.1f)]
    public float edgeThickness = 0.02f;
    private int screenW;

    [SerializeField] private int currentAngle = 2;
    private bool started = false;
    private Coroutine questioning;

    public CinemachineBrain cineBrain;
    public DialogueRunner dialogueRunner;
    public MultiAimConstraint aimConstraint;

    void Start()
    {
        if (dialogueRunner)
            Debug.Log("got it");

        //Determine where screen edges would be on set resolution
        screenW = Screen.width;
    }

    void Update()
    {
        if (currentAngle == 3 && !started)
        {
            started = true;
            if (questioning == null) // Only start if coroutine isn't running
                questioning = StartCoroutine(Question());
        }

        if (currentAngle != 3 && started)
        {
            started = false;
            aimConstraint.weight = 0f;

            // Stop dialogue and reset if the angle changes
            dialogueRunner.Dialogue.Continue();

            // Stop the questioning coroutine if it's running
            if (questioning != null)
            {
                StopCoroutine(questioning);
                questioning = null; // Nullify the reference after stopping
            }
        }

        Vector3 m = Input.mousePosition;

        bool rightEdge = m.x < screenW * edgeThickness;
        bool leftEdge = m.x > screenW * (1 - edgeThickness);

        // Check mouse position to detect edge touches -> update currentAngle
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
        // Lower all cam priority
        foreach (var p in CamAngles)
        {
            p.Priority = 5;
        }

        // Boost current camera's priority
        CamAngles[currentAngle].Priority = 10;
    }

    IEnumerator Question()
    {
        // Ensure we are still in the right camera angle
        yield return new WaitForSeconds(1f); // Wait for 1 second before starting the lerp

        float duration = 0.7f;  // Duration for the lerp (1 second here, can be adjusted)
        float timeElapsed = 0f; // Time tracker for lerping

        // Lerp from the current weight to 1 over the specified duration
        float initialWeight = aimConstraint.weight;  // Get the initial weight value

        while (timeElapsed < duration)
        {
            aimConstraint.weight = Mathf.Lerp(initialWeight, 1f, timeElapsed / duration);  // Interpolate weight
            timeElapsed += Time.deltaTime;  // Increment elapsed time
            yield return null;  // Wait for the next frame
        }

        // Ensure weight is exactly 1 at the end of the lerp
        aimConstraint.weight = 1f;

        // Start dialogue after the lerp completes
        dialogueRunner.StartDialogue("Ballsy");
    }
}
