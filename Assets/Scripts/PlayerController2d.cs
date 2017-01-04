using UnityEngine;
using System.Collections;

public class PlayerController2d : MonoBehaviour
{

    public int moveSpeed;
    public int jumpHeight;
    public int dashForceHorizontal;
    public int dashForceVertical;
    public int dashDistance;
    private int faceRight;
    public float defaultGravity;
    private float dashStallTime;
    private float dashFallTime;
    //private float dashIntervalTime;
    //private float timeSinceDash;

    public Transform groundPoint;
    public float radius;
    public LayerMask groundMask;
    public LayerMask breakableMask;
    public RaycastHit2D hits;
    
    public bool isGrounded;
    public bool dashUsed = false;
    public bool hasKey;
    // public int dashX;
    public Vector2 savedVelocity;

    Rigidbody2D rb2D;
    BoxCollider2D bx2D;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        //bx2D = GetComponent<BoxCollider2D>();
        dashStallTime = 0.2f;
        dashFallTime = 0.2f;
        //dashIntervalTime = 0.1f;
    }


    void Update()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb2D.velocity.y);
        rb2D.velocity = moveDir;

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, groundMask);


        //horizontal movement flip, recognizes what direction the player
        //is inputting and scales the sprite to face either left or right
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            transform.localScale = new Vector3(1, 1, 1);
            faceRight = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            faceRight = -1;
        }


        //vertical jump force, adds vertical force to player when space bar is pressed
        //else statement checks to see if player has used double jump, then disables after use
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb2D.AddForce(new Vector2(0, jumpHeight));
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isGrounded == false && dashUsed == false)
        {
            dashUsed = true;
            rb2D.isKinematic = true;
            rb2D.isKinematic = false;
            rb2D.gravityScale = 0;
            Vector2 fwd = new Vector2(faceRight, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd, dashDistance, breakableMask);
            hits = hit;
            Debug.DrawRay(transform.position, fwd, Color.green, 60f);
            if (hit)
            {
                dashUsed = false;
                hit.transform.gameObject.SetActive(false);
            }
            rb2D.AddForce(new Vector2(Input.GetAxisRaw("Horizontal") * dashForceHorizontal, 0));
        }
        else if (dashUsed == false && hits)
        {
            dashFallTime -= Time.deltaTime;
            if (dashFallTime < 0)
            {
                rb2D.gravityScale = defaultGravity;
                dashFallTime = 0.2f;
            }
        }
   


        if (dashUsed == true)
        {
            dashStallTime -= Time.deltaTime;
        }
        if (dashStallTime < 0)
        {
            rb2D.gravityScale = defaultGravity;
            dashStallTime = 0.2f;
        }


        //reenables double jump after coming in contact with the ground
        if (isGrounded)
        {
            dashUsed = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Breakable Wall"))
        {
            other.gameObject.SetActive(false);
            dashUsed = false;
        }
        if (other.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            hasKey = true;
        }
        if (other.CompareTag("Door") && hasKey == true)
        {
            other.gameObject.SetActive(false);
            hasKey = false;
        }
    }
}