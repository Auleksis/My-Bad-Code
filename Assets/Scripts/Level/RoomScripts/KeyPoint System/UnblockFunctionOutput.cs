using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnblockFunctionOutput : AfterEffect
{
    [SerializeField] TFunction function;
    public override void ApplyEffect()
    {
        function.UnblockFunctionOutput();
    }
}
