using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public Color jumpColor = Color.cyan;

    [Header("Mario Jump Settings")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Color originalColor;
    private bool isGrounded = false;
    private bool isDead = false;
    private bool isJumpHeld = false;
    private float moveInput;
    
    [Header("Audio")]
    public AudioClip jumpSound;
    private AudioSource audioSource;
    
    private PlayerInput playerInput;
    private InputAction jumpAction;
    private float lastJumpTime;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        originalColor = spriteRenderer.color;
        
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            jumpAction = playerInput.actions["Jump"];
        }
    }

    void OnMove(InputValue value)
    {
        if (isDead) return;
        Vector2 inputVec = value.Get<Vector2>();
        moveInput = inputVec.x;
    }

    void OnJump(InputValue value)
    {
        if (isDead) return;
        
        if (jumpAction == null)
            isJumpHeld = value.isPressed;
    }

    void Update()
    {
        if (jumpAction != null)
        {
            isJumpHeld = jumpAction.IsPressed();
        }

        if (isDead) return;

        if (animator != null)
        {
            animator.SetFloat("Speed", Mathf.Abs(moveInput));
            animator.SetBool("IsJumping", !isGrounded);
        }

        if (moveInput > 0)
            spriteRenderer.flipX = false;
        else if (moveInput < 0)
            spriteRenderer.flipX = true;

        if (isJumpHeld && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (isDead)
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        
        CheckGrounded();
        
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !isJumpHeld)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }

    void Jump()
    {
        if (Time.time - lastJumpTime < 0.1f) return;
        lastJumpTime = Time.time;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        isGrounded = false;

        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }

        spriteRenderer.color = jumpColor;
    }

    private void CheckGrounded()
    {
        if (Time.time - lastJumpTime < 0.1f)
        {
            isGrounded = false;
            return;
        }

        bool wasGrounded = isGrounded;
        isGrounded = false;

        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int count = rb.GetContacts(contacts);

        for (int i = 0; i < count; i++)
        {
            if (contacts[i].normal.y > 0.5f)
            {
                isGrounded = true;
                if (!wasGrounded)
                {
                    spriteRenderer.color = originalColor;
                }
                break;
            }
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        moveInput = 0f;
        rb.linearVelocity = Vector2.zero;

        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("IsJumping", false);
            animator.SetTrigger("Die");
        }

        if (GameManager.Instance != null)
        {
            GameManager.Instance.PlayerDied();
        }
    }
}
