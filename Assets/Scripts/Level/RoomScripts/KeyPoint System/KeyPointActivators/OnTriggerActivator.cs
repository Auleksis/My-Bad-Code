using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTriggerActivator : KeyPointAbstractActivator
{
    [SerializeField] Trigger trigger;
    
    public override bool CheckToActivate()
    {
        if (trigger.IsUsed())
            return true;
        return false;
    }
}
