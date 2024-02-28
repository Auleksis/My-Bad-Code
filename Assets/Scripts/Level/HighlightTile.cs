using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightTile : AfterEffect
{
    [SerializeField] TileBase highlightTile;
    [SerializeField] Color highlightColor;
    [SerializeField] Grid grid;
    [SerializeField] Tilemap hightlightLayer;

    [SerializeField] int flashCount = 10;
    [SerializeField] float flastTimeSeconds = 0.3f;

    [SerializeField] Trigger [] triggers;

    public override void ApplyEffect()
    {
        StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        for (int i = 0; i < flashCount; i++)
        {
            foreach (Trigger t in triggers)
            {
                Vector3Int position = grid.WorldToCell(t.gameObject.transform.position);

                hightlightLayer.SetTile(position, highlightTile);
                hightlightLayer.SetColor(position, highlightColor);
            }

            yield return new WaitForSeconds(flastTimeSeconds);

            foreach (Trigger t in triggers)
            {
                Vector3Int position = grid.WorldToCell(t.gameObject.transform.position);
                hightlightLayer.SetTile(position, null);
            }

            yield return new WaitForSeconds(flastTimeSeconds);
        }
    }
}
