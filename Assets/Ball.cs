using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Ball : MonoBehaviour
{
    public string ballID;

    public void Whuh()
    {
        Debug.Log("This is Ball No." + ballID);
    }
}
