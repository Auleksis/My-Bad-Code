using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFunctionOutput : AfterEffect
{
    [SerializeField] TFunction function;
    [SerializeField] bool isOutputBlocked;
    public override void ApplyEffect()
    {
        function.DoPrintOutput(!isOutputBlocked);
    }
}
