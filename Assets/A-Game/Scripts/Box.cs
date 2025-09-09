using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [HideInInspector] public bool onBox = false;
    Action action;
    DemoAction demoAction;
    private void Start()
    {
        action=FindAnyObjectByType<Action>();
        demoAction = FindAnyObjectByType<DemoAction>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(action != null)
        {
            if (other.CompareTag("Letter") && action.isDraggingForBox)
            {
                onBox = true;

            }
            else
            {
                onBox = false;
            }
        }
        else
        {
            if (other.CompareTag("Letter") && demoAction.isDraggingForBox)
            {
                onBox = true;

            }
            else
            {
                onBox = false;
            }
        }


    }
}
