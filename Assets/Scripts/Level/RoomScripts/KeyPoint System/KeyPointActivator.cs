using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ACTIVATOR_TYPE {ON_START_ROOM ,ON_PLAYER_IS_HERE, ON_TRIGGER_ACTIVATED, ON_PLAYER_CONTINUE_DIALOG, ON_KEY_PRESSED }
public class KeyPointActivator : MonoBehaviour
{
    [SerializeField] ACTIVATOR_TYPE type;

    private BoxCollider colliderToCheckPlayer; //this should be a trigger!

    [SerializeField] Trigger trigger;

    [SerializeField] BaseRoom waitingRoom; //Room that waits to be startded. When this room has been started then the activator activate a point

    [SerializeField] KeyPoint pointToStart;

    [SerializeField] KeyCode key;

    [SerializeField] KeyPoint[] mustBeAchievedBefore; 

    private bool toStartPlayerIsHere = false;

    void Start()
    {
        colliderToCheckPlayer = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pointToStart.IsAchieved())
        {
            foreach(KeyPoint p in mustBeAchievedBefore)
            {
                if (!p.IsAchieved())
                    return;
            }

            switch (type)
            {
                case ACTIVATOR_TYPE.ON_START_ROOM:
                    if (waitingRoom.IsStarted())
                        pointToStart.Achieve();
                    break;
                case ACTIVATOR_TYPE.ON_PLAYER_IS_HERE:
                    if (toStartPlayerIsHere)
                        pointToStart.Achieve();
                    break;
                case ACTIVATOR_TYPE.ON_TRIGGER_ACTIVATED:
                    if (trigger.IsUsed())
                        pointToStart.Achieve();
                    break;
                case ACTIVATOR_TYPE.ON_KEY_PRESSED:
                    if (Input.GetKeyDown(key))
                        pointToStart.Achieve();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        toStartPlayerIsHere = true;
    }
}
