using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TObject : MonoBehaviour, IInteractive
{
    public abstract void Enable();
    public abstract void Disable();
}
