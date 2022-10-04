using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUIHandler : MonoBehaviour
{
    [SerializeField] private GameObject victoryMenu;
    [SerializeField] private GameObject defeatMenu;

    private void Awake()
    {
        EventManager.OnGameOver.AddListener(EnableMenu);
    }

    public void Continue()
    {
        GameManager.Instance.NextLevel();
        victoryMenu.SetActive(false);
    }
    
    public void Retry()
    {
        GameManager.Instance.RestartLevel();
        defeatMenu.SetActive(false);
    }

    private void EnableMenu(bool victory)
    {
        victoryMenu.SetActive(victory);
        defeatMenu.SetActive(!victory);
    }
}
