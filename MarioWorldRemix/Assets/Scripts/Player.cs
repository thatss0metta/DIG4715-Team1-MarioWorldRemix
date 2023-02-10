using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Vertical Movement")]
    public float jumpSpeed = 15f;
    public float jumpDelay = 0.25f;
    private float jumpTimer;

    [Header("Components")]
    public Rigidbody2D rb;
    public Animator animator;
    public LayerMask groundLayer;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;
    public float gravity = 1;
    public float fallMultiplier = 5f;

    [Header("Collision")]
    public bool onGround = false;
    public float groundLength = 3f;
    public Vector3 colliderOffset;
    public bool StarPowerup = false;

    [Header("Canvas")]
    public GameObject loseScreen;
    AudioSource audioSource;
    public AudioSource BackgroundMusic;
    public AudioSource BackgroundMusic2;
    public AudioSource LoseAudio;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI CoinTextFront;
    int coinCount = 0;
    public AudioClip jumpClip;
    public AudioClip HurtClip;
    public ParticleSystem dust;
    public AudioClip powerUpClip;
    public bool MoshiMode = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (MoshiMode)
        {
            animator.SetBool("moshi", true);
            jumpSpeed = 20;
        }
        else
        {
            animator.SetBool("moshi", false);
            jumpSpeed = 15;
        }

        onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, groundLength, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, groundLength, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            jumpTimer = Time.time + jumpDelay;
        }

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate()
    {
        moveCharacter(direction.x);
        if (jumpTimer > Time.time && onGround)
        {
            Jump();
        }
        modifyPhysics();
    }

    void moveCharacter(float horizontal)
    {
        rb.AddForce(Vector2.right * horizontal * moveSpeed);

        if ((horizontal > 0 && !facingRight) || (horizontal < 0 && facingRight))
        {
            Flip();
        }

        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
        animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));
        //        animator.SetFloat("vertical", rb.velocity.y);
    }

    void Jump()
    {
        PlaySound(jumpClip);
        CreateDust();
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
        jumpTimer = 0;
    }

    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
        if (onGround)
        {
            if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
            {
                rb.drag = linearDrag;
            }
            else
            {
                rb.drag = 0f;
            }
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = gravity;
            rb.drag = linearDrag * 0.15f;

            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravity * fallMultiplier;
            }
            else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                rb.gravityScale = gravity * (fallMultiplier / 2);
            }
        }

    }

    void Flip()
    {
        if (onGround)
        {
            CreateDust();
        }
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * groundLength);
        Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * groundLength);
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Death()
    {
        BackgroundMusic.Stop();
        BackgroundMusic2.Stop();
        LoseAudio.Play();
        CoinText.gameObject.SetActive(false);
        CoinTextFront.gameObject.SetActive(false);
        loseScreen.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    void SetCoinText()
    {
        CoinTextFront.text = "Coins X " + coinCount.ToString();
        CoinText.text = "Coins X " + coinCount.ToString();
    }

    public void CoinCount()
    {
        coinCount++;
        SetCoinText();
    }

    void CreateDust()
    {
        dust.Play();
    }

    public void powerUp()
    {
        PlaySound(powerUpClip);
        maxSpeed = 30;
        moveSpeed = 30;
        StartCoroutine(changeSpeed());
    }

    public void InvinPowerup()
    {
        PlaySound(powerUpClip);
        StarPowerup = true;
        StartCoroutine(changeInvin());
    }

    IEnumerator changeSpeed()
    {
        yield return new WaitForSeconds(5f);
        maxSpeed = 7;
        moveSpeed = 15;
        PlaySound(powerUpClip);
    }
    IEnumerator changeInvin()
    {
        yield return new WaitForSeconds(5f);
        StarPowerup = false;

        PlaySound(powerUpClip);
    }
}

