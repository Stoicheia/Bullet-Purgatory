using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenUI : MonoBehaviour
{
    public void GoHome()
    {
    	GlobalManager.instance.GoToMenu(4);
    }
}
