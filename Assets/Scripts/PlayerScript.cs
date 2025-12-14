using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //Player
    public float speed = 5f;
    public float jumpForce = 6f;

    private Rigidbody2D rb;
    private Animator characterAnimator;
    public float bounceForce = 8f;

    public bool bFaceRight;
    private bool groundedAnimator = true;

    //Ground check
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.15f;
    [SerializeField] private LayerMask groundLayer;


    // Input
    private Vector2 moveInput;
    private bool jumpPressed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        Physics2D.queriesStartInColliders = false;
    }

    //Input calls

    //Move action
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    //Jump action
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            jumpPressed = true;
        }
    }

    //Fisicas

    void FixedUpdate()
    {
        float horizontalMovement = moveInput.x;
        characterAnimator.SetFloat("MovementSpeed", Mathf.Abs(horizontalMovement));

        bool isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        if (isGrounded && !groundedAnimator)
        {
            groundedAnimator = true;
            characterAnimator.SetBool("isGrounded", true);
        }
        else if (!isGrounded && groundedAnimator)
        {
            groundedAnimator = false;
            characterAnimator.SetBool("isGrounded", false);
        }

        if (jumpPressed && groundedAnimator)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            groundedAnimator = false;
            characterAnimator.SetBool("isGrounded", false);
            characterAnimator.SetTrigger("Jump");
        }
        jumpPressed = false;

        rb.linearVelocity = new Vector2(horizontalMovement * speed, rb.linearVelocity.y);

        if (horizontalMovement < 0 && !bFaceRight || horizontalMovement > 0 && bFaceRight)
        {
            Turn();
        }
    }


    void Turn()
    {
        transform.localScale = new Vector2(
            transform.localScale.x * -1,
            transform.localScale.y
        );

        bFaceRight = !bFaceRight;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //validacion de caer desde arriba
            if (rb.linearVelocity.y < 0 &&
                transform.position.y > collision.transform.position.y)
            {
                //Rebote
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, bounceForce);

                collision.gameObject
                    .GetComponent<EnemyAI>()
                    .Die();
            }
            else
            {
                    SceneManager.LoadScene("LoseScene");
            }
        }
    }
}

