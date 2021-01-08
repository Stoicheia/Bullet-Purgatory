using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ButtonHoverEnlarge : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private RectTransform rectTransform;
    public float onHoverEnlargeBy = 1.1f;
    public float timeToEnlarge = 0.2f;
    private Vector3 originalScale;
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalScale = rectTransform.localScale;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        StartCoroutine(nameof(EnlargeSequence));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopCoroutine(nameof(EnlargeSequence));
        rectTransform.localScale = originalScale;
    }

    IEnumerator EnlargeSequence()
    {
        float startTime = Time.time;
        while (Time.time - startTime < timeToEnlarge)
        {
            float b = (Time.time - startTime) / timeToEnlarge;
            rectTransform.localScale = Vector3.Lerp(originalScale, originalScale*onHoverEnlargeBy, b);
            yield return null;
        }
    }

    public void OnValidate()
    {
        if (onHoverEnlargeBy < 1) onHoverEnlargeBy = 1;
    }
}
