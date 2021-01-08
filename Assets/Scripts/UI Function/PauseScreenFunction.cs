using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreenFunction : MonoBehaviour
{
    public void BackToMenu()
    {
    	GlobalManager.instance.GoToMenu(4);
    }
}
