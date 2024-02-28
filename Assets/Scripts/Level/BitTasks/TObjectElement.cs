using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TObjectElement
{
    public Trigger [] tObjects;
    public int targetValueIncludesValue;
    public bool requiresOnlyDefinedValue = false;
    public Message message;
}
