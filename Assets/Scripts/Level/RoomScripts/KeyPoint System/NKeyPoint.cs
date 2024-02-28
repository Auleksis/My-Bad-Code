using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NKeyPoint : MonoBehaviour
{
    [SerializeField] Message message;

    [SerializeField] bool doesItFinishRoom = false;

    [SerializeField] BaseRoom room;

    private bool isAchieved = false;

    public void Achieve()
    {
        room.GetUIManager().PrintMessage(message);

        isAchieved = true;

        if (isAchieved && doesItFinishRoom) room.FinishRoom();
    }

    public virtual bool IsAchieved()
    {
        return isAchieved;
    }

    public void SetAchieved(bool value)
    {
        isAchieved = value;
    }
}
