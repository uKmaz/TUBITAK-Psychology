using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private DataCollector datas;
    SceneTransitionManager sceneTransitionManager;
    [SerializeField] private ScenarioDatas scenarioData;
    [HideInInspector] public bool oneTime = false;
    [SerializeField] private Button yes;
    [SerializeField] private Button no;

    private void Start()
    {
        datas=FindAnyObjectByType<DataCollector>();
        sceneTransitionManager=FindAnyObjectByType<SceneTransitionManager>();
        oneTime = false;

    }
    private void Update()
    {
        if (oneTime)
        {
            yes.enabled = false;
            no.enabled = false;
        }
    }

    public void yesButtonCheckScene()
    {
        datas.InputCheckAnswer[GameManager.Instance.currentIndex] = true;
        if(!oneTime)
        {
            if (scenarioData.Scenarios[GameManager.Instance.currentIndex].CheckUpAnswer)
            {
                GameManager.Instance.score += 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);
                datas.isInputCorrect[GameManager.Instance.currentIndex] = true;
            }
            else
            {
                GameManager.Instance.score -= 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
                datas.isInputCorrect[GameManager.Instance.currentIndex] = false;
            }
            sceneTransitionManager.checkSceneConditionMet = true;
        }
        oneTime = true;
    }
    public void noButtonCheckScene()
    {
        datas.InputCheckAnswer[GameManager.Instance.currentIndex] = false;
        if (!oneTime)
        {
            if (!scenarioData.Scenarios[GameManager.Instance.currentIndex].CheckUpAnswer)
            {
                GameManager.Instance.score += 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);
                datas.isInputCorrect[GameManager.Instance.currentIndex] = true;
            }
            else
            {
                GameManager.Instance.score -= 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
                datas.isInputCorrect[GameManager.Instance.currentIndex] = false;
            }
        }
        oneTime = true;
        sceneTransitionManager.checkSceneConditionMet = true;



    }
    public void demoButtonMainScene()
    {
        sceneTransitionManager.changeDemo(true);

    }
}
