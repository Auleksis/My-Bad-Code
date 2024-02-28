using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : Trigger
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isUsed)
            Enable();
    }
}
