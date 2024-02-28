using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PrintDescription
{
    public AbstractProgramm expectedProgrammStateOwner; //Объекты одинакового класса имеют одинаковые стейты (кроме id)
    public COMPARE_TYPE compareType;
    public OUTPUT_MODE outputMode;
    [Multiline] public string description;
}
