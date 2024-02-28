using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Trigger : TObject
{
    [SerializeField] protected Grid grid;
    [SerializeField] protected Tilemap interactive;

    [SerializeField] protected TileBase active;
    [SerializeField] protected TileBase inactive;

    [SerializeField] protected TFunction function;

    [SerializeField] protected TFunction[] additionalFunctions;

    protected Vector3Int position;

    protected bool isUsed = false;

    public bool canBeChangedDirectly = true;

    protected virtual void Start()
    {
        if(grid != null)
            position = grid.WorldToCell(transform.position);
    }

    public override void Enable()
    {
        if(grid != null)
            interactive.SetTile(position, active);
        isUsed = true;
        if(function != null)
            function.SetBit(isUsed, this);

        foreach (TFunction f in additionalFunctions)
            f.SetBit(isUsed, this);
    }

    public override void Disable()
    {
        if(grid != null)
            interactive.SetTile(position, inactive);
        isUsed = false;
        if(function != null)
            function.SetBit(isUsed, this);

        foreach (TFunction f in additionalFunctions)
            f.SetBit(isUsed, this);
    }

    public virtual void Use()
    {
        if(grid != null)
            interactive.SetTile(position, isUsed ? inactive : active);
        isUsed = !isUsed;
        if (function != null)
            function.SetBit(isUsed, this);

        foreach (TFunction f in additionalFunctions)
            f.SetBit(isUsed, this);
    }

    public virtual bool IsUsed()
    {
        return isUsed;
    }
}
