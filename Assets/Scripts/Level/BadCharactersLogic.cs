using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BadCharactersLogic : MonoBehaviour
{
    public Tilemap badLayer;

    public Grid grid;

    public TileBase badCharactor;

    private Vector3Int position;

    private void Start()
    {
        position = grid.WorldToCell(transform.position);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        /*badLayer.ClearAllTiles();

        Vector3Int position = grid.WorldToCell(collision.transform.position);

        collision.GetComponent<Renderer>().enabled = false;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                position = grid.WorldToCell(collision.transform.position) + new Vector3Int(i, j, 0);
                if (interactive.GetTile(position) != null)
                    badLayer.SetTile(position, badCharactor);
            }
        }*/
        if (collision.gameObject.tag.Equals("Programm") || collision.gameObject.tag.Equals("Player"))
        {
            badLayer.SetTile(position, badCharactor);
            collision.GetComponent<Renderer>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        /*badLayer.ClearAllTiles();
        collision.GetComponent<Renderer>().enabled = true;*/
        if (collision.gameObject.tag.Equals("Programm") || collision.gameObject.tag.Equals("Player"))
        {
            badLayer.SetTile(position, null);
            collision.GetComponent<Renderer>().enabled = true;
        }
    }
}
