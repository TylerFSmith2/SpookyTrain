using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menus : MonoBehaviour
{
    public GameObject escMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //open Escape Menu
            escMenu.SetActive(!escMenu.activeInHierarchy);
        }
    }

    public void OpenMenu(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void CloseMenu(GameObject menu)
    {
        menu.SetActive(false);
    }

    public void ToggleMenu(GameObject menu)
    {
        menu.SetActive(!menu.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
