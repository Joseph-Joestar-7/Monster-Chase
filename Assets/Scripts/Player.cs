using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveForce = 10f;

    [SerializeField]
    private float jumpForce = 11f;

    private float movementX;

    private Rigidbody2D myBody;

    private SpriteRenderer sr;

    private Animator anim;
    private string WALK_ANIMATION = "Walk";
    private string GROUND_TAG = "Ground";
    private string ENEMY_TAG = "Enemy";

    private string APPLE_TAG = "Apple";
    private string COIN_TAG = "Coin";

    private bool isGrounded;
    private bool isPhasing=false;
    private Coroutine ghostCoroutine;
    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr = GetComponent<SpriteRenderer>(); 

    }

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        PlayerJump();
        AnimatePlayer();
    }

    private void FixedUpdate()
    {
       
    }

    void PlayerMoveKeyboard()
    {
        movementX = Input.GetAxisRaw("Horizontal");

        transform.position += new Vector3(movementX, 0f, 0f) * Time.deltaTime * moveForce;

    }

    void AnimatePlayer()
    {
        if (movementX > 0f)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX=false;
        }

        else if (movementX < 0f)
        {
            anim.SetBool(WALK_ANIMATION, true);
            sr.flipX = true;
        }

        else
        {
            anim.SetBool(WALK_ANIMATION, false);
        }
        if (!isGrounded)
            anim.SetBool("Jump", true);
        else
            anim.SetBool("Jump", false);
    }

    void PlayerJump()
    {
        if(Input.GetButtonDown("Jump")&& isGrounded)
        {
            isGrounded = false;
            myBody.AddForce(new Vector2(0f,jumpForce),ForceMode2D.Impulse);
        }
    }

    private void ActivatePhasing(float duration)
    {
        if (isPhasing) return;
        isPhasing = true;
        Debug.Log("Activated");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
                                       LayerMask.NameToLayer("Enemy"),
                                       true);
        ghostCoroutine = StartCoroutine(GhostPulse(duration));
        StartCoroutine(DisablePhasingAfter(duration));
    }

    private IEnumerator DisablePhasingAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"),
                                       LayerMask.NameToLayer("Enemy"),
                                       false);
        isPhasing = false;
        Debug.Log("Deactivated");

        if (ghostCoroutine != null)
        {
            StopCoroutine(ghostCoroutine);
        }
    }

    private IEnumerator GhostPulse(float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration && isPhasing)
        {
            float alpha = 0.5f + 0.5f * Mathf.PingPong(elapsed * 2f, 1f);
            sr.color = new Color(1f, 1f, 1f, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(1f, 1f, 1f, 1f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(GROUND_TAG))
        {
            isGrounded=true;
        }

        if (collision.gameObject.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY_TAG))
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }

        if(collision.CompareTag(APPLE_TAG))
        {
            Destroy(collision.gameObject);
            ActivatePhasing(5f);
        }

        if (collision.CompareTag(COIN_TAG))
        {
            Destroy(collision.gameObject);
        }
    }
}
