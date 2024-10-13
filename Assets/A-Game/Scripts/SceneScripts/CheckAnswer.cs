using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckAnswer : MonoBehaviour
{
    [SerializeField] private ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI text;
    private DataCollector datas;
    private void Start()
    {
        datas=FindAnyObjectByType<DataCollector>();
        if (datas.isInputCorrect[GameManager.Instance.currentIndex])
        {
            text.text = "DOĞRU";
            text.color = Color.green;
        }
        else
        {
            text.text = "YANLIŞ";
            text.color = Color.red;
        }
    }
}
