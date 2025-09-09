using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DemoAction : MonoBehaviour
{
    private DataCollector datas;
    [SerializeField] private ScenarioDatas scenarioData;

    private Box box;
    #region Texts
    [SerializeField] private TextMeshProUGUI textUGUI;
    [SerializeField] private TextMeshProUGUI scoreUGUI;
    private string missingText;
    private string trueText;
    private char trueLetter;
    #endregion

    #region Letters
    private Dictionary<char, GameObject> letterDict = new Dictionary<char, GameObject>();
    [SerializeField] private GameObject[] letters = new GameObject[29];
    private List<GameObject> spawnedLetters = new List<GameObject>();
    [SerializeField] private GameObject Spawnpoint1;
    [SerializeField] private GameObject Spawnpoint2;

    #endregion

    [HideInInspector] public bool oneTime;
    [HideInInspector] public bool isCorrectLetter;
    [HideInInspector] public bool isDraggingForBox = false;
    private bool didStart;
    [HideInInspector] public bool didEnd;
    private float timer;

    private void Update()
    {

        scoreUGUI.text = GameManager.Instance.score.ToString();

        collectData();
        timer += Time.deltaTime;
        if (timer > 6f)
        {
            if (!didStart)
            {
                StartCoroutine(SpawnLetters());
                didStart = true;
            }

            // Harflerin ekranın dışına düşüp düşmediğini kontrol et
            for (int i = spawnedLetters.Count - 1; i >= 0; i--)
            {
                if (spawnedLetters[i] == null)
                {
                    spawnedLetters.RemoveAt(i);
                    continue;
                }

                if (spawnedLetters[i].transform.position.y < -5) // -5 y koordinatı sınırını temsil eder
                {
                    GameObject letterObj = spawnedLetters[i];
                    spawnedLetters.RemoveAt(i);
                    Destroy(letterObj);


                }
                if (spawnedLetters.Count <= 0)
                {
                    didEnd = true;
                }
            }


        }
    }
    private void Start()
    {
        // Türkçe alfabenin eklenmesi
        letterDict.Add('A', letters[0]);
        letterDict.Add('B', letters[1]);
        letterDict.Add('C', letters[2]);
        letterDict.Add('Ç', letters[3]);
        letterDict.Add('D', letters[4]);
        letterDict.Add('E', letters[5]);
        letterDict.Add('F', letters[6]);
        letterDict.Add('G', letters[7]);
        letterDict.Add('Ğ', letters[8]);
        letterDict.Add('H', letters[9]);
        letterDict.Add('I', letters[10]);
        letterDict.Add('İ', letters[11]);
        letterDict.Add('J', letters[12]);
        letterDict.Add('K', letters[13]);
        letterDict.Add('L', letters[14]);
        letterDict.Add('M', letters[15]);
        letterDict.Add('N', letters[16]);
        letterDict.Add('O', letters[17]);
        letterDict.Add('Ö', letters[18]);
        letterDict.Add('P', letters[19]);
        letterDict.Add('R', letters[20]);
        letterDict.Add('S', letters[21]);
        letterDict.Add('Ş', letters[22]);
        letterDict.Add('T', letters[23]);
        letterDict.Add('U', letters[24]);
        letterDict.Add('Ü', letters[25]);
        letterDict.Add('V', letters[26]);
        letterDict.Add('Y', letters[27]);
        letterDict.Add('Z', letters[28]);
        datas = FindAnyObjectByType<DataCollector>();
        box = FindAnyObjectByType<Box>();
        // Scriptable Object'ten veri alımı
        missingText = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoActionText;
        trueText = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoActionTrueText;
        trueLetter = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoActionTrueLetter.ToUpper()[0];
        // Initialization
        isCorrectLetter = false;
        didStart = false;
        oneTime = false;
        spawnedLetters.Clear();
        timer = 0f;
    }
    IEnumerator SpawnLetters()
    {
        Vector2 spawnRange;

        // Doğru harfi listeye ekle
        List<char> lettersList = new List<char> { trueLetter };

        // Rastgele diğer harfleri ekle
        while (lettersList.Count < 5)
        {
            int randomLetterIndex = Random.Range(0, letters.Length);
            if (!lettersList.Contains(letters[randomLetterIndex].name[0]) && letterDict.ContainsKey(letters[randomLetterIndex].name[0]))
            {
                lettersList.Add(letters[randomLetterIndex].name[0]);
            }
        }


        // Harfleri rastgele sıraya koy
        for (int i = lettersList.Count - 1; i > 0; i--)
        {
            int randIndex = Random.Range(0, i + 1);
            char temp = lettersList[i];
            lettersList[i] = lettersList[randIndex];
            lettersList[randIndex] = temp;
        }

        // Harfleri yarat ve sahneye yerleştir
        List<Vector2> usedPositions = new List<Vector2>(); // Önceden kullanılan koordinatlar
        float minDistance = 0.5f; // İki harf arasında olması gereken minimum mesafe

        for (int i = 0; i < lettersList.Count; i++)
        {
            char letter = lettersList[i];
            bool isPositionValid;

            int maxAttempts = 100;
            int attemptCount = 0;

            do
            {
                isPositionValid = true;

                spawnRange = new Vector2(
                    Random.Range(Spawnpoint1.transform.position.x, Spawnpoint2.transform.position.x),
                    Spawnpoint1.transform.position.y
                );

                if (!(spawnRange.x > 0.83f || spawnRange.x < -0.77f))
                {
                    isPositionValid = false;
                    continue;
                }

                foreach (Vector2 pos in usedPositions)
                {
                    if (Vector2.Distance(pos, spawnRange) < minDistance)
                    {
                        isPositionValid = false;
                        break;
                    }
                }

                attemptCount++;
                if (attemptCount >= maxAttempts)
                {
                    Debug.LogWarning("Max attempts reached for finding a valid position.");
                    break;
                }
            }
            while (!isPositionValid);


            // Yeni pozisyonu listeye ekle
            usedPositions.Add(spawnRange);

            GameObject letterObj;

            if (letterDict.TryGetValue(letter, out letterObj))
            {
                // Harfi oluştur ve sahneye ekle
                letterObj = Instantiate(letterObj, spawnRange, Quaternion.identity);
                spawnedLetters.Add(letterObj);
            }

            yield return new WaitForSeconds(0.5f); // Yarım saniye bekle
        }



    }
    public void revealText()
    {
        if (box.onBox && !oneTime)
        {
            if (!isCorrectLetter)
            {
                textUGUI.color = Color.red;
            }
            else
            {
                textUGUI.color = Color.green;
            }
            datas = FindAnyObjectByType<DataCollector>();
            datas.actionTimes[GameManager.Instance.currentIndex] = timer;
            textUGUI.text = scenarioData.DemoScenarios[GameManager.Instance.currentIndex].DemoActionTrueText;


        }
    }
    public void collectData()
    {
        if (!didEnd)
        {
            timer += Time.deltaTime;
        }
        else
        {
            datas.actionTimes[GameManager.Instance.currentIndex] = timer;
            datas.actionAns[GameManager.Instance.currentIndex] = isCorrectLetter;
        }
    }
}
