using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Trigger
{
    private BoxCollider2D boxCollider;

    protected override void Start()
    {
        base.Start();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public override void Disable()
    {
        base.Disable();
        boxCollider.enabled = true;
    }

    public override void Enable()
    {
        base.Enable();
        boxCollider.enabled = true;
    }

    public override void Use()
    {
        base.Use();
        boxCollider.enabled = !boxCollider.enabled;
    }
}
