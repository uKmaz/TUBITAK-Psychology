using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [SerializeField] private ScenarioDatas scenarioData;
    [SerializeField] private TextMeshProUGUI text;
    private Action action;
    private DemoAction demoAction;
    /*
    public TypeWriter typeWrite;
    public TextMeshProUGUI[] textElements;
    private string[] texts=new string[1];
    private int currentIndex = 0;
    */
    [SerializeField]
    void Start()
    {
        action = FindAnyObjectByType<Action>();
        if(action != null)
        {
            text.text = scenarioData.Scenarios[GameManager.Instance.currentIndex].ActionSceneText;
        }
        else
        {
            demoAction = FindAnyObjectByType<DemoAction>();
            text.text = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoActionText;
        }
        /*for (int i = 0; i < textElements.Length; i++)
        {
            texts[i]= scenarioData.Scenarios[GameManager.Instance.currentIndex].ActionSceneText;
        }
         
        ShowNextText();*/
    }
    /*
    private void Update()
    {
        if (typeWrite.IsDone())
        {
            ShowNextText();
        }
    }

    public void ShowNextText()
    {
        if (currentIndex < texts.Length)
        {
            typeWrite.textToDisplay = textElements[currentIndex];

            typeWrite.StartTyping(texts[currentIndex]);

            currentIndex++;
        }


    }
    */
}
