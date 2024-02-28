using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugConsoleEffect : AfterEffect
{
    [SerializeField] BaseRoom room;
    public override void ApplyEffect()
    {
        int pageCount = room.GetUIManager().GetConsole().GetPageCount();
        if (pageCount > 1)
        {
            room.GetUIManager().GetConsole().Bug();
        }
    }
}
