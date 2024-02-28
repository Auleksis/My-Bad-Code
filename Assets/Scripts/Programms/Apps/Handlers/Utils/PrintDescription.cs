using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PrintDescription
{
    public AbstractProgramm expectedProgrammStateOwner; //������� ����������� ������ ����� ���������� ������ (����� id)
    public COMPARE_TYPE compareType;
    public OUTPUT_MODE outputMode;
    [Multiline] public string description;
}
