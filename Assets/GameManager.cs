using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public int score = 0;
    [HideInInspector] public bool balloonPopped;
    [HideInInspector] public bool balloonPoppedToPass;
    private void Start()
    {
        balloonPopped = false;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
