using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonScript : MonoBehaviour
{
    [SerializeField] private ScenarioDatas2 scenarioDatas2;

    private void Start()
    {
        GameManager.Instance.balloonPopped = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Arrow"))
        {
            if (gameObject.name[0].ToString() == "O"|| gameObject.name[0].ToString() == "B")
            {
                GameManager.Instance.score += 10;
            }
            else
            {
                GameManager.Instance.score -= 10;
            }
            GameManager.Instance.balloonPopped = true;
            Destroy(gameObject);
        }
    }


}
