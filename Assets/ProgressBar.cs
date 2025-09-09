using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider bar;
    private DataCollector collector;

    private void Start()
    {
        collector = FindAnyObjectByType<DataCollector>();
        bar.minValue = 0;
        if (SceneManager.GetActiveScene().name == "GameScene")
            bar.maxValue = collector.BgameCount;
        else if (SceneManager.GetActiveScene().name == "MainScene")
            bar.maxValue = collector.AgameCount;
        else
            bar.maxValue = 5;
        bar.value = GameManager.Instance.currentIndex;
        bar.wholeNumbers = true;
        bar.fillRect.GetComponent<Image>().color = Color.green;
    }

}
