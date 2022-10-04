using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Tile : MonoBehaviour
{
    [SerializeField] public Collider col;
    [SerializeField] public string nameOfType;
    [SerializeField] private GameObject body;
    [SerializeField] private GameObject card;

    private bool canBeClicked;
    private bool isGameActive = true;

    private void Awake()
    {
        EventManager.OnBoardChanged.AddListener(DetectTilesAbove);
        EventManager.OnGameOver.AddListener((x) => isGameActive = false);
        EventManager.OnGameStarted.AddListener(() => isGameActive = true);
    }

    private void Start()
    {
        DetectTilesAbove();
    }

    private void OnMouseDown()
    {
        if (canBeClicked && isGameActive)
        {
            TileContainer.Instance.AddTile(this);
            col.enabled = false;
            EventManager.SendBoardChanged();
        }
    }

    private void OnMouseOver()
    {
        if (canBeClicked && isGameActive)
        {
            SetColor(new Color(0.85f, 0.85f, 0.85f));
        }
    }
    private void OnMouseExit()
    {
        if (canBeClicked && isGameActive)
        {
            SetColor(Color.white);
        }
    }

    private void DetectTilesAbove()
    {
        int tilesAboveCount = 0;
        Vector3 overlapOffset = new Vector3(0, col.bounds.size.y, 0);
        Collider[] colliders = Physics.OverlapBox(col.transform.position + overlapOffset, col.bounds.size / 2, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<Tile>() != null && collider.gameObject != gameObject)
            {
                tilesAboveCount++;
            }
        }
        if (tilesAboveCount > 0)
        {
            canBeClicked = false;
            SetColor(Color.gray);
        }
        if (tilesAboveCount == 0)
        {
            canBeClicked = true;
            SetColor(Color.white);
        }
    }

    private void SetColor(Color color)
    {
        body.GetComponent<Renderer>().material.color = color;
        card.GetComponent<Renderer>().material.color = color;
    }
}
