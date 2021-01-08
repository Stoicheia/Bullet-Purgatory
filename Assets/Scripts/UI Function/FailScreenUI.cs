using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScreenUI : MonoBehaviour
{
    public void GoHome()
    {
    	GlobalManager.instance.GoToMenu(4);
    }

    public void Retry()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
