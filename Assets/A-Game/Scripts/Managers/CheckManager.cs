using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckManager : MonoBehaviour
{
    private ButtonManager buttonManager;
    private DataCollector datas;
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    private float timer;
    private void Start()
    {
        datas=FindAnyObjectByType<DataCollector>();
        buttonManager=FindAnyObjectByType<ButtonManager>();
        timer = 0;
        textUGUI.text = scenarioData.Scenarios[GameManager.Instance.currentIndex].CheckUpQuestion;
    }
    private void Update()
    {
        if (!buttonManager.oneTime)
        {
        timer += Time.deltaTime;
        }
        else
        {
            datas.InputCheckTime[GameManager.Instance.currentIndex] = timer;
        }
        scoreUGUI.text = GameManager.Instance.score.ToString();
    }
}
