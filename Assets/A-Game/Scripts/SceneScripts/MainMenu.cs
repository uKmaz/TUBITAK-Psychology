using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private ScenarioDatas scenarioDatas;
    [SerializeField] private ScenarioDatas2 scenarioDatas2;
    [SerializeField] private TMP_InputField rumuzField;
    [HideInInspector] public bool rumuzChanged;
    private void Start()
    {
        rumuzChanged = false;
        scenarioDatas.InitializeData();
        scenarioDatas.initializeDemo();
        scenarioDatas2.initializeData();
        scenarioDatas2.initializeDemo();
    }

}
