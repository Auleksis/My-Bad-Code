using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitsLogic : MonoBehaviour
{
    [SerializeField] TFunction function;

    private bool isUsedDirectly = false; //�����, ����� ����� ���� �������� ��������

    public void Use()
    {
        isUsedDirectly = !isUsedDirectly;
        //function.SetBit(isUsedDirectly, this);
    }
}
