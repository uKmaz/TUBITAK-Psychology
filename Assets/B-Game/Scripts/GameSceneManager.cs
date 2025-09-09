
using TMPro;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private float timer;
    private DataCollector collector;
    [HideInInspector] public bool popped;
    private bool oneTime;

    private void Start()
    {
        collector = FindAnyObjectByType<DataCollector>();
        popped = false;
        oneTime = false;
        timer = 0;
    }
    private void Update()
    {
            timer += Time.deltaTime;
        if (!oneTime && popped)
        {

            
            collector.time_spent[GameManager.Instance.currentIndex] = timer;

            oneTime = true;
        }
        score.text = GameManager.Instance.score.ToString();
    }

    
}
