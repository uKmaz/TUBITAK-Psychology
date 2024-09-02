using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TextManager : MonoBehaviour
{
    [SerializeField] private ScenarioDatas scenarioData;
    public TypeWriter typeWrite;
    public TextMeshProUGUI[] textElements;
    private string[] texts=new string[1];
    private int currentIndex = 0;

    void Start()
    {
        
        for (int i = 0; i < textElements.Length; i++)
        {
            texts[i]= scenarioData.Scenarios[GameManager.Instance.currentIndex].ActionSceneText;
        }
         
        ShowNextText();
    }
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
}
