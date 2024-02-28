using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    [SerializeField] Sprite unsaved;
    [SerializeField] Sprite saved;
    [SerializeField] GameObject floppy;

    [SerializeField] PlayerController player;

    public void Save()
    {
        floppy.GetComponent<SpriteRenderer>().sprite = saved;
    }

    public void SpawnPlayer()
    {
        player.transform.position = transform.position;
    }
}
