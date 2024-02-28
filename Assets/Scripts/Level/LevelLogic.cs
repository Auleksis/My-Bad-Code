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

    //Чтобы обрабатывать логику тригеров, я думаю сделать функцию для каждой комнаты, которая будет подгужаться при входе в них. В этой функции будет разрешено только сложение, умножение на (-1) и вычитание.
    //Игроку нужно с помощью переключения тригеров получить нужное значение целевой функции

    public void ChangeBit()
    {
        //сначала пробуем поменять бит справа
        /*Vector3 position = player.transform.position + new Vector3(0.64f, 0, 0);
        Vector3Int changedTilePos = grid.WorldToCell(position);
        TileBase currentBitState = interactiveLayer.GetTile(changedTilePos);*/

        //Теперь будем искать ближайший бит в зоне доступа и менять его
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
