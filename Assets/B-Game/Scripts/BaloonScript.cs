
using UnityEngine;

public class BaloonScript : MonoBehaviour
{
    [HideInInspector] private DataCollector collector;
    [HideInInspector] private GameSceneManager gameSceneManager;
    [HideInInspector] private AudioManager audioManager;
    private void Start()
    {
        audioManager=FindAnyObjectByType<AudioManager>();
        collector=FindAnyObjectByType<DataCollector>();
        gameSceneManager=FindAnyObjectByType<GameSceneManager>();
        GameManager.Instance.balloonPopped = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Arrow"))
        {
            audioManager.balloonPop();
            if (gameObject.name[0].ToString() == "Y"|| gameObject.name[0].ToString() == "T")
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.CorrectClick);
                GameManager.Instance.score += 10;
                collector.chosenBalloon[GameManager.Instance.currentIndex] = collector.upOrDown[GameManager.Instance.currentIndex];
                
            }
            else
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.WrongClick);
                GameManager.Instance.score -= 10;
                collector.chosenBalloon[GameManager.Instance.currentIndex] = !collector.upOrDown[GameManager.Instance.currentIndex];
            }
            GameManager.Instance.balloonPopped = true;
            gameSceneManager.popped = true;
            Destroy(gameObject);
        }
    }


}
