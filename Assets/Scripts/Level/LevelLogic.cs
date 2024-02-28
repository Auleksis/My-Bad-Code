using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelLogic : MonoBehaviour
{
    [HideInInspector] public PlayerController player;

    [SerializeField] Tilemap interactiveLayer;
    [SerializeField] Grid grid;

    [SerializeField] TileBase activeBit;
    [SerializeField] TileBase inactiveBit;

    [SerializeField] Tilemap highlightLayer;

    [SerializeField] TileBase highlightTile;

    [SerializeField] BaseRoom startRoom;

    [HideInInspector] public BaseRoom currentRoom;

    private void Start()
    {
        startRoom.StartRoom();
        startRoom.LoadSavePoint();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            currentRoom.LoadSavePoint();
        }
    }

    //����� ������������ ������ ��������, � ����� ������� ������� ��� ������ �������, ������� ����� ����������� ��� ����� � ���. � ���� ������� ����� ��������� ������ ��������, ��������� �� (-1) � ���������.
    //������ ����� � ������� ������������ �������� �������� ������ �������� ������� �������

    public void ChangeBit()
    {
        //������� ������� �������� ��� ������
        /*Vector3 position = player.transform.position + new Vector3(0.64f, 0, 0);
        Vector3Int changedTilePos = grid.WorldToCell(position);
        TileBase currentBitState = interactiveLayer.GetTile(changedTilePos);*/

        //������ ����� ������ ��������� ��� � ���� ������� � ������ ���
        GameObject interactive = player.GetNearestObject();
        if (interactive != null)
        {
            Trigger expectedTrigger = interactive.GetComponent<Trigger>();

            if (expectedTrigger != null)
            {
                UseBitsLogic(expectedTrigger);
                return;
            }

            AppWrapper expectedApp = interactive.GetComponent<AppWrapper>();

            if(expectedApp != null)
            {
                expectedApp.CallHandlers(player.GetComponent<Virus>());
            }
        }
    }

    public void HightLightObject(GameObject trigger)
    {
        highlightLayer.ClearAllTiles();
        if(trigger != null)
            highlightLayer.SetTile(grid.WorldToCell(trigger.gameObject.transform.position), highlightTile);
    }

    private void UseBitsLogic(Trigger trigger)
    {
        if (trigger == null)
            return;

        if (trigger.canBeChangedDirectly)
            trigger.Use();
    }

    private void UseBitsLogic(Vector3 position)
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(position, LayerMask.GetMask("BitsLogic"));

        foreach (Collider2D c in colliders)
        {
            if (c.gameObject.tag.Equals("Bits"))
            {
                Trigger t = c.gameObject.GetComponent<Trigger>();
                if (t.canBeChangedDirectly)
                    t.Use();
                break;
            }
        }
    }

    public void ChangeState(bool back) 
    {
        if (!back)
        {
            GameObject interactive = player.GetNearestObject();
            Virus stepan = player.GetComponent<Virus>();
            if (interactive != null)
            {
                AppWrapper change_to = player.GetNearestObject().GetComponent<AppWrapper>();
                if (change_to != null)
                {
                    stepan.SetNewState(change_to.GetStateInfo());
                }                
                else
                    stepan.ChangeStateToSaved();
            }
            else
                stepan.ChangeStateToSaved();
               
        }

        if (back)
        {
            Virus stepan = player.GetComponent<Virus>();
            stepan.ChangeStateToOriginal();
        }
    }
}
