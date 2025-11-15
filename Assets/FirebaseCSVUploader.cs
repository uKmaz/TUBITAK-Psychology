using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.IO;

public class FirebaseCSVUploader : MonoBehaviour
{
    private string uploadUrl = "https://us-central1-dusuncelerinihafiflet-1c13d.cloudfunctions.net/uploadCSV";

    public void UploadCSV(string filePath)
    {
        StartCoroutine(SendCSV(filePath));
    }

    private IEnumerator SendCSV(string filePath)
    {
        byte[] fileBytes = File.ReadAllBytes(filePath);
        string base64 = System.Convert.ToBase64String(fileBytes);

        UploadPayload payload = new UploadPayload
        {
            fileName = Path.GetFileName(filePath),
            fileContent = base64
        };

        string json = JsonUtility.ToJson(payload);

        UnityWebRequest req = new UnityWebRequest(uploadUrl, "POST");
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(json);
        req.uploadHandler = new UploadHandlerRaw(jsonBytes);
        req.downloadHandler = new DownloadHandlerBuffer();
        req.SetRequestHeader("Content-Type", "application/json");

        yield return req.SendWebRequest();

        if (req.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Cloud Function upload OK: " + req.downloadHandler.text);

            // İstersen dosyayı sil
            File.Delete(filePath);
        }
        else
        {
            Debug.LogError("Upload ERROR: " + req.error);
            Debug.LogError(req.downloadHandler.text);
        }
    }

    [System.Serializable]
    public class UploadPayload
    {
        public string fileName;
        public string fileContent; // Base64
    }
}
