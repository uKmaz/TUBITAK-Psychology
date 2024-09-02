using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    SceneTransitionManager sceneTransitionManager;
    [SerializeField] private ScenarioDatas scenarioData;
    [HideInInspector] public bool oneTime = false;
    [SerializeField] private Button yes;
    [SerializeField] private Button no;
    ColorBlock colorBlockYes;
    ColorBlock colorBlockNo;
    private void Start()
    {
        sceneTransitionManager=FindAnyObjectByType<SceneTransitionManager>();
        oneTime = false;
        colorBlockYes = yes.colors;
        colorBlockNo = no.colors;

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
       
        if(!oneTime)
        {
            if (scenarioData.Scenarios[GameManager.Instance.currentIndex].CheckUpAnswer)
            {
                GameManager.Instance.score += 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);
                colorBlockYes.pressedColor = Color.green;
            }
            else
            {
                GameManager.Instance.score -= 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
                colorBlockYes.pressedColor= Color.red;
            }
            sceneTransitionManager.checkSceneConditionMet = true;
        }
        oneTime = true;
    }
    public void noButtonCheckScene()
    {
        if(!oneTime)
        {
            if (!scenarioData.Scenarios[GameManager.Instance.currentIndex].CheckUpAnswer)
            {
                GameManager.Instance.score += 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);

            }
            else
            {
                GameManager.Instance.score -= 10;
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
                colorBlockNo.pressedColor = Color.red;

            }
        }
        oneTime = true;
        sceneTransitionManager.checkSceneConditionMet = true;



    }
}
