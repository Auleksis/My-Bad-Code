using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTriggers : AfterEffect
{
    [SerializeField] Trigger[] triggers; 
    public override void ApplyEffect()
    {
        foreach (Trigger trigger in triggers)
            trigger.Disable();
    }
}
