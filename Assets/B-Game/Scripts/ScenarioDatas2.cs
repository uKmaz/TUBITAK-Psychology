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

[CreateAssetMenu(fileName = "NewScenarioData", menuName = "ScriptableObject/ScenarioData2", order = 2)]
public class ScenarioDatas2 : ScriptableObject
{
    [SerializeField] private Image[] image1NonFixed;
    [SerializeField] private Image[] image2NonFixed;
    [HideInInspector] public int currentSceneCount;
    [HideInInspector] public List<ScenarioData2> ImagePairs = new List<ScenarioData2>();

    public void initializeData()
    {
        List<ScenarioData2> tempPairs = new List<ScenarioData2>();

        for(int j = 0; j < 2; j++)
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
    }

}
