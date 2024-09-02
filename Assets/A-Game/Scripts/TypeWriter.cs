using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI textToDisplay;

    public float typingSpeed = 0.05f;

    private Coroutine typingCoroutine;
    bool typingDone = false;
    bool skipTyping = false;
    public void StartTyping(string text)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(text));
    }

    private IEnumerator TypeText(string fullText)
    {
        typingDone = false;
        textToDisplay.text = "";

        foreach (char letter in fullText.ToCharArray())
        {
            if (skipTyping)
            {
                textToDisplay.text = fullText;
                skipTyping = false;
                break;
            }

            textToDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingDone = true;
    }
    public bool IsDone()
    {
        return typingDone;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !typingDone)
        {
            skipTyping = true;
        }
    }
}
