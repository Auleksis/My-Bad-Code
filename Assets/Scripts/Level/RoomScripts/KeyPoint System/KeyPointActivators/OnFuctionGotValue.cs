using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnFuctionGotValue : KeyPointAbstractActivator
{
    [SerializeField] TFunction function;
    [SerializeField] int expectedValue = 0;
    public override bool CheckToActivate()
    {
        if(function.GetCurrentValue() == expectedValue)
        {
            function.BlockFunctionOutput();
            return true;
        }
        return false;
    }
}
