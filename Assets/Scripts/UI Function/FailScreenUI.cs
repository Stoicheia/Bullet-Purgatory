using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailScreenUI : MonoBehaviour
{
    public void GoHome()
    {
    	SceneManager.LoadScene(0);
    }

    public void Retry()
    {
    	SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
