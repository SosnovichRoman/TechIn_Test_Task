using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static UnityEvent OnBoardChanged = new UnityEvent();
    public static UnityEvent<bool> OnGameOver = new UnityEvent<bool>();
    public static UnityEvent OnGameStarted = new UnityEvent();

    public static void SendBoardChanged()
    {
        OnBoardChanged?.Invoke();
    }
    public static void SendGameOver(bool victory)
    {
        OnGameOver?.Invoke(victory);
    }
    public static void SendGameStarted()
    {
        OnGameStarted?.Invoke();
    }
}
