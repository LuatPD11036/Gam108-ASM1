using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public partial class PlayerMovement : MonoBehaviour
{
     private float dirX = 0f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] private LayerMask jumpableGround;
    private SpriteRenderer sprite;[SerializeField]

    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float gravityScaleAtStart;

    bool isAlive = true;

    private enum MovementState { idle, running, jumping, falling }

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        sprite = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        myRigidbody.velocity = new Vector2(dirX * runSpeed, myRigidbody.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
        }
        UpdateAnimationState();

    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (myRigidbody.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (myRigidbody.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        myAnimator.SetInteger("state", (int)state);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(myFeetCollider.bounds.center, myFeetCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
