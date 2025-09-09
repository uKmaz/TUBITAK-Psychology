using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DemoMainScene : MonoBehaviour
{
    [SerializeField] ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    DataCollector datas;
    [SerializeField] private GameObject skip;

    private void Start()
    {
        datas = FindAnyObjectByType<DataCollector>();
        scoreUGUI.text = GameManager.Instance.score.ToString();
        textUGUI.text = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoMainText;

        datas.fillDatasA();
    }
    public void skip_buttonReveal()
    {

        skip.SetActive(true);


    }

}
