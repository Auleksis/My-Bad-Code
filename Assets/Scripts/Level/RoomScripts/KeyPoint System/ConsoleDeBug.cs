using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleDeBug : AfterEffect
{
    [SerializeField] BaseRoom room;
    public override void ApplyEffect()
    {
        room.GetUIManager().GetConsole().DeBug();
    }
}
