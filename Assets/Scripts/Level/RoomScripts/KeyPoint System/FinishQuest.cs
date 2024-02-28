using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishQuest : AfterEffect
{
    [SerializeField] BaseRoom room;

    public override void ApplyEffect()
    {
        room.GetUIManager().FinishQuest();
    }
}
