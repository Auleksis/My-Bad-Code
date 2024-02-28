using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ConsoleTextItem
{
    public TextItem[] textItems;

    [Header("Set only if you aren't going to show any text but want to clear console")]
    public bool clearConsole;
}
