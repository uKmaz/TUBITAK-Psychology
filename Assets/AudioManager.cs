using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // GameManager'? sahne geçi?lerinde yok etme
        }
    }
    [Header("---------Audio Source-----------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("---------Audio Clip-----------")]
    public AudioClip background;
    public AudioClip buttonPress;
    public AudioClip CorrectClick;
    public AudioClip WrongClick;
    public AudioClip PlacingLetter;
    [Header("---------B GAME -----------")]
    public AudioClip BalloonPop;
    [HideInInspector] public bool didBaloonPop;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    private void Update()
    {
        if (GameManager.Instance.balloonPopped)
        {
            SFXSource.PlayOneShot(BalloonPop);
            GameManager.Instance.balloonPopped = false;
            GameManager.Instance.balloonPoppedToPass = true;

        }
    }


}
