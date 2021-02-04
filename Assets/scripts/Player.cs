using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
public class Player : MonoBehaviour
{
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(250f, 250f);
    Rigidbody2D rigidbody;
    Animator myanimator;
    CapsuleCollider2D mybodycollider;
    BoxCollider2D myfeet;
    float gravityScaleAtStart;
    bool isAlive = true;
    GameSession gameSession;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        myanimator = GetComponent<Animator>();
        mybodycollider = GetComponent<CapsuleCollider2D>();
        myfeet = GetComponent<BoxCollider2D>();
        gravityScaleAtStart = rigidbody.gravityScale;
        gameSession =  FindObjectOfType<GameSession>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive)
        {
            return;
        }
        run();
        FlipSprite();
        Jump();
        climbLadder();
        Die();
    }
    private void run()
    {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal"); // +1 or -1
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playerVelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        myanimator.SetBool("Running", playerHasHorizontalSpeed);
        
    }
    private void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }

    private void Jump()
    {
        if (!myfeet.IsTouchingLayers(LayerMask.GetMask("Ground")))
            return;

        if(CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);
            rigidbody.velocity += jumpVelocityToAdd;
        }
    }

    private void climbLadder()
    {
        if (!myfeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            rigidbody.gravityScale = gravityScaleAtStart;
            myanimator.SetBool("Climbing", false);
            return;

        }
        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical"); // +1 or -1
        Vector2 climbVelocity = new Vector2(rigidbody.velocity.x, controlThrow * climbSpeed);
        rigidbody.gravityScale = 0f;
        rigidbody.velocity = climbVelocity;

        bool playerHasVerticalSpeed = Mathf.Abs(rigidbody.velocity.y) > Mathf.Epsilon;
        myanimator.SetBool("Climbing", playerHasVerticalSpeed);

       
    }
    private void Die()
    {
        if(mybodycollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazard")))
        {
            myanimator.SetTrigger("Dieing");
            GetComponent<Rigidbody2D>().velocity = deathKick;
          
            isAlive = false;
            if(gameSession)
            gameSession.ProcessPlayerDeath();

        }
    }

    
}
