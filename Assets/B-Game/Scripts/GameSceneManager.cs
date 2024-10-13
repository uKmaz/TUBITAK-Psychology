using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI score;

    private void Update()
    {
        score.text = GameManager.Instance.score.ToString();
    }
}
