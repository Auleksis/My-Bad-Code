using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractHandler : MonoBehaviour
{
    public abstract void Handle(AbstractProgramm callingProgramm);
}
