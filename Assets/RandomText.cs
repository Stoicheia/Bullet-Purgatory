using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(TextMeshProUGUI))]
public class RandomText : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    public List<string> titles;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        tmp.text = titles[Random.Range(0, titles.Count)];
    }
}
