using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject mainMenuPopup;

    private GameObject m_ActiveMenu;

    private void Start()
    {  
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        mainMenuPopup.SetActive(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(m_ActiveMenu == pauseMenu)
            {
                Time.timeScale = 1.0f;
                m_ActiveMenu.SetActive(false);
                m_ActiveMenu = null;
            }
            else if(m_ActiveMenu != pauseMenu && m_ActiveMenu)
            {
                m_ActiveMenu.SetActive(false);
                m_ActiveMenu = pauseMenu;
                m_ActiveMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 0.0f;
                m_ActiveMenu = pauseMenu; 
                m_ActiveMenu.SetActive(true);
                pauseMenu.transform.Find("Resume").GetComponent<Button>().Select();

            }
        }
    }

    public void OnResume()
    {
        Time.timeScale = 1.0f;
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(false);
        m_ActiveMenu = null;
    }
    public void OnOptions()
    {
        m_ActiveMenu.SetActive(false);
        m_ActiveMenu = optionsMenu;
        m_ActiveMenu.SetActive(true);
       optionsMenu.GetComponentInChildren<Button>().Select();


    }
    public void OnBack()
    {
        m_ActiveMenu.SetActive(false);
        m_ActiveMenu = pauseMenu;
        m_ActiveMenu.SetActive(true);

        pauseMenu.transform.Find("Options").GetComponent<Button>().Select();
    }

    public void OnMainMenu()
    {
        m_ActiveMenu.SetActive(false);
        m_ActiveMenu = mainMenuPopup;
        m_ActiveMenu.SetActive(true);
        mainMenuPopup.GetComponentInChildren<Button>().Select();
    }

    public void OnMainMenuConfirm()
    {
        pauseMenu.SetActive(false);
        mainMenuPopup.SetActive(false);
        Time.timeScale = 1.0f;
        m_ActiveMenu = null;
    }

}
