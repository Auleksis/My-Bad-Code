using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ActionCircle : MonoBehaviour
{
    private CircleCollider2D action_circle;

    private GameObject nearest_object;

    private List<GameObject> objects;

    public LevelLogic levelLogic;

    private void Start()
    {
        action_circle = GetComponent<CircleCollider2D>();
        objects = new List<GameObject>();
    }

    private void Update()
    {
        foreach(GameObject t in objects)
        {
            Vector3 d2 = t.transform.position - transform.position;
            float d2f = d2.sqrMagnitude;

            //if (nearest_trigger == null && t.canBeChangedDirectly && d2f <= action_circle.radius) nearest_trigger = t;
            if (nearest_object == null && d2f <= action_circle.radius) nearest_object = t;
            else if(nearest_object != null)
            {
                Vector3 d1 = nearest_object.transform.position - transform.position;

                float d1f = d1.sqrMagnitude;

                nearest_object = d1f < d2f ? nearest_object : t;

                if (d1f > action_circle.radius && d2f > action_circle.radius) nearest_object = null;
            }
        }
        
        levelLogic.HightLightObject(nearest_object);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Interactive") || objects.Contains(collision.gameObject))
            return;

        objects.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!objects.Contains(collision.gameObject))
            return;

        objects.Remove(collision.gameObject);
    }

    public GameObject GetTrigger()
    {
        return nearest_object;
    }
}
