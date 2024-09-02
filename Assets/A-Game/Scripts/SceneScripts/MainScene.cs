using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    [SerializeField] ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    private void Start()
    {
        scoreUGUI.text = GameManager.Instance.score.ToString();
        textUGUI.text = scenarioData.Scenarios[GameManager.Instance.currentIndex].MainSceneText;
    }
    
}
