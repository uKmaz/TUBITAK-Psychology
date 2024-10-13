
using System.Text;
using System.IO;
using TMPro;
using UnityEngine;


public class DataCollector : MonoBehaviour
{
    #region Singleton
    public static DataCollector Instance;
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
    #endregion
    #region DATAS
    private SceneTransitionManager STM;
    [SerializeField] private ScenarioDatas scenarioDatas;
    private string rumuz;
    // Senaryodan arrayler - Start fonksiyonunda alındı
    [HideInInspector] public string[] mainSceneText;
    [HideInInspector] public string[] actionSceneText;
    [HideInInspector] public string[] actionSceneTrueLetter;
    [HideInInspector] public string[] checkUpQuestion;
    [HideInInspector] public bool[] checkUpAnswer;
    // Action'dan alınacak
    [HideInInspector] public float[] actionTimes;
    [HideInInspector] public bool[] actionAns;
    // CheckManager'dan alınacak
    [HideInInspector] public float[] InputCheckTime;
    // ButtonManager'dan alınacak
    [HideInInspector] public bool[] InputCheckAnswer;
    [HideInInspector] public bool[] isInputCorrect;
    // Letters'dan alınacak
    [HideInInspector] public string[] letterChosen;
    #endregion
    [SerializeField] private TMP_InputField userInput;
    MainMenu mainMenu;


    public void Start()
    {
        mainMenu= FindAnyObjectByType<MainMenu>();
        STM=FindAnyObjectByType<SceneTransitionManager>();
        userInput.onValueChanged.AddListener(OnInputValueChanged);
        int count= scenarioDatas.Scenarios.Count;
        mainSceneText = new string[count];
        actionSceneText = new string[count];
        actionSceneTrueLetter = new string[count];
        checkUpQuestion = new string[count];
        checkUpAnswer = new bool[count];
        actionTimes= new float[count];
        actionAns = new bool[count];
        letterChosen = new string[count];
        

    }

    public void OnInputValueChanged(string inputText)
    {
        
        mainMenu.rumuzChanged = true;
        rumuz = inputText;
        
    }
    public void ExportDataToCSV()
    {

        //TARİH SAAT - MainSceneText - ActionSceneText - ActionSceneTrueLetter - letterChosen - actionTimes - CheckUpQuestion - CheckUpAnswer - InputCheckAnswer - isInputCorrect - InputCheckTime
        // CSV dosyasının kaydedileceği dosya yolu
        string filePath = Application.dataPath + "/"+rumuz+".csv";

        // CSV dosyasının başlık satırı
        StringBuilder csvContent = new StringBuilder();
        csvContent.AppendLine("Senaryo, Boşluklu yazı, Seçilmesi gereken harf, Seçilen harf, Geçirilen Süre, Kontrol Sorusu, Doğru cevap, Verilen cevap, D/Y, Geçirilen süre");
        //csvContent.AppendLine(rumuz+", "+ STM.time);
        // Örnek veriler

        for( int i = 0; i < 36; i++)
        {
            csvContent.AppendLine(mainSceneText[i].ToLower() + ", " + actionSceneText[i].ToLower() + ", " + actionSceneTrueLetter[i].ToLower() + ", " 
                + letterChosen[i].ToLower() + ", " + actionTimes[i] + ", " + checkUpQuestion[i].ToLower() + ", " + checkUpAnswer[i] + ", "
                + InputCheckAnswer[i] + ", " + isInputCorrect[i] + ", " + InputCheckTime[i]);
        }


        // Dosyayı oluştur ve verileri yaz
        File.WriteAllText(filePath, csvContent.ToString());
        Debug.Log("CSV dosyası başarıyla oluşturuldu: " + filePath);
    }


    public void fillDatas()
    {
        int currentIndex = GameManager.Instance.currentIndex;
            mainSceneText[currentIndex] = scenarioDatas.Scenarios[currentIndex].MainSceneText;
            actionSceneText[currentIndex] = scenarioDatas.Scenarios[currentIndex].ActionSceneText;
            actionSceneTrueLetter[currentIndex] = scenarioDatas.Scenarios[currentIndex].ActionSceneTrueLetter;
            checkUpQuestion[currentIndex] = scenarioDatas.Scenarios[currentIndex].CheckUpQuestion;
            checkUpAnswer[currentIndex] = scenarioDatas.Scenarios[currentIndex].CheckUpAnswer;
        

    }

    public void DebugLog()
    {
        int currentIndex = GameManager.Instance.currentIndex;
        Debug.Log(rumuz);
        Debug.Log(mainSceneText[currentIndex]+", "+ actionSceneText[currentIndex] + ", " + actionSceneTrueLetter[currentIndex] + ", " + checkUpQuestion[currentIndex]
            + ", " + checkUpAnswer[currentIndex]);
    }
}
