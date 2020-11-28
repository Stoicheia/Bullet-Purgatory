using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public delegate void MenuChangeAction(GameObject menu);
    public static event MenuChangeAction OnMenuChange;

	static GameObject activeMenu;
    public static GameObject ActiveMenu { get => activeMenu; }
	public GameObject startingMenu;

    private void Awake()
    {
        GoToMenu(startingMenu);
    }
    void Start()
    {
        Bullet.SetStage(GameObject.FindGameObjectWithTag("Stage").GetComponent<MeshCollider>());
        Bullet.SetPooler(FindObjectOfType<ObjectPooler>());
    }

    void Update()
    {
        
    }

    public void GoToMenu(GameObject menu)
    {
    	if(activeMenu!=null)
    		activeMenu.SetActive(false);
    	activeMenu = menu;
        menu.SetActive(true);
        OnMenuChange(activeMenu);
    }
}
