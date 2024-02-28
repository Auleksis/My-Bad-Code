using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProgramm : MonoBehaviour
{
    [SerializeField] protected StateInfo stateInfo;
    [SerializeField] STATE stateMode;
    private int id;

    [SerializeField] string programmName;

    static int ID = 0;

    protected virtual void Start()
    {
        id = ID++;
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        stateInfo = new StateInfo(stateMode, spriteRenderer.sprite, id);        
    }

    public StateInfo GetStateInfo() { return stateInfo; }

    public abstract StateInfo GetOriginalStateInfo();

    public abstract void RequestToStop();

    public string GetProgrammName()
    {
        return programmName;
    }
}
