using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainScene : MonoBehaviour
{
    [SerializeField] ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    [SerializeField] private GameObject skip;
    DataCollector datas;
    private void Start()
    {

        datas =FindAnyObjectByType<DataCollector>();
        scoreUGUI.text = GameManager.Instance.score.ToString();
        textUGUI.text = scenarioData.Scenarios[GameManager.Instance.currentIndex].MainSceneText;
        
        datas.fillDatasA();
    }
    public void skip_buttonReveal()
    {

        skip.SetActive(true);


    }
}
