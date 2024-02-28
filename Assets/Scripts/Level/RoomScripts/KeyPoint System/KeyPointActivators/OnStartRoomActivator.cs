using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStartRoomActivator : KeyPointAbstractActivator
{
    [SerializeField] BaseRoom activatorRoom;

    public override bool CheckToActivate()
    {
        if (activatorRoom.IsStarted())
            return true;
        return false;
    }
}
