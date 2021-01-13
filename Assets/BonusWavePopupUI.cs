using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BonusWavePopupUI : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private GameObject tmpObject;

    public float flashInterval = 0.5f;
    public int numberOfFlashes = 4;
    
    void Start()
    {
        tmp = GetComponentsInChildren<TextMeshProUGUI>()[0];
        tmpObject = tmp.gameObject;
        tmpObject.SetActive(false);
    }

    private void OnEnable()
    {
        WaveSpawner.NormalWavesCompleted += StartPopup;
    }

    private void OnDisable()
    {
        WaveSpawner.NormalWavesCompleted -= StartPopup;
    }

    void Update()
    {
        
    }

    void StartPopup()
    {
        StartCoroutine(PopupFlashSequence());
    }

    IEnumerator PopupFlashSequence()
    {
        for (int i = 0; i < numberOfFlashes; i++)
        {
            tmpObject.SetActive(true);
            yield return new WaitForSeconds(flashInterval);
            tmpObject.SetActive(false);
            yield return new WaitForSeconds(flashInterval);
        }
        tmpObject.SetActive(false);
    }
}
