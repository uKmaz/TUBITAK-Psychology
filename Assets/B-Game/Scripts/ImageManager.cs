using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private ScenarioDatas2 scenarioDatas2;
    private void Start()
    {
            image1.sprite = scenarioDatas2.ImagePairs[GameManager.Instance.currentIndex].image1.sprite;
            image2.sprite = scenarioDatas2.ImagePairs[GameManager.Instance.currentIndex].image2.sprite;
    }
}
