using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private bool facingRight;

    public Rigidbody2D rb;
    public Collider2D col;
    public SpriteRenderer guySprite;
    public SpriteRenderer shadowSprite;
    public SpriteRenderer eSprite;
    public Animator animator;
    public GameObject cleanWaterParticle;

    private void Start()
    {
    }

    void Update()
    {
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.SetLayerMask(LayerMask.GetMask("Interactable"));
        contactFilter.useLayerMask = true;
        contactFilter.useTriggers = true;
        Collider2D[] colliders = new Collider2D[10];

        Physics2D.OverlapCollider(col, contactFilter, colliders);

        if (colliders[0] != null)
        {
            eSprite.enabled = true;
            if (Input.GetKeyDown("e"))
            {
                colliders[0].gameObject.GetComponent<Interactable>().Interact();
                if (colliders[0].gameObject.TryGetComponent(out InteractableFilter filter) || colliders[0].gameObject.TryGetComponent(out InteractableRiver river))
                {
                    Instantiate(cleanWaterParticle, this.transform);
                }
            }
        }
        else
        {
            eSprite.enabled = false;
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

        if (horizontalForce != 0 || verticalForce != 0)
        {
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }

    private void FlipSprite()
    {
        guySprite.flipX = !guySprite.flipX;
        shadowSprite.flipX = !guySprite.flipX;
    }
}
