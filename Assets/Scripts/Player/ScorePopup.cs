using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class ScorePopup : MonoBehaviour
{
    [SerializeField] private float timeToFade;
    private float fadeMultiplier;
    [SerializeField] private float distance;
    private TextMeshPro tmp;
    private Color color;

    private Vector3 originalPosition;

    private void OnEnable()
    {
        transform.SetParent(null, true);
        ResetPos();
        StartFade();
    }

    public void ResetPos()
    {
        transform.rotation = Quaternion.identity;
        originalPosition = transform.position;
        tmp = GetComponent<TextMeshPro>();
    }

    IEnumerator FadeSequence()
    {
        tmp.color = color;
        float startTime = Time.time;
        Vector3 targetPosition = new Vector3(originalPosition.x,originalPosition.y+distance,originalPosition.z);
        while (Time.time - startTime < timeToFade)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, (Time.time - startTime) / timeToFade);;
            tmp.color = new Color(color.r,color.g,color.b,1-(Time.time-startTime)/timeToFade);
            yield return null;
        }
        Destroy(this.gameObject);
    }

    public void StartFade()
    {
        StartCoroutine(FadeSequence());
    }

    public void SetColor(Color c)
    {
        color = c;
    }

    public void SetText(string s)
    {
        tmp.text = s;
    }

    public void MultiplyFade(float f)
    {
        fadeMultiplier = f;
        timeToFade *= fadeMultiplier;
    }
}
