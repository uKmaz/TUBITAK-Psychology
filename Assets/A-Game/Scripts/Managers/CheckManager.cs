using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    private void Start()
    {
        textUGUI.text = scenarioData.Scenarios[GameManager.Instance.currentIndex].CheckUpQuestion;
    }
    private void Update()
    {
        scoreUGUI.text = GameManager.Instance.score.ToString();
    }
}
