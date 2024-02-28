using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class BaseRoom : MonoBehaviour
{
    [SerializeField] UIManager uIManager;

    [SerializeField] BaseRoom nextRoom;

    [SerializeField] SavePoint savePoint;

    [SerializeField] LevelLogic levelLogic;

    [SerializeField] NKeyPointsQueue keyPointsQueue;

    private bool isStarted = false;

    private bool isFinished = false;

    public void StartRoom()
    {
        isStarted = true;

        //keyPointsQueue.NextPoint();

        levelLogic.currentRoom = this;

        ActivateSavePoint();

        Init();
    }

    protected abstract void Init();

    public bool IsStarted()
    {
        return isStarted;
    }

    public UIManager GetUIManager()
    {
        return uIManager;
    }

    public void FinishRoom()
    {
        isFinished = true;
        nextRoom.StartRoom();
    }

    public void ActivateSavePoint()
    {
        if (savePoint != null)
            savePoint.Save();
        else
            throw new NullReferenceException("The save point is missing!");
    }

    public void LoadSavePoint()
    {
        if(savePoint != null)
            savePoint.SpawnPlayer();
    }
}
