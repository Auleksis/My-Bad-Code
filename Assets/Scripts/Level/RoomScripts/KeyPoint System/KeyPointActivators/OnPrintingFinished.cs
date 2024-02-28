using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPrintingFinished : KeyPointAbstractActivator
{
    [SerializeField] OUTPUT_MODE outputMode;
    [SerializeField] BaseRoom room;

    public override bool CheckToActivate()
    {
        return !room.GetUIManager().IsOutputPrinting(outputMode);
    }
}
