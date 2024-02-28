using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWindowClosed : KeyPointAbstractActivator
{
    [SerializeField] BaseRoom room;
    public override bool CheckToActivate()
    {
        return !room.GetUIManager().IsOutputActive(OUTPUT_MODE.WINDOW);
    }
}
