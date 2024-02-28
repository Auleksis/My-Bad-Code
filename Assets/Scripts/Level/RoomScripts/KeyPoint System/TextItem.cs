using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TextItem 
{
    [Multiline]
    public string text;
    public CONSOLE_MODE mode;
    public bool addToExistingText;
}
