using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnConsoleModeChosen : KeyPointAbstractActivator
{
    [SerializeField] OUTPUT_MODE mode;
    [SerializeField] BaseRoom room;
    public override bool CheckToActivate()
    {
        return room.GetUIManager().GetConsoleOutputMode() == mode;
    }
}
