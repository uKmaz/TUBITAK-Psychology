using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ScenarioData
{
    public string MainSceneText;
    public string ActionSceneText;
    public string ActionSceneTrueText;
    public string ActionSceneTrueLetter;
    public string CheckUpQuestion;
    public bool CheckUpAnswer;
}
public class DemoScenarioData
{
    public string DemoMainText;
    public string DemoActionText;
    public string DemoActionTrueText;
    public string DemoActionTrueLetter;
    public string DemoCheckQuestion;
    public bool DemoCheckAnswer;
}

[CreateAssetMenu(fileName = "NewScenarioData", menuName = "ScriptableObject/ScenarioDatas", order = 1)]
public class ScenarioDatas : ScriptableObject
{
    #region INPUT DATAS
    [Header("----Abartılı Tehdit Algısı----")]
    public string[] MainSceneTexts1;
    public string[] ActionSceneTexts1;
    public string[] ActionSceneTrueTexts1;
    public string[] ActionSceneTrueLetter1;
    public string[] CheckUpQuestions1;
    public bool[] CheckUpAnswers1;

    [Header("----Abartılı Sorumluluk----")]
    public string[] MainSceneTexts2;
    public string[] ActionSceneTexts2;
    public string[] ActionSceneTrueTexts2;
    public string[] ActionSceneTrueLetter2;
    public string[] CheckUpQuestions2;
    public bool[] CheckUpAnswers2;

    [Header("----Mükemmelliyetçilik----")]
    public string[] MainSceneTexts3;
    public string[] ActionSceneTexts3;
    public string[] ActionSceneTrueTexts3;
    public string[] ActionSceneTrueLetter3;
    public string[] CheckUpQuestions3;
    public bool[] CheckUpAnswers3;

    [Header("----Belirsizliğe Tahammülsüzlük----")]
    public string[] MainSceneTexts4;
    public string[] ActionSceneTexts4;
    public string[] ActionSceneTrueTexts4;
    public string[] ActionSceneTrueLetter4;
    public string[] CheckUpQuestions4;
    public bool[] CheckUpAnswers4;

    [Header("----Düşüncelerin Aşırı Önemsenmesi----")]
    public string[] MainSceneTexts5;
    public string[] ActionSceneTexts5;
    public string[] ActionSceneTrueTexts5;
    public string[] ActionSceneTrueLetter5;
    public string[] CheckUpQuestions5;
    public bool[] CheckUpAnswers5;

    [Header("----Düşünce Kontrolünün Önemsenmesi----")]
    public string[] MainSceneTexts6;
    public string[] ActionSceneTexts6;
    public string[] ActionSceneTrueTexts6;
    public string[] ActionSceneTrueLetter6;
    public string[] CheckUpQuestions6;
    public bool[] CheckUpAnswers6;
    #endregion
    #region DEMO DATAS
    [Header("----  DEMO  ----")]
    public string[] demoMainSceneTexts;
    public string[] demoActionSceneTexts;
    public string[] demoActionSceneTrueTexts;
    public string[] demoActionSceneTrueLetter;
    public string[] demoCheckUpQuestions;
    public bool[] demoCheckUpAnswers;
    #endregion
    #region OUTPUT DATAS
    [HideInInspector] public List<ScenarioData> Scenarios = new List<ScenarioData>();
    [HideInInspector] public List<DemoScenarioData> DemoScenarios = new List<DemoScenarioData>();
    #endregion

    #region Functions
    public void InitializeData()
    {
        bool balance = false;
        while (!balance)
        {
            Scenarios.Clear();
            int[] randomIndices1 = GetRandomIndices(MainSceneTexts1.Length, 3);
            int[] randomIndices2 = GetRandomIndices(MainSceneTexts2.Length, 3);
            int[] randomIndices3 = GetRandomIndices(MainSceneTexts3.Length, 3);
            int[] randomIndices4 = GetRandomIndices(MainSceneTexts4.Length, 3);
            int[] randomIndices5 = GetRandomIndices(MainSceneTexts5.Length, 3);
            int[] randomIndices6 = GetRandomIndices(MainSceneTexts6.Length, 3);

            FillData(MainSceneTexts1, ActionSceneTexts1, ActionSceneTrueTexts1, ActionSceneTrueLetter1, CheckUpQuestions1, CheckUpAnswers1, randomIndices1);
            FillData(MainSceneTexts2, ActionSceneTexts2, ActionSceneTrueTexts2, ActionSceneTrueLetter2, CheckUpQuestions2, CheckUpAnswers2, randomIndices2);
            FillData(MainSceneTexts3, ActionSceneTexts3, ActionSceneTrueTexts3, ActionSceneTrueLetter3, CheckUpQuestions3, CheckUpAnswers3, randomIndices3);
            FillData(MainSceneTexts4, ActionSceneTexts4, ActionSceneTrueTexts4, ActionSceneTrueLetter4, CheckUpQuestions4, CheckUpAnswers4, randomIndices4);
            FillData(MainSceneTexts5, ActionSceneTexts5, ActionSceneTrueTexts5, ActionSceneTrueLetter5, CheckUpQuestions5, CheckUpAnswers5, randomIndices5);
            FillData(MainSceneTexts6, ActionSceneTexts6, ActionSceneTrueTexts6, ActionSceneTrueLetter6, CheckUpQuestions6, CheckUpAnswers6, randomIndices6);

            ShuffleList(Scenarios);

            balance = CheckQ_Balance();
        }
        
    }

    private int[] GetRandomIndices(int length, int count)
    {
        return Enumerable.Range(0, length).OrderBy(x => Random.value).Take(count).ToArray();
    }

    private void FillData(string[] sourceMain, string[] sourceAction, string[] sourceActionTrue, string[] sourceTrueLetter, string[] sourceCheckUpQ, bool[] sourceCheckUpA, int[] randomIndices)
    {
        foreach (int index in randomIndices)
        {
            ScenarioData scenario1 = new ScenarioData
            {
                MainSceneText = sourceMain[index],
                ActionSceneText = sourceAction[index],
                ActionSceneTrueText = sourceActionTrue[index],
                ActionSceneTrueLetter = sourceTrueLetter[index],
                CheckUpQuestion = sourceCheckUpQ[index],
                CheckUpAnswer = sourceCheckUpA[index]
            };

            ScenarioData scenario2 = new ScenarioData
            {
                MainSceneText = sourceMain[index],
                ActionSceneText = sourceAction[index],
                ActionSceneTrueText = sourceActionTrue[index],
                ActionSceneTrueLetter = sourceTrueLetter[index],
                CheckUpQuestion = sourceCheckUpQ[index],
                CheckUpAnswer = sourceCheckUpA[index]
            };

            Scenarios.Add(scenario1);
            Scenarios.Add(scenario2);
        }
    }
    public void initializeDemo()
    {
        fillDemo(demoMainSceneTexts, demoActionSceneTexts, demoActionSceneTrueTexts, demoActionSceneTrueLetter, demoCheckUpQuestions, demoCheckUpAnswers);
    }
    private void fillDemo(string[] sourceMain, string[] sourceAction, string[] sourceActionTrue, string[] sourceTrueLetter, string[] sourceCheckUpQ, bool[] sourceCheckUpA)
    {
        
        for (int index=0;index<sourceMain.Count();index++)
        {
            DemoScenarioData demo = new DemoScenarioData
            {
                DemoMainText = sourceMain[index],
                DemoActionText = sourceAction[index],
                DemoActionTrueText = sourceActionTrue[index],
                DemoActionTrueLetter = sourceTrueLetter[index],
                DemoCheckQuestion = sourceCheckUpQ[index],
                DemoCheckAnswer = sourceCheckUpA[index]
            };
            DemoScenarios.Add(demo);
        }
            
        
    }

    private void ShuffleList(List<ScenarioData> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(0, list.Count);
            ScenarioData temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    private bool CheckQ_Balance()
    {
        int yes = 0;
        int no = 0;
        for(int i = 0;i< Scenarios.Count; i++)
        {
            ScenarioData temp = Scenarios[i];
            if (Scenarios[i].CheckUpAnswer == true)
            {
                yes++;
            }
            else
            {
                no++;
            }
        }
        if (yes == no)
            return true;
        else return false;
    }
    #endregion
    
}
