using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileCounter : MonoBehaviour
{
    private List<Tile> tiles = new List<Tile>();
    private int count;

    private void Awake()
    {
        EventManager.OnBoardChanged.AddListener(RefreshCount);
    }

    private void Start()
    {
        foreach (var tile in GetComponentsInChildren<Tile>())
        {
            if(tile != null) tiles.Add(tile);
        }
        count = tiles.Count;
    }

    private void RefreshCount()
    {
        count--;
        if (count <= 0) EventManager.SendGameOver(true);
    }
}
