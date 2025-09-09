using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DemoCheckManager : MonoBehaviour
{
    private ButtonManager buttonManager;
    private DataCollector datas;
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    private void Start()
    {
        datas = FindAnyObjectByType<DataCollector>();
        buttonManager = FindAnyObjectByType<ButtonManager>();
        textUGUI.text = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoCheckQuestion;
    }
    private void Update()
    {
        scoreUGUI.text = GameManager.Instance.score.ToString();
    }
}
