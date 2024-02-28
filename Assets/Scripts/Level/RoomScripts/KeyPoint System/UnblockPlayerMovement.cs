using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockPlayerMovement : AfterEffect
{
    PlayerController player;

    private void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>();
    }

    public override void ApplyEffect()
    {
        player.UnblockMovement();
    }
}
