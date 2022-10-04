using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class TileContainer : MonoBehaviour
{
    [SerializeField] private float animationDuration;
    private Vector3 currentPos;
    [SerializeField] private int containerCapacity;

    public List<Tile> tiles = new List<Tile>();
    private Dictionary<string, int> tileNames = new Dictionary<string, int>();
    private int tileCount = 0;

    public static TileContainer Instance;
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentPos = transform.position;
        for (int i = 0; i < containerCapacity; i++)
        {
            tiles.Add(null);
        }
    }
    public void AddTile(Tile tile)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            if (tiles[i] == null)
            {
                tiles[i] = tile;
                currentPos = transform.position - new Vector3(tile.col.bounds.size.x + 0.1f, 0, 0) * i;
                tileCount++;
                break;
            }
        }

        tile.gameObject.transform.DOLocalJump(currentPos, 2f, 1, animationDuration).SetEase(Ease.OutQuad);

        if (tileNames.ContainsKey(tile.nameOfType)) tileNames[tile.nameOfType] += 1;
        else tileNames.Add(tile.nameOfType, 1);
        if (tileNames[tile.nameOfType] > 2) DeleteTriplet(tile.nameOfType);
        if (tileCount >= containerCapacity)
        {
            EventManager.SendGameOver(false);
        }
    }

    private void DeleteTriplet(string name)
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            if(tiles[i] != null)
            {
                if (tiles[i].nameOfType == name)
                {
                    tileNames.Remove(name);
                    StartCoroutine(DeleteTiles(tiles[i]));
                    tiles[i] = null;
                    tileCount--;
                }
            }
        }
    }

    private IEnumerator DeleteTiles(Tile tile)
    {
        yield return new WaitForSeconds(animationDuration);
        Destroy(tile.gameObject);
    }

}
