using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [HideInInspector] public bool onBox = false;
    Action action;
    private void Start()
    {
        action=FindAnyObjectByType<Action>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Letter")&&action.isDraggingForBox)
        {
            onBox = true;
            
        }
        else
        {
            onBox = false;
        }

    }
}
