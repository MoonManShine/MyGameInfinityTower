using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedX;
    [SerializeField] private Animator animator;
    [SerializeField] private Transform playerModelTransform;
    [SerializeField] private Transform swordHitBoxTransform;
    [SerializeField] private float fallThroughDuration = 0.5f;

    private float _horizontal = 0f;
    private bool _isFacingRight = true;
    private bool _isGround = false;
    private bool _isJump = false;
    private bool _isTouchingWall = false;
    private bool _isFallingThrough = false;
    private Rigidbody2D _rb;
    const float speedXMultiplier = 50f;

    int playerObj, platformsObj;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerObj = LayerMask.NameToLayer("Player");
        platformsObj = LayerMask.NameToLayer("Platforms");
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxis("Horizontal");
        animator.SetFloat("speedX", Mathf.Abs(_horizontal));

        if (Input.GetKeyDown(KeyCode.Space) && _isGround)
        {
            _isJump = true;
        }
        if (Input.GetKeyDown(KeyCode.S) && !_isFallingThrough)
        {
            StartCoroutine(DisablePlatformsCollision());
            Debug.Log("S pressed disabling collision");
        }
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_horizontal * speedX * speedXMultiplier * Time.fixedDeltaTime, _rb.linearVelocity.y);

        if (_isJump)
        {
            _rb.AddForce(new Vector2(0f, 500f));
            _isGround = false;
            _isJump = false;
        }
        if (_horizontal > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (_horizontal < 0 && _isFacingRight)
        {
            Flip();
        }
        if (_isTouchingWall && !_isGround)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, -5f);
            animator.SetBool("wallSlide", true);
        }
    }

    void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 playerScale = playerModelTransform.localScale;
        playerScale.x *= -1;
        playerModelTransform.localScale = playerScale;

        //Mirror the hitbox
        Vector3 hitBoxLocalPos = swordHitBoxTransform.localPosition;
        hitBoxLocalPos.x *= -1;
        swordHitBoxTransform.localPosition = hitBoxLocalPos;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground") && _rb.linearVelocity.y <= 0)
        {
            _isGround = true;
            animator.SetBool("wallSlide", false);
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isGround = false;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            _isTouchingWall = false;
            animator.SetBool("wallSlide", false);
        }
    }

   IEnumerator DisablePlatformsCollision()
    {
        _isFallingThrough = true;
        Physics2D.IgnoreLayerCollision(playerObj, platformsObj, true);
        _rb.linearVelocity = new Vector2(_rb.linearVelocityX, -5f);
        yield return new WaitForSeconds(fallThroughDuration);
        Debug.Log("Start Coroutine");
        Physics2D.IgnoreLayerCollision(playerObj, platformsObj, false);
        _isFallingThrough = false;
    }
    
}
