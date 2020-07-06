﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string name;
    public DialogueSentence[] sentences;

    public int GetSentenceCount(){
    	return sentences.Length;
    }
}
