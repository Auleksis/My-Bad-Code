using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPageCount : KeyPointAbstractActivator
{
    [SerializeField] int expectedValueGreaterThan;
    [SerializeField] BaseRoom room;
    public override bool CheckToActivate()
    {
        if (room.GetUIManager().GetConsole().GetPageCount() > expectedValueGreaterThan)
            return true;
        return false;
    }
}
