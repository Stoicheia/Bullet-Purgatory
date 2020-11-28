using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    static SaveData _current;
    public static SaveData current { 
        get
        {
            if (_current == null) _current = new SaveData();
            return _current;
        }
        set
        {
            _current = value;
        }
    }

    public PlayerData playerData;
}
