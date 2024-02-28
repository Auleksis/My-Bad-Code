using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class KeyPointAbstractActivator : MonoBehaviour
{
    private NKeyPoint pointToStart;

    [SerializeField] NKeyPoint[] mustBeAchievedBefore;
    [SerializeField] NKeyPoint[] ForbidGetAchieved;

    [SerializeField] protected bool reachievable = false;
    private bool toAchieve = false;

    private List<AfterEffect> afterEffects;
    private List<AfterEffect> beforeEffects;

    private void Start()
    {
        pointToStart = GetComponent<NKeyPoint>();

        afterEffects = new List<AfterEffect>();
        beforeEffects = new List<AfterEffect>();

        AfterEffect[] effects = GetComponents<AfterEffect>();

        foreach (AfterEffect e in effects)
        {
            if (e.isAfter)
                afterEffects.Add(e);
            else
                beforeEffects.Add(e);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if ((!pointToStart.IsAchieved() || reachievable))
        {
            foreach (NKeyPoint p in ForbidGetAchieved)
            {
                if (p.IsAchieved())
                {
                    pointToStart.SetAchieved(true);
                    reachievable = false;
                    return;
                }
            }

            foreach (NKeyPoint p in mustBeAchievedBefore)
            {
                if (!p.IsAchieved())
                    return;
            }

            if (CheckToActivate())
            {
                toAchieve = true;
            }
        }
    }

    private void LateUpdate()
    {
        if (!pointToStart.IsAchieved() && toAchieve)
        {
            OnBeforeAchievement();
            pointToStart.Achieve();
            OnAfterAchievement();
            return;
        }
    }

    public abstract bool CheckToActivate();

    public void OnBeforeAchievement()
    {
        foreach (AfterEffect e in beforeEffects)
            e.ApplyEffect();
    }

    public virtual void OnAfterAchievement()
    {
        foreach (AfterEffect e in afterEffects)
            e.ApplyEffect();
    }
}
