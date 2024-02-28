using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Virus : AbstractProgramm
{
    private StateInfo lastGotStateInfo;
    private StateInfo stepanStateInfo;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();

        stepanStateInfo = this.stateInfo;

        animator = GetComponent<Animator>();
    }

    public void SetNewState(StateInfo stateInfo)
    {
        lastGotStateInfo = stateInfo;
        ChangeStateToSaved();
    }

    public void ChangeStateToOriginal()
    {
        this.stateInfo = stepanStateInfo;
        ApplyChanges();
    }

    public void ChangeStateToSaved()
    {
        if (lastGotStateInfo == null)
            return;
        this.stateInfo = lastGotStateInfo;
        ApplyChanges();
    }

    void ApplyChanges()
    {
        animator.enabled = false;
        Debug.Log(this.stateInfo.GetState().ToString());
        spriteRenderer.sprite = this.stateInfo.GetSprite();

        //ƒополнительна€ логика, если вернулись в состо€ние игрока
        //...
    }

    public override StateInfo GetOriginalStateInfo()
    {
        return stepanStateInfo;
    }

    public override void RequestToStop()
    {
        //”ведомление игроку остановитьс€
    }
}
