using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

using System;
using System.Linq;


public class SceneTransitionManager : MonoBehaviour
{
    #region VARIABLES
    [HideInInspector] public string time;// BAŞLANGIÇ ZAMANI
    [HideInInspector] public string[] Ctime; // DEVAMLI ZAMAN
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
    [HideInInspector] public bool gameStarted = false;
    [HideInInspector] public bool skipScene = false;
    [HideInInspector] private Action action;
    private DataCollector collector;
    [HideInInspector] public bool demo=false;
    private int sceneCount = 0;
    private int tempOturumSayisi;
    [HideInInspector] public bool gameSelection;
    private int generalCount = 0;
    #endregion
    private void Start()
    {

        Ctime = new string[250];
        collector = FindAnyObjectByType<DataCollector>();
        DateTime dateAndTime = DateTime.Now;
        time = dateAndTime.ToString("yyyy-MM-dd HH:mm:ss");
        DontDestroyOnLoad(gameObject);
        if (!PlayerPrefs.HasKey("FirstTime"))
        {
            tempOturumSayisi = 1;
            PlayerPrefs.SetInt("OturumSayisi",1);
            PlayerPrefs.DeleteKey("FirstTime");
        }
        else
        {
            tempOturumSayisi = PlayerPrefs.GetInt("OturumSayisi")+1;
            Debug.Log(PlayerPrefs.GetInt("OturumSayisi"));

            PlayerPrefs.SetInt("OturumSayisi", tempOturumSayisi);
        }
        PlayerPrefs.Save();
        //StartCoroutine(gameChoice());

        //StartCoroutine(HandleSceneTransitionsA());
        //gameSelection=true;
        //StartCoroutine(HandleSceneTransitionsB());
        //gameSelection = false;
        //StartCoroutine(DemoA());
        //demo = true;
        StartCoroutine(dataTest());

    }


    private IEnumerator HandleSceneTransitionsA()
    {
        yield return StartCoroutine(AGame(true));
        yield return StartCoroutine(LoadSceneAndWait("Transition", transitionWaitTime, true));
        GameManager.Instance.AgameCount = GameManager.Instance.currentIndex;
        sceneCount = 0;
        GameManager.Instance.currentIndex = 0;
        yield return StartCoroutine(BGame(false));
        GameManager.Instance.BgameCount = GameManager.Instance.currentIndex;
        GameManager.Instance.oturumSayisi = tempOturumSayisi;
        yield return StartCoroutine(LoadSceneAndWait("EndScene", 300f, true));
        yield return StartCoroutine(LoadSceneAndWait("Scoreboard",180f, true));

    }
    private IEnumerator HandleSceneTransitionsB()
    {

        yield return StartCoroutine(BGame(true));
        yield return StartCoroutine(LoadSceneAndWait("Transition", transitionWaitTime, false));
        GameManager.Instance.BgameCount = GameManager.Instance.currentIndex;
        sceneCount = 0;
        GameManager.Instance.currentIndex = 0;
        yield return StartCoroutine(AGame(false));
        GameManager.Instance.AgameCount = GameManager.Instance.currentIndex;
        GameManager.Instance.oturumSayisi = tempOturumSayisi;
        yield return StartCoroutine(LoadSceneAndWait("EndScene", 300f, true));
        yield return StartCoroutine(LoadSceneAndWait("Scoreboard", 180f, true));

    }

    private IEnumerator LoadSceneAndWait(string sceneName, float waitTime,bool aGame)
    {
        bool oneTime = true;
            if (aGame)
            {
            SceneManager.LoadScene(sceneName);
            yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
            datas = FindObjectOfType<DataCollector>();
            elapsedTime = 0;
            while (elapsedTime < waitTime)
            {
                if (sceneName == "PreGame" && skipScene)
                {
                    skipScene= false;
                    break;
                }
                if (sceneName == "MainMenu")
                {
                    mainMenu = FindObjectOfType<MainMenu>();
                    if (skipScene)
                    {
                        skipScene=false;
                        break;
                    }
                    if (mainMenu.rumuzChanged)
                    {
                        elapsedTime = 0f;
                        waitTime = 10f;
                        mainMenu.rumuzChanged = false;
                        
                    }
                }

                if (( sceneName == "WelcomeSceneP3") && gameStarted)
                {
                    break;
                }

                if ((sceneName == "AGameInfo" || sceneName == "WelcomeScene" || sceneName == "WelcomeSceneP2" || sceneName == "AGameInfoP2"  || sceneName == "AGameInfo2"||sceneName=="AGameInfo2DEMO"|| sceneName == "DEMOTOMAIN" || sceneName == "Transition" || sceneName == "AGameInfo2Alıştırma") && skipScene)
                {
                    skipScene = false;
                    break;
                }

                if ((sceneName == "MainScene" || sceneName == "DemoMain")&&elapsedTime>5f)
                {
                    MainScene ms = FindAnyObjectByType<MainScene>();
                    if(ms == null)
                    {
                        DemoMainScene dms = FindAnyObjectByType<DemoMainScene>();

                        dms.skip_buttonReveal();
                    }
                    else
                        ms.skip_buttonReveal();
                    
                    if (skipScene)
                    {
                        skipScene = false;
                        break; // MainScene'de tıklama ile bekleme süresi bitiriliyor
    

                    }
                }
                if((sceneName == "ActionScene") || (sceneName == "DemoAction"))
                {
                    // Bütün harflerin aşağıya düşmesi
                    action=FindAnyObjectByType<Action>();
                    if(action == null)
                    {
                        DemoAction ac = FindAnyObjectByType<DemoAction>();
                        if (ac.didEnd)
                            break;
                    }
                    else
                    {
                        if (action.didEnd)
                            break;
                    }

                }
                if ((sceneName == "ActionScene" && actionSceneConditionMet) || (sceneName == "DemoAction" && actionSceneConditionMet))
                {
                    actionSceneConditionMet = false;
                    waitTime = Mathf.Min(9f, waitTime); // ActionScene'de koşul karşılanırsa süre 5 saniyeye düşüyor

                }
                if(((sceneName == "CheckScene")|| (sceneName == "DemoCheck")) && elapsedTime > 20f)
                {
                    datas.InputCheckTime[GameManager.Instance.currentIndex] = -1f;
                    break;
                }
                if ((sceneName == "CheckScene" && checkSceneConditionMet) || (sceneName == "DemoCheck" && checkSceneConditionMet))
                {
                    datas.InputCheckTime[GameManager.Instance.currentIndex] = elapsedTime;
                    checkSceneConditionMet = false;
                    break;
                }

                if (sceneName == "EndScene" && skipScene || !(elapsedTime < waitTime))
                {
                    skipScene = false;
                    break;
                }
                if (sceneName == "Scoreboard"&&oneTime)
                {
                    GameManager.Instance.AddScore();
                    oneTime = !oneTime;
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
                    mainMenu = FindObjectOfType<MainMenu>();
                    if (skipScene)
                    {
                        skipScene = false;
                        break;
                    }
                    if (mainMenu.rumuzChanged)
                    {
                        elapsedTime = 0f;
                        waitTime = 10f;
                        mainMenu.rumuzChanged = false;

                    }
                }
                if (sceneName == "WelcomeSceneP3" && gameStarted)
                {
                    break;
                }

                if ((sceneName== "BGameInfo" || sceneName == "BGameInfoP2" || sceneName == "WelcomeScene" || sceneName == "WelcomeSceneP2" || sceneName == "Transition" || sceneName == "DEMOTOMAIN") && skipScene) 
                {
                    skipScene = false;
                    break;
                }
                if ((sceneName.Equals("GameScene")) && GameManager.Instance.balloonPoppedToPass || (sceneName.Equals("DemoGameScene")) && GameManager.Instance.balloonPoppedToPass)
                {
                    elapsedTime = 0;
                    waitTime = 2f;
                    GameManager.Instance.balloonPoppedToPass = false;
                }
                if (sceneName == "EndScene" && skipScene || !(elapsedTime < waitTime))
                {
                    skipScene = false;
                    break;
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
            yield return StartCoroutine(LoadSceneAndWait("WelcomeScene", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("WelcomeSceneP2", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("WelcomeSceneP3", 3600f, true));

            yield return StartCoroutine(LoadSceneAndWait("AGameInfo", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("AGameInfoP2", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("AGameInfo2", 3600f, true));

            GameManager.Instance.score = 0;
            sceneCount = 0;
            while (sceneCount < scenarioData.Scenarios.Count
                )
            {
                yield return StartCoroutine(LoadSceneAndWait("MainScene", mainSceneWaitTime, true));
                yield return StartCoroutine(LoadSceneAndWait("ActionScene", actionSceneWaitTime, true));
                Ctime[generalCount] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                generalCount++;
                yield return StartCoroutine(LoadSceneAndWait("CheckScene", checkSceneWaitTime, true));
                if (datas.InputCheckTime[GameManager.Instance.currentIndex] != -1f)
                {
                    yield return StartCoroutine(LoadSceneAndWait("CheckSceneAnswer", 2.5f, true));
                }

                GameManager.Instance.currentIndex++;
                sceneCount++;
            }
        }
        else
        {
            yield return StartCoroutine(LoadSceneAndWait("AGameInfo", 30f, true));
            yield return StartCoroutine(LoadSceneAndWait("AGameInfoP2", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("AGameInfo2", 3600f, true));
            while (sceneCount < scenarioData.Scenarios.Count)
            {
                yield return StartCoroutine(LoadSceneAndWait("MainScene", mainSceneWaitTime, true));
                yield return StartCoroutine(LoadSceneAndWait("ActionScene", actionSceneWaitTime, true));
                Ctime[generalCount] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                generalCount++;
                yield return StartCoroutine(LoadSceneAndWait("CheckScene", checkSceneWaitTime, true));
                if (datas.InputCheckTime[GameManager.Instance.currentIndex] != -1f)
                    yield return StartCoroutine(LoadSceneAndWait("CheckSceneAnswer", 2.5f, true));
                GameManager.Instance.currentIndex++;
                sceneCount++;
            }

        }

    }

    private IEnumerator BGame(bool isFirst)
    {
        if (isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("MainMenu", mainMenuWaitTime, false));
            sceneCount = 0;
            GameManager.Instance.currentIndex = 0;
            yield return StartCoroutine(LoadSceneAndWait("WelcomeScene", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("WelcomeSceneP2", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("WelcomeSceneP3", 3600f, true));
            yield return StartCoroutine(LoadSceneAndWait("BGameInfo", 30f, false));
            yield return StartCoroutine(LoadSceneAndWait("BGameInfoP2", 30f, false));
            
            while (sceneCount < scenarioData2.ImagePairs.Count)
            {
                collector = FindAnyObjectByType<DataCollector>();
                collector.fillDatasB();
                yield return StartCoroutine(LoadSceneAndWait("FocusScene", focusSceneWaitTime, false));
                yield return StartCoroutine(LoadSceneAndWait("ImageScene", imageSceneWaitTime, false));
                yield return StartCoroutine(LoadSceneAndWait("GameScene", gameSceneWaitTime, false));
                Ctime[generalCount] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                generalCount++;
                GameManager.Instance.currentIndex++;
                sceneCount++;
            }
        }
        else
        {
            sceneCount = 0;
            GameManager.Instance.currentIndex = 0;
            yield return StartCoroutine(LoadSceneAndWait("BGameInfo", 30f, false));
            yield return StartCoroutine(LoadSceneAndWait("BGameInfoP2", 30f, false));
            
            while (sceneCount < scenarioData2.ImagePairs.Count
                )
            {

                yield return StartCoroutine(LoadSceneAndWait("FocusScene", focusSceneWaitTime, false));
                collector = FindAnyObjectByType<DataCollector>();
                collector.fillDatasB();
                yield return StartCoroutine(LoadSceneAndWait("ImageScene", imageSceneWaitTime, false));
                yield return StartCoroutine(LoadSceneAndWait("GameScene", gameSceneWaitTime, false));
                Ctime[generalCount] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                generalCount++;

                GameManager.Instance.currentIndex++;
                sceneCount++;
            }
        }

        


    }
    private IEnumerator DemoA()
    {
        yield return StartCoroutine(LoadSceneAndWait("MainMenu", mainMenuWaitTime, true));
        yield return StartCoroutine(LoadSceneAndWait("WelcomeScene", 3600f, true));
        yield return StartCoroutine(LoadSceneAndWait("WelcomeSceneP2", 3600f, true));
        yield return StartCoroutine(LoadSceneAndWait("WelcomeSceneP3", 3600f, true));

        yield return StartCoroutine(LoadSceneAndWait("AGameInfo", 3600f, true));
        yield return StartCoroutine(LoadSceneAndWait("AGameInfoP2", 3600f, true));
        yield return StartCoroutine(LoadSceneAndWait("AGameInfo2DEMO", 3600f, true));

        while (sceneCount < scenarioData.DemoScenarios.Count)
        {

            yield return StartCoroutine(LoadSceneAndWait("DemoMain", mainSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("DemoAction", actionSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("DemoCheck", checkSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("DemoCheckAnswer", 2.5f, true));

            GameManager.Instance.currentIndex++;
            sceneCount++;
        }
        yield return StartCoroutine(LoadSceneAndWait("DemoEnd", 60f, true));
        Application.Quit();
    }
    private IEnumerator dataTest()
    {
        yield return StartCoroutine(LoadSceneAndWait("MainMenu", mainMenuWaitTime, true));
        GameManager.Instance.score = 0;
        while (sceneCount < 2)
        {
            yield return StartCoroutine(LoadSceneAndWait("MainScene", mainSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("ActionScene", actionSceneWaitTime, true));
            yield return StartCoroutine(LoadSceneAndWait("CheckScene", checkSceneWaitTime, true));
            if (datas.InputCheckTime[GameManager.Instance.currentIndex] != -1f)
            {
                yield return StartCoroutine(LoadSceneAndWait("CheckSceneAnswer", 2.5f, true));
            }

            GameManager.Instance.currentIndex++;
            sceneCount++;
        }
        GameManager.Instance.AgameCount = GameManager.Instance.currentIndex;
        sceneCount = 0;
        GameManager.Instance.currentIndex = 0;
        while (sceneCount < 2)
        {

            yield return StartCoroutine(LoadSceneAndWait("FocusScene", focusSceneWaitTime, false));
            collector = FindAnyObjectByType<DataCollector>();
            collector.fillDatasB();
            yield return StartCoroutine(LoadSceneAndWait("ImageScene", imageSceneWaitTime, false));
            yield return StartCoroutine(LoadSceneAndWait("GameScene", gameSceneWaitTime, false));

            GameManager.Instance.currentIndex++;
            sceneCount++;
        }
        GameManager.Instance.BgameCount = GameManager.Instance.currentIndex;
        GameManager.Instance.oturumSayisi = tempOturumSayisi;
        yield return StartCoroutine(LoadSceneAndWait("EndScene", 300f, true));
        yield return StartCoroutine(LoadSceneAndWait("Scoreboard", 180f, true));
    }
    private IEnumerator gameChoice()
    {
        yield return LoadSceneAndWait("PreGame",1800, true);
        if (gameSelection)
        {
            StartCoroutine(HandleSceneTransitionsA());
        }
        else
            StartCoroutine(HandleSceneTransitionsB());
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
