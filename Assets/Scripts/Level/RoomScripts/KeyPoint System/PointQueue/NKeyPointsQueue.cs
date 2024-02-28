using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NKeyPointsQueue : MonoBehaviour
{
    static NKeyPoint currentPoint;

    private Queue<NKeyPoint> queue;

    private void Start()
    {
        queue = new Queue<NKeyPoint>();
        for(int i = 0; i < transform.childCount; i++)
        {
            NKeyPoint point = transform.GetChild(i).GetComponent<NKeyPoint>();

            if (point != null)
            {
                queue.Enqueue(point);
            }
            else
                throw new NullReferenceException("All objects within NKeyPointsQueue have to have a NKeyPoint script!");
        }
    }

    public void StartQueue()
    {
        NextPoint();
    }

    public void NextPoint()
    {

    }

    public NKeyPoint GetCurrentPoint()
    {
        return currentPoint;
    }
}
