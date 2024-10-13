using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    [SerializeField] private float velocity = 5f;
    private bool clicked = false;

    void Update()
    {
        if (clicked)
        {
            // Move the arrow continuously to the left
            transform.Translate(Vector2.left * velocity * Time.deltaTime);
        }
    }

    private void OnMouseDown()  
    {
        clicked = true;  // Start moving the arrow on click
    }
}
