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
    [HideInInspector] public bool didClick = false;
    [SerializeField] private Button yes;
    [SerializeField] private Button no;

    private void Start()
    {
        datas=FindAnyObjectByType<DataCollector>();
        sceneTransitionManager=FindAnyObjectByType<SceneTransitionManager>();
        didClick = false;

    }
    private void Update()
    {
        if (didClick)
        {
            yes.enabled = false;
            no.enabled = false;
        }
    }

    public void demoYes()
    {
        datas.InputCheckAnswer[GameManager.Instance.currentIndex] = true;
        if (!didClick)
        {
            if (scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoCheckAnswer)
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
        didClick = true;
    }
    
    public void demoNo()
    {
        datas.InputCheckAnswer[GameManager.Instance.currentIndex] = false;
        if (!didClick)
        {
            if (!scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoCheckAnswer)
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
        didClick = true;
        sceneTransitionManager.checkSceneConditionMet = true;

    }
    public void yesButtonCheckScene()
    {
        datas.InputCheckAnswer[GameManager.Instance.currentIndex] = true;
        if(!didClick)
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
        didClick = true;
    }
    public void noButtonCheckScene()
    {
        datas.InputCheckAnswer[GameManager.Instance.currentIndex] = false;
        if (!didClick)
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
        didClick = true;
        sceneTransitionManager.checkSceneConditionMet = true;



    }
    public void StartButton()
    {
        sceneTransitionManager.gameStarted = true;
    }
    public void skipScene()
    {
        sceneTransitionManager.skipScene = true;
    }
    public void closeGame()
    {
        Application.Quit();
    }
    public void allScores()
    {
        GameManager.Instance.PrintAllScores();
    }
    public void gameChoiceA()
    {
        sceneTransitionManager.gameSelection = true;
        skipScene();
    }
    public void gameChoiceB()
    {
        skipScene();
        sceneTransitionManager.gameSelection = false;
    }
}
