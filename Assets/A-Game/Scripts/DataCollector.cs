using System;
using System.Text;
using System.IO;
using TMPro;
using UnityEngine;
using System.Net;
using System.Net.Mail;



public class DataCollector : MonoBehaviour
{
    bool onetime = false;
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
    #region DATAS FOR A
    private SceneTransitionManager STM;
    [SerializeField] private ScenarioDatas scenarioDatas;
    private string rumuz;
    // Senaryodan arrayler - MainScene'den alınacak
    [HideInInspector] public string[] type;
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
    #region DATAS FOR B
    [SerializeField] private ScenarioDatas2 scenarioDatas2;
    // BalloonScript'ten alınacak
    [HideInInspector] public bool[] chosenBalloon;
    // GameSceneManager'dan alınacak
    [HideInInspector] public float[] time_spent;
    [HideInInspector] public bool[] upOrDown;
    [HideInInspector] public int AgameCount;
    [HideInInspector] public int BgameCount;
    
    #endregion

    public void Start()
    {
        mainMenu = FindAnyObjectByType<MainMenu>();
        STM=FindAnyObjectByType<SceneTransitionManager>();
        userInput.ActivateInputField();
        userInput.onValueChanged.AddListener(OnInputValueChanged);
        userInput.onSelect.AddListener(OpenKeyboard);    // When input field is selected
        //A
        int count= scenarioDatas.Scenarios.Count;
        AgameCount = count;
        type = new string[count];
        mainSceneText = new string[count];
        actionSceneText = new string[count];
        actionSceneTrueLetter = new string[count];
        checkUpQuestion = new string[count];
        checkUpAnswer = new bool[count];
        actionTimes= new float[count];
        actionAns = new bool[count];
        letterChosen = new string[count];
        //B
        scenarioDatas2.initializeData();
        int count2 = scenarioDatas2.ImagePairs.Count;
        BgameCount = count2;
        upOrDown = new bool[count2];
        chosenBalloon = new bool[count2];
        time_spent = new float[count2];




    }
    void OpenKeyboard(string _)
    {
        if (Application.isMobilePlatform)
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }
    }
    public void OnInputValueChanged(string inputText)
    {
        
        mainMenu.rumuzChanged = true;
        rumuz = inputText;
        
    }
    public void ExportDataToCSV()
    {
        string filePath = Application.persistentDataPath + "/" + rumuz + " " + GameManager.Instance.oturumSayisi + ". Oturum" + ".csv";

        StringBuilder csvContent = new StringBuilder();
        int additionA = 0;// A VE B'NİN Ctime LARI İÇİN
        int additionB = 0;
        string gameChoice=" ";
        if (STM.gameSelection)
        {
            gameChoice = "İlk MY oynandı.";
            additionA = 0;
            additionB = GameManager.Instance.AgameCount;

        }
        else
        {
            gameChoice = "İlk MD oynandı.";
            additionA = GameManager.Instance.BgameCount;
            additionB = 0; ;
        }
        
        
        csvContent.AppendLine(rumuz + "\t " + STM.time+"\t "+gameChoice);
        // Sekme 1: A OYUNU
        csvContent.AppendLine("my | MY");
        csvContent.AppendLine("Tarih ve Saat\t Tür\t Senaryo\t Boşluklu Yazı\t Seçilmesi Gereken Harf\t Seçilen Harf\t Tepki Süresi\t Kontrol Sorusu\t Doğru Cevap\t Verilen Cevap\t Sonuç\t Tepki Süresi");

        for (int i = 0; i < GameManager.Instance.AgameCount; i++)
        {
            csvContent.AppendLine($"{STM.Ctime[i+additionA]}\t {type[i]}\t {mainSceneText[i].ToString().ToLower()}\t { actionSceneText[i].ToString().ToLower()}\t {actionSceneTrueLetter[i]}\t {letterChosen[i]}\t {actionTimes[i]}\t {checkUpQuestion[i].ToString().ToLower()}\t {checkUpAnswer[i]}\t {InputCheckAnswer[i]}\t {isInputCorrect[i]}\t {InputCheckTime[i]}");

                                  
                                  
                                  
        }

        // Sekme 2: DİKKAT | MD
        csvContent.AppendLine("\nDİKKAT | MD");
        csvContent.AppendLine($"Tarih ve Saat\t Fotoğraf Çifti\t Nötr Fotoğraf\t OKB Fotoğraf \tYapılan Seçim\t Seçilen Balon Rengi\t Sonuç\t Tepki Süresi");
        for (int i = 0; i < GameManager.Instance.BgameCount; i++)
        {
            string up_down = upOrDown[i] ? "Üst" : "Alt";
            string up_downOpp = upOrDown[i] ? "Alt" : "Üst";
            string chosen = chosenBalloon[i] ? "Üst" : "Alt";
            string renk = scenarioDatas2.ImagePairs[i].colorOfTrueBalloon ? "Sarı-Mor" : "Kırmızı Mavi";
            bool result = up_down.Equals(chosen);
            string sonuc = result ? "Doğru" : "Yanlış";
            csvContent.AppendLine($"{STM.Ctime[i+ additionB]}\t Çift {scenarioDatas2.ImagePairs[i].image2.name[0]}\t {up_down}\t {up_downOpp}\t{chosen} \t {renk} \t {sonuc}\t {time_spent[i]}");
        }

        // Toplam Skor ve Diğer Bilgiler
        csvContent.AppendLine("\nToplam Skor: " + GameManager.Instance.score);


        // Dosyayı yaz
        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            using (var writer = new StreamWriter(stream, Encoding.UTF8))
            {
                writer.Write(csvContent.ToString());
            }
        }
        Debug.Log("CSV dosyası başarıyla oluşturuldu: " + filePath);

        FindAnyObjectByType<FirebaseCSVUploader>().UploadCSV(filePath);
    }

    private void SendEmailWithAttachment(string recipientEmail)
    {
        if (!onetime)
        {
            try
            {
                string senderEmail = "mail.senderemre@gmail.com";
                string senderPassword = "uvzrnlmasznoevfv";
                string smtpServer = "smtp.gmail.com";
                int smtpPort = 587;
                string[] csvFiles = Directory.GetFiles(Application.persistentDataPath, "*Oturum.csv");
                foreach (string filePaths in csvFiles)
                {
                    // E-posta mesajı oluştur
                    MailMessage mail = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = "KULLANICI VERİLERİ",
                        Body = "Veriler ekteki .csv dosyasındadır."
                    };
                    mail.To.Add(recipientEmail);
                    mail.Attachments.Add(new Attachment(filePaths));

                    // SMTP istemcisi oluştur
                    using (SmtpClient smtp = new SmtpClient(smtpServer, smtpPort))
                    {
                        smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }


                    Debug.Log("E-posta başarıyla gönderildi!");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("E-posta gönderimi başarısız: " + ex.Message);
            }
            onetime = true;

        }

    }

    public void fillDatasA()
    {
        int currentIndex = GameManager.Instance.currentIndex;
            mainSceneText[currentIndex] = scenarioDatas.Scenarios[currentIndex].MainSceneText;
            actionSceneText[currentIndex] = scenarioDatas.Scenarios[currentIndex].ActionSceneText;
            actionSceneTrueLetter[currentIndex] = scenarioDatas.Scenarios[currentIndex].ActionSceneTrueLetter;
            checkUpQuestion[currentIndex] = scenarioDatas.Scenarios[currentIndex].CheckUpQuestion;
            checkUpAnswer[currentIndex] = scenarioDatas.Scenarios[currentIndex].CheckUpAnswer;
            type[currentIndex] = scenarioDatas.Scenarios[currentIndex].Type;

    }
    public void fillDatasB()
    {
        int currentIndex = GameManager.Instance.currentIndex;
        upOrDown[currentIndex] = scenarioDatas2.ImagePairs[currentIndex].answer;
    }

    public void DebugLog()
    {
        Debug.Log(upOrDown[GameManager.Instance.BgameCount - 1]+" " + chosenBalloon[GameManager.Instance.BgameCount-1]+" " + time_spent[GameManager.Instance.BgameCount-1]);
        


    }
}
