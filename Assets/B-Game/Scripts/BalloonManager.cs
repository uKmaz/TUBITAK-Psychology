using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BalloonManager : MonoBehaviour
{
    [SerializeField] private ScenarioDatas2 scenarioDatas2;
    [SerializeField] private GameObject[] truePrefab;
    [SerializeField] private GameObject redPrefab;
    [SerializeField] private GameObject upTransform;
    [SerializeField] private GameObject downTransform;
    [HideInInspector] private SceneTransitionManager sceneTransitionManager;

    private void Start()
    {
        sceneTransitionManager = FindAnyObjectByType<SceneTransitionManager>();
        if (sceneTransitionManager.isDemoOn())
        {
            int colorDecision;
            if (scenarioDatas2.DemoImagePairs[GameManager.Instance.currentIndex].colorOfTrueBalloon)
            {
                colorDecision = 0;
            }
            else
            {
                colorDecision = 1;
            }
            if (scenarioDatas2.DemoImagePairs[GameManager.Instance.currentIndex].answer)
            {

                Instantiate(truePrefab[colorDecision], upTransform.transform.position, Quaternion.identity);
                Instantiate(redPrefab, downTransform.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(truePrefab[colorDecision], downTransform.transform.position, Quaternion.identity);
                Instantiate(redPrefab, upTransform.transform.position, Quaternion.identity);
            }
        }
        else
        {
            int colorDecision;
            if (scenarioDatas2.ImagePairs[GameManager.Instance.currentIndex].colorOfTrueBalloon)
            {
                colorDecision = 0;
            }
            else
            {
                colorDecision = 1;
            }
            if (scenarioDatas2.ImagePairs[GameManager.Instance.currentIndex].answer)
            {

                Instantiate(truePrefab[colorDecision], upTransform.transform.position, Quaternion.identity);
                Instantiate(redPrefab, downTransform.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(truePrefab[colorDecision], downTransform.transform.position, Quaternion.identity);
                Instantiate(redPrefab, upTransform.transform.position, Quaternion.identity);
            }
        }
       
    }
}
