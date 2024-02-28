using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveProgress : AfterEffect
{
    [SerializeField] SavePoint point;
    public override void ApplyEffect()
    {
        point.Save();
    }
}
