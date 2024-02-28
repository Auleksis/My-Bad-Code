using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class App : Trigger
{
    [HideInInspector] public bool interacted = false;
    private void Start()
    {
        canBeChangedDirectly = true;
    }

    public override void Enable()
    {
        if (function != null && !isUsed)
        {
            isUsed = true;
            interactive.SetTile(position, active);
            function.SetBit(isUsed, this);
        }
    }

    public override void Disable()
    {
        base.Disable();
        interacted = false;
    }

    public override void Use()
    {
        Enable();
        interacted = true;
    }
}
