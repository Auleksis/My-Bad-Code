using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseOutput : AfterEffect
{
    [SerializeField] OUTPUT_MODE mode;
    [SerializeField] BaseRoom room;

    public override void ApplyEffect()
    {
        room.GetUIManager().HideOutput(mode);    
    }
}
