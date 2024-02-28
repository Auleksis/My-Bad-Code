using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AfterEffect : MonoBehaviour
{
    public bool isAfter = true;
    public abstract void ApplyEffect();
}
