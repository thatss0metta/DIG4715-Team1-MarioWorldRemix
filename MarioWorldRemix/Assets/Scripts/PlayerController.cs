using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Horizontal Movement")]
    public float moveSpeed = 10f;
    public Vector2 direction;
    private bool facingRight = true;

    [Header("Components")]
    public Rigidbody2D rb;
    //public Animator animator; 
    public LayerMask groundLayer;

    [Header("Canvas")]
    public GameObject loseScreen;
    AudioSource audioSource;
    public TextMeshProUGUI CoinText;
    public TextMeshProUGUI CoinTextFront;
    int coinCount = 0;
    public AudioClip jumpClip;

    [Header("Physics")]
    public float maxSpeed = 7f;
    public float linearDrag = 4f;

    [Header("Collision")]
    public bool onGround = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        onGround = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        moveCharacter(direction.x);
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
        //animator.SetFloat("horizontal", Mathf.Abs(rb.velocity.x));

    }
    void modifyPhysics()
    {
        bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);

        if (Mathf.Abs(direction.x) < 0.4f || changingDirections)
        {
            rb.drag = linearDrag;
        }
        else
        {
            rb.drag = 0f;
        }

    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                PlaySound(jumpClip);
                rb.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void Death()
    {

        loseScreen.gameObject.SetActive(true);
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

}