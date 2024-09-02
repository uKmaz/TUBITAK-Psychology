using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private float mainMenuWaitTime;  // MainMenu'de Bekleme Süresi
    [SerializeField] private float mainSceneWaitTime;   // MainScene'de bekleme süresi
    [SerializeField] private float actionSceneWaitTime; // ActionScene'de bekleme süresi
    [SerializeField] public float checkSceneWaitTime;  // CheckScene'de bekleme süresi
    [SerializeField] private float transitionWaitTime;  // A oyunundan B oyununa geçiş sahnesi bekleme süresi
    [SerializeField] private ScenarioDatas scenarioData;      // ScenarioDatas scriptable object referansı
    [HideInInspector] public bool actionSceneConditionMet=false;
    [HideInInspector] public bool checkSceneConditionMet = false;
    private int sceneCount = 0;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        StartCoroutine(HandleSceneTransitions());
    }

    private IEnumerator HandleSceneTransitions()
    {
        yield return StartCoroutine(AGame(true, false));
        yield return StartCoroutine(LoadSceneAndWait("Transition", transitionWaitTime));
        yield return StartCoroutine(BGame(false));
    }

    private IEnumerator LoadSceneAndWait(string sceneName, float waitTime)
    {
        SceneManager.LoadScene(sceneName);
        float elapsedTime = 0f;

        while (elapsedTime < waitTime)
        {
            if (sceneName == "MainScene" && Input.GetMouseButtonDown(0))
            {
                break; // MainScene'de tıklama ile bekleme süresi bitiriliyor
            }

            if (sceneName == "ActionScene" && actionSceneConditionMet)
            {
                actionSceneConditionMet = false;
                waitTime = Mathf.Min(5f, waitTime); // ActionScene'de koşul karşılanırsa süre 5 saniyeye düşüyor
            }

            if (sceneName == "CheckScene" && checkSceneConditionMet)
            {
                checkSceneConditionMet = false;
                waitTime=Mathf.Min(2f,waitTime);
                break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }



    private IEnumerator TutorialCoroutine()
    {
        yield return new WaitForSeconds(5f);
    }

    private IEnumerator AGame(bool isFirst, bool silinecek)
    {
        if (silinecek)
        {
            if (PlayerPrefs.GetInt("FirstTime", 1) == 1)
            {
                yield return StartCoroutine(TutorialCoroutine());
                if (!isFirst)
                {
                    PlayerPrefs.SetInt("FirstTime", 0);
                }
            }
        }
        yield return StartCoroutine(LoadSceneAndWait("MainMenu", mainMenuWaitTime));
        while (sceneCount < scenarioData.Scenarios.Count)
        {

            yield return StartCoroutine(LoadSceneAndWait("MainScene", mainSceneWaitTime));
            yield return StartCoroutine(LoadSceneAndWait("ActionScene", actionSceneWaitTime));
            yield return StartCoroutine(LoadSceneAndWait("CheckScene", checkSceneWaitTime));

            GameManager.Instance.currentIndex++;
            sceneCount++;
        }

        if (!isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("EndScene", 0f));
        }
    }

    private IEnumerator BGame(bool isFirst)
    {
        if (PlayerPrefs.GetInt("FirstTime", 1) == 1)
        {
            yield return StartCoroutine(TutorialCoroutine());
            if (!isFirst)
            {
                PlayerPrefs.SetInt("FirstTime", 0);
            }
        }

        yield return new WaitForSeconds(10f);

        if (!isFirst)
        {
            yield return StartCoroutine(LoadSceneAndWait("EndScene", 0f));
        }

    }
    
}
