using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ScenarioData2
{
    public Image image1;
    public Image image2;
    public bool answer;
    public bool colorOfTrueBalloon; // True: Orange, False: Other color
}
public class DemoScenarioData2
{
    public Image image1;
    public Image image2;
    public bool answer;
    public bool colorOfTrueBalloon;
}

[CreateAssetMenu(fileName = "NewScenarioData", menuName = "ScriptableObject/ScenarioData2", order = 2)]
public class ScenarioDatas2 : ScriptableObject
{
    [SerializeField] private Image[] image1NonFixed;
    [SerializeField] private Image[] image2NonFixed;
    [HideInInspector] public int currentSceneCount;
    [HideInInspector] public List<ScenarioData2> ImagePairs = new List<ScenarioData2>();
    [HideInInspector] public List<DemoScenarioData2> DemoImagePairs = new List<DemoScenarioData2>();

    public void initializeData()
    {
        List<ScenarioData2> tempPairs = new List<ScenarioData2>();

        for(int j = 0; j < 6; j++)
        {
            for (int i = 0; i < image1NonFixed.Length; i++)
            {
                // Create the 4 unique possibilities for the current pair
                List<ScenarioData2> pairPossibilities = new List<ScenarioData2>
            {
                new ScenarioData2
                {
                    image1 = image1NonFixed[i],
                    image2 = image2NonFixed[i],
                    answer = true, // True answer above
                    colorOfTrueBalloon = true // True: Orange
                },
                new ScenarioData2
                {
                    image1 = image2NonFixed[i],
                    image2 = image1NonFixed[i],
                    answer = false, // True answer below
                    colorOfTrueBalloon = true // True: Orange
                },
                new ScenarioData2
                {
                    image1 = image1NonFixed[i],
                    image2 = image2NonFixed[i],
                    answer = true, // True answer above
                    colorOfTrueBalloon = false // False: Blue
                },
                new ScenarioData2
                {
                    image1 = image2NonFixed[i],
                    image2 = image1NonFixed[i],
                    answer = false, // True answer below
                    colorOfTrueBalloon = false // False: Blue
                }
            };


                tempPairs.AddRange(pairPossibilities);
            }
        }
        

        ImagePairs = tempPairs.OrderBy(x => Random.value).ToList();
        Debug.Log(ImagePairs.Count());
    }
    public void initializeDemo()
    {
        List<DemoScenarioData2> pairPossibilities = new List<DemoScenarioData2>
            {
                new DemoScenarioData2
                {
                    image1 = image1NonFixed[0],
                    image2 = image2NonFixed[0],
                    answer = true, // True answer above
                    colorOfTrueBalloon = true // True: Orange
                },
                new DemoScenarioData2
                {
                    image1 = image2NonFixed[1],
                    image2 = image1NonFixed[1],
                    answer = false, // True answer below
                    colorOfTrueBalloon = true // True: Orange
                },
                new DemoScenarioData2
                {
                    image1 = image1NonFixed[2],
                    image2 = image2NonFixed[2],
                    answer = true, // True answer above
                    colorOfTrueBalloon = false // False: Blue
                },
                new DemoScenarioData2
                {
                    image1 = image2NonFixed[3],
                    image2 = image1NonFixed[3],
                    answer = false, // True answer below
                    colorOfTrueBalloon = false // False: Blue
                }
            };
        DemoImagePairs.AddRange(pairPossibilities);
    }
}
