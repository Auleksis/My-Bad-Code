using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFunction : AfterEffect
{
    [SerializeField] TFunction function;
    public override void ApplyEffect()
    {
        function.BlockEvaluating();
    }
}
