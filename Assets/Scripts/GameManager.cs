using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] levelPrefab;
    //[SerializeField] private Transform spawnPoint;
    private int currentLevelIndex = 0;
    private GameObject currentLevel;

    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        currentLevel = Instantiate(levelPrefab[currentLevelIndex], Vector3.zero, Quaternion.identity);
    }

    public void RestartLevel()
    {
        currentLevel.SetActive(false);
        Destroy(currentLevel);
        currentLevel = Instantiate(levelPrefab[currentLevelIndex], Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        currentLevel.SetActive(false);
        Destroy(currentLevel);
        if(currentLevelIndex + 1 >= levelPrefab.Length) currentLevelIndex = 0;
        else currentLevelIndex = currentLevelIndex + 1;
        currentLevel = Instantiate(levelPrefab[currentLevelIndex], Vector3.zero, Quaternion.identity);
    }
}
