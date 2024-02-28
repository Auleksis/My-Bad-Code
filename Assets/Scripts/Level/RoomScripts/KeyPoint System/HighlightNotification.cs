using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightNotification : AfterEffect
{
    [SerializeField] BaseRoom room;

    public override void ApplyEffect()
    {
        room.GetUIManager().HighlightNoticication();
    }
}
