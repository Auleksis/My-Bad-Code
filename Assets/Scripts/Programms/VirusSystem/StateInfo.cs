using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Можно стать файлоприёмником? просто как идея
public enum STATE { TEXT_FILE, EXEC_FILE, VIRUS, DEFAULT}
public enum COMPARE_TYPE { CID, CTYPE}
public class StateInfo
{
    private STATE state;
    private Sprite copiedSprite;
    private int id;

    public StateInfo(STATE state, Sprite sprite, int id)
    {
        this.state = state;
        this.copiedSprite = sprite;
        this.id = id;
    }

    public STATE GetState()
    {
        return state;
    }

    public Sprite GetSprite()
    {
        return copiedSprite;
    }

    public bool EqualsToState(StateInfo other, COMPARE_TYPE compareType)
    {
        switch(compareType)
        {
            case COMPARE_TYPE.CID:
                return this.id == other.id;

            case COMPARE_TYPE.CTYPE:
                return this.state == other.state;
        }

        return false;
    }
}
