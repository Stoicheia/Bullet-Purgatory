﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class LevelSelNavButton : MonoBehaviour
{
    public delegate void SubmenuNavAction(int subMenu);

    public static event SubmenuNavAction OnNav;

    private LevelSelectUI ui;
    [SerializeField] private int toMenu;

    private void Start()
    {
        ui = FindObjectOfType<LevelSelectUI>();
        if (ui == null)
        {
            Debug.LogError("I have no parent of type \"LevelSelectUI\"",  this);
        }
    }

    public void Click()
    {
        OnNav?.Invoke(toMenu);
    }
}
