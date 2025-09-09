
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [HideInInspector] public int currentIndex = 0;
    [HideInInspector] public int score = 0;
    [HideInInspector] public bool balloonPopped;
    [HideInInspector] public bool balloonPoppedToPass;
    [HideInInspector] public bool arrowClickedOnce;
    [HideInInspector] public int oturumSayisi;
    [SerializeField] private TextMeshProUGUI list;
    [HideInInspector] public int AgameCount;
    [HideInInspector] public int BgameCount;
    public TextMeshProUGUI[] textMeshes;
    private DataCollector collector;
    private SceneTransitionManager sceneTransitionManager;
    
    public void Scoreboard()
    {
        textMeshes=FindObjectsOfType<TextMeshProUGUI>();
        for(int i =0; i < textMeshes.Length; i++)
        {
            if (textMeshes[i].name == "List")
                list = textMeshes[i];
        }
        list.text = "";


        int maxScore = PlayerPrefs.GetInt("MaxScore");

            list.text += "Puanınız : "+score+ "\n";
            
            if (score > maxScore)
            {
                PlayerPrefs.SetInt("MaxScore", score);
                list.text += "YENİ REKOR : " + score + "\n";
            }
            else
            {
                list.text += "Rekorunuz : " + maxScore + "\n";
            }
            list.text += "Tebrikler !!!";
            PlayerPrefs.Save();
        collector = FindAnyObjectByType<DataCollector>();
        collector.ExportDataToCSV();
    }
    public void AddScore()
    {
        // Mevcut skorları al
        string currentScores = PlayerPrefs.GetString("allScores", "");

        // Yeni skoru virgül ile ekle
        if (!string.IsNullOrEmpty(currentScores))
        {
            currentScores += "," + score.ToString(); // Mevcut skorlar varsa araya virgül ekle
        }
        else
        {
            currentScores = score.ToString(); // İlk skor ise direkt ekle
        }

        // Güncellenmiş skoru kaydet
        PlayerPrefs.SetString("allScores", currentScores);
        Scoreboard();
        PlayerPrefs.Save();
    }
    public void PrintAllScores()
    {
        // "allScores" anahtarındaki string'i al
        string allScores = PlayerPrefs.GetString("allScores", "");
        textMeshes = FindObjectsOfType<TextMeshProUGUI>();
        for (int i = 0; i < textMeshes.Length; i++)
        {
            if (textMeshes[i].name == "List")
                list = textMeshes[i];
        }
        list.text = "";
        // Eğer skorlar varsa, virgül ile ayır
        if (!string.IsNullOrEmpty(allScores))
        {
            string[] scoreArray = allScores.Split(',');
            // Her bir skoru ekranda yazdır
            for (int i = 0; i < scoreArray.Length; i++)
            {
                list.text += "Skor " + (i + 1) + ": " + scoreArray[i]+"\n" ;
            }
        }
        else
        {
            list.text = "Henüz kaydedilmiş bir skor yok.";
        }
    }


    private void Start()
    {
        sceneTransitionManager=FindAnyObjectByType<SceneTransitionManager>();
        AgameCount = 0;
        BgameCount = 0;
        arrowClickedOnce = false;
        balloonPopped = false;
        if(!PlayerPrefs.HasKey("MaxScore"))
        {
            PlayerPrefs.SetInt("MaxScore", 0);
        }

        PlayerPrefs.Save();
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
