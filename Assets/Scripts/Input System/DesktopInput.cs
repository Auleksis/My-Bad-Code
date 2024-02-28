using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : MonoBehaviour
{
    public PlayerController player;

    public LevelLogic levelLogic;

    float x_input;
    float y_input;

    Vector3 velocity_vector;

    DesktopKeycodes keycodes;

    private void Start()
    {
        levelLogic.player = player;
        keycodes = GetComponent<DesktopKeycodes>();
    }

    void Update()
    {
        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.H))
        {
            player.SwapLook();
        }

        if (Input.GetKeyDown(keycodes.keys[ACTIONS.INTERACT]))
        {
            levelLogic.ChangeBit();
        }

        if (Input.GetKeyDown(keycodes.keys[ACTIONS.CHANGE_TO]))
        {
            levelLogic.ChangeState(false);
        }

        if (Input.GetKeyDown(keycodes.keys[ACTIONS.CHANGE_BACK]))
        {
            levelLogic.ChangeState(true);
        }

        velocity_vector = new Vector3(x_input, y_input, 0);
    }

    private void FixedUpdate()
    {
        player.SetMoveVector(velocity_vector);
    }
}
