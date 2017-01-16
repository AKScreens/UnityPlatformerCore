using UnityEngine;
using System.Collections;

public class PlayerController2d : MonoBehaviour
{

    public int moveSpeed;
    public int jumpHeight;
    public int dashForceHorizontal;
    public int dashForceVertical;
    public float dashDistance;
    private int faceRight;
    public float defaultGravity;
    private float dashStallTime;
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
        dashStallTime = 0f;
        //dashIntervalTime = 0.1f;
    }


    void Update()
    {
        Vector2 moveDir = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb2D.velocity.y);
        rb2D.velocity = moveDir;

        isGrounded = Physics2D.OverlapCircle(groundPoint.position, radius, groundMask);

        /*for (int i = 0; i < 1000000f; i++)
        {
            float j = Mathf.Exp(10000f);
        }*/

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
            dashStallTime = 0.2f;
            rb2D.gravityScale = 0;

            rb2D.velocity = Vector2.zero;
            Vector2 fwd = new Vector2(faceRight, 0);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd, dashDistance + 0.5f, breakableMask);
            hits = hit;
            Debug.DrawRay(transform.position, fwd, Color.green, 60f);
            if (hit)
            {
                dashUsed = false;
                hit.transform.gameObject.SetActive(false);
                float moveDist = Mathf.Sign(Input.GetAxis("Horizontal")) * Mathf.Min(hit.distance - 0.5f, Mathf.Abs(Input.GetAxisRaw("Horizontal") * dashDistance));
                rb2D.MovePosition(rb2D.position + new Vector2(moveDist, 0));
            }
            else
                rb2D.MovePosition(rb2D.position + new Vector2(Input.GetAxisRaw("Horizontal") * dashDistance, 0));
        }

        if (dashStallTime < 0f)
            rb2D.gravityScale = defaultGravity;
        else
            dashStallTime -= Time.deltaTime;

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