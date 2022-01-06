using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    private Rigidbody2D rb2D;
    public Animator animator;

    public float moveSpeed;
    public float jumpForce;
    private bool isJumping;
    private float moveHorizontal;
    private float moveVertical;
    public float gravityForce;
    bool facingLeft = true;




    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        moveSpeed = 1f;
        jumpForce = 35f;
        isJumping = false;
        gravityForce = 1.62f;

    }

    // Update is called once per frame
    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
    }

    private void FixedUpdate()
    {
        Physics2D.gravity = new Vector2(0, -gravityForce);

        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f)
        {
            rb2D.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }

        if (!isJumping && moveVertical > 0.1f)
        {
            rb2D.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
        }

        if (moveHorizontal > 0 && !facingLeft)
        {
            Flip();
        }
        if (moveHorizontal < 0 && facingLeft)
        {
            Flip();
        }

    }

    void Flip() // movement code
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingLeft = !facingLeft;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = false;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

}
