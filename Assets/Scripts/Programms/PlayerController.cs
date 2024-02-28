using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;

    private SpriteRenderer renderer;

    [SerializeField] float max_velocity = 1f;

    [SerializeField] public float action_radius = 1f;

    [SerializeField] Sprite stifSprite;

    public Sprite anotherSprite;

    bool realLook = true;

    public ActionCircle actionCircle;

    private bool canMove = true;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMoveVector(Vector3 velocity_vector)
    {
        if (canMove)
        {
            Vector3 normalized = velocity_vector.normalized;
            if (normalized.sqrMagnitude > 0)
                rigidbody.velocity = normalized * max_velocity;
            else
                rigidbody.velocity = new Vector3(0, 0, 0);
        }
        else
            rigidbody.velocity = new Vector3(0, 0, 0);
    }

    public void SwapLook()
    {
        realLook = !realLook;

        if (realLook) renderer.sprite = stifSprite;
        else renderer.sprite = anotherSprite;
    }
        
    public void BlockMovement()
    {
        canMove = false;
    }

    public void UnblockMovement()
    {
        canMove = true;
    }

    public GameObject GetNearestObject()
    {
        return actionCircle.GetTrigger();
    }
}
