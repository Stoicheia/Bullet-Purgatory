using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class RhythmicObject : MonoBehaviour 
{
	protected int currentStyle;
	public bool shootEnabled;

    protected virtual void Awake()
    {
    	currentStyle = 0;
        shootEnabled = true;
    }

    public abstract void Shoot();
    public virtual void Change(int i){ 
    	currentStyle = i;
    }
}

