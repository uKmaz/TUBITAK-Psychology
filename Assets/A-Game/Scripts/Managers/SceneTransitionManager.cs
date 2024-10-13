using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class SceneTransitionManager : MonoBehaviour
{
    #region VARIABLES
    [HideInInspector] public string time;
    private DataCollector datas;
    [SerializeField] private float mainMenuWaitTime;  // MainMenu'de Bekleme Süresi
    [SerializeField] private float mainSceneWaitTime;   // MainScene'de bekleme süresi
    [SerializeField] private float actionSceneWaitTime; // ActionScene'de bekleme süresi
    [SerializeField] public float checkSceneWaitTime;  // CheckScene'de bekleme süresi
    [SerializeField] private float bGameInfoWaitTime;
    [SerializeField] private float focusSceneWaitTime;
    [SerializeField] private float imageSceneWaitTime;
    [SerializeField] private float gameSceneWaitTime;

    [SerializeField] private float transitionWaitTime;  // A oyunundan B oyununa geçiş sahnesi bekleme süresi
    [SerializeField] private ScenarioDatas scenarioData;      // ScenarioDatas scriptable object referansı A OYUNU İÇİN
    [SerializeField] private ScenarioDatas2 scenarioData2;      // ScenarioDatas scriptable object referansı B OYUNU İÇİN

    [HideInInspector] public bool actionSceneConditionMet = false;
    [HideInInspector] public bool checkSceneConditionMet = false;
    [HideInInspector] public MainMenu mainMenu;
    [SerializeField] public float elapsedTime = 0f;
    private bool demo=false;
    private int sceneCount = 0;
    #endregion
    private void Start()
    {
        DateTime dateAndTime = DateTime.Now;
        time = dateAndTime.ToString("yyyy-MM-dd HH:mm:ss");
        DontDestroyOnLoad(gameObject);
        StartCoroutine(HandleSceneTransitionsB());
    }

    private IEnumerator HandleSceneTransitionsA()
    {
        yield return StartCoroutine(AGame(true));
        yield return StartCoroutine(LoadSceneAndWait("Transition", transitionWaitTime, true));
        yield return StartCoroutine(BGame(false));
    }
    private IEnumerator HandleSceneTransitionsB()
    {
        yield return StartCoroutine(BGame(true));
        yield return StartCoroutine(LoadSceneAndWait("Transition", transitionWaitTime, false));
        yield return StartCoroutine(AGame(false));
    }

    private IEnumerator LoadSceneAndWait(string sceneName, float waitTime,bool aGame)
    {

        if (aGame)
        {
            SceneManager.LoadScene(sceneName);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            datas = FindObjectOfType<DataCollector>();
            elapsedTime = 0;
            while (elapsedTime < waitTime)
            {

                if (sceneName == "MainMenu")
                {
                    if (demo)
                    {
                        break;
                    }
                    mainMenu = FindObjectOfType<MainMenu>();
                    if (mainMenu.rumuzChanged)
                    {
                        elapsedTime = 0f;
                        waitTime = 5f;
                        mainMenu.rumuzChanged = false;
                    }
                }
                if ((sceneName == "MainScene" && Input.GetMouseButtonDown(0)) || (sceneName == "DemoMain" && Input.GetMouseButtonDown(0)))
                {
                    break; // MainScene'de tıklama ile bekleme süresi bitiriliyor
                }

                if ((sceneName == "ActionScene" && actionSceneConditionMet) || (sceneName == "DemoAction" && actionSceneConditionMet))
                {
                    actionSceneConditionMet = false;
                    waitTime = Mathf.Min(5f, waitTime); // ActionScene'de koşul karşılanırsa süre 5 saniyeye düşüyor

                }

                if ((sceneName == "CheckScene" && checkSceneConditionMet) || (sceneName == "DemoCheck" && checkSceneConditionMet))
                {
                    checkSceneConditionMet = false;
                    break;
                }

                elapsedTime += Time.deltaTime;
                yield return null;
            }
        }
        else if(!aGame)
        {
            SceneManager.LoadScene(sceneName);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            datas = FindObjectOfType<DataCollector>();
            elapsedTime = 0;
            while (elapsedTime < waitTime)
            {

                if (sceneName == "MainMenu")
                {
                    if (demo)
                    {
                        break;
                    }
                    mainMenu = FindObjectOfType<MainMenu>();
                    if (mainMenu.rumuzChanged)
                    {
                        elapsedTime = 0f;
                        waitTime = 5f;
                        mainMenu.rumuzChanged = false;
                    }
                }

                if ((sceneName.Equals("BGameInfo"))&&Input.GetMouseButtonDown(0))
                {
                    break;
                }
                if ((sceneName.Equals("FocusScene")) && Input.GetMouseButtonDown(0))
                {
                    break;
                }
                if ((sceneName.Equals("ImageScene")) && Input.GetMouseButtonDown(0))
                {
                    break;
                }
                if ((sceneName.Equals("GameScene")) && GameManager.Instance.balloonPoppedToPass)
                {
                    elapsedTime = 0;
                    waitTime = 2f;
                    GameManager.Instance.balloonPoppedToPass = false;
                }
                if ((sceneName.Equals("DemoFocusScene")) && Input.GetMouseButtonDown(0))
                {
                    break;
                }
                if ((sceneName.Equals("DemoImageScene")) && Input.GetMouseButtonDown(0))
                {
                    break;
                }
                if ((sceneName.Equals("DemoGameScene")) && GameManager.Instance.balloonPoppedToPass)
                {
                    elapsedTime = 0;
                    waitTime = 2f;
                    GameManager.Instance.balloonPoppedToPass = false;
                }
                elapsedTime += Time.deltaTime;

                yield return null;
                
            }
        }
        
    }


    private IEnumerator AGame(bool isFirst)
    {
        if (isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("MainMenu", mainMenuWaitTime, true));
        }
        else
        {
            GameManager.Instance.currentIndex = 0;
        }
        if (demo)
        {

            while (sceneCount < scenarioData.DemoScenarios.Count)
            {

                yield return StartCoroutine(LoadSceneAndWait("DemoMain", mainSceneWaitTime,true));
                yield return StartCoroutine(LoadSceneAndWait("DemoAction", actionSceneWaitTime, true));
                yield return StartCoroutine(LoadSceneAndWait("DemoCheck", checkSceneWaitTime, true));
                yield return StartCoroutine(LoadSceneAndWait("DemoCheckAnswer", 2.5f, true));

                GameManager.Instance.currentIndex++;
                sceneCount++;
            }
            GameManager.Instance.score = 0;
            GameManager.Instance.currentIndex = 0;
            sceneCount = 0;
            demo = false;
            scenarioData.InitializeData();
            yield return StartCoroutine(LoadSceneAndWait("DEMOTOMAIN", 3f, true));
        }
        while (sceneCount < scenarioData.Scenarios.Count)
        {
            yield return StartCoroutine(LoadSceneAndWait("MainScene", mainSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("ActionScene", actionSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("CheckScene", checkSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("CheckSceneAnswer", 2.5f, true));

            GameManager.Instance.currentIndex++;
            sceneCount++;
        }

        if (!isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("EndScene", 0f, true));
        }
    }

    private IEnumerator BGame(bool isFirst)
    {
        sceneCount = 0;
        if (isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("MainMenu", mainMenuWaitTime, true));
        }
        else
        {
            GameManager.Instance.currentIndex = 0;
        }
        if (demo)
        {
            sceneCount = 0;
            
            while (sceneCount < scenarioData2.DemoImagePairs.Count)
            {

                yield return StartCoroutine(LoadSceneAndWait("DemoFocusScene", focusSceneWaitTime, false));
                yield return StartCoroutine(LoadSceneAndWait("DemoImageScene", imageSceneWaitTime, false));
                yield return StartCoroutine(LoadSceneAndWait("DemoGameScene", gameSceneWaitTime, false));

                GameManager.Instance.currentIndex++;
                sceneCount++;
            }
            GameManager.Instance.score = 0;
            GameManager.Instance.currentIndex = 0;
            sceneCount = 0;
            demo = false;
            scenarioData.InitializeData();
            yield return StartCoroutine(LoadSceneAndWait("DEMOTOMAIN", 3f, true));
        }
        yield return StartCoroutine(LoadSceneAndWait("BGameInfo", 30f, false));

        while (sceneCount < scenarioData2.ImagePairs.Count)
        {
            yield return StartCoroutine(LoadSceneAndWait("FocusScene", focusSceneWaitTime, false));
            yield return StartCoroutine(LoadSceneAndWait("ImageScene", imageSceneWaitTime, false));
            yield return StartCoroutine(LoadSceneAndWait("GameScene", gameSceneWaitTime, false));

            GameManager.Instance.currentIndex++;
            sceneCount++;
        }

        if (!isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("EndScene", 0f, true));
        }

    }

    public bool isDemoOn()
    {
        return demo;
    }
    public void changeDemo(bool T_F)
    {
        demo=T_F;
    }
}
