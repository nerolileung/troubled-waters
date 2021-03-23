using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool facingRight;

    Rigidbody2D rb;
    Collider2D col;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            ContactFilter2D contactFilter = new ContactFilter2D();
            contactFilter.SetLayerMask(LayerMask.GetMask("Interactable"));
            contactFilter.useLayerMask = true;
            contactFilter.useTriggers = true;
            Collider2D[] colliders = new Collider2D[10];

            Physics2D.OverlapCollider(col, contactFilter, colliders);

            if (colliders[0] != null)
            {
                Debug.Log(colliders[0].gameObject.GetComponent<Interactable>().Interact());
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontalForce = Input.GetAxis("Horizontal");
        float verticalForce = Input.GetAxis("Vertical");
        Vector2 combinedForce = new Vector2(horizontalForce, verticalForce);

        rb.AddForce(Vector2.ClampMagnitude(combinedForce, 1) * speed);
        if (horizontalForce > 0 && facingRight == true)
        {
            FlipSprite();
            facingRight = false;
        }
        if (horizontalForce < 0 && facingRight == false)
        {
            FlipSprite();
            facingRight = true;
        }
    }

    private void FlipSprite()
    {
        spriteRenderer.flipX = !spriteRenderer.flipX;
    }
}
