using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, IDamagable
{
    // Start is called before the first frame update
    private Rigidbody2D _rigidbody;
    [SerializeField]
    private float lift_force = 5.0f;
    [SerializeField] 
    private float speed = 3.0f;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private LayerMask _groundLayer;

    private PlayerAnimations _playerAnim;

    private bool resetJumpNeeded = false;

    private SpriteRenderer _pSpriteRenderer;

    private SpriteRenderer _swordArcSpriteRenderer;

    public int Health { get; set; }


    void Start()
    {
        Health = 3;
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<PlayerAnimations>();
        _pSpriteRenderer = GetComponent<SpriteRenderer>();
        _swordArcSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();


    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Mouse0) && IsGrounded() == true)
        {
            _playerAnim.Attack();   
        }
    }

    bool IsGrounded()
    {
        float move = Input.GetAxisRaw("Horizontal");
        Vector3 center = new Vector3(0.10f, 0.0f, 0.0f);;
        
        /// FLIP
        if (move > 0)
        {
            _pSpriteRenderer.flipX = false;
        }
        else if (move < 0)
        {
            _pSpriteRenderer.flipX = true;
        }

        if (!_pSpriteRenderer.flipX)
        {
            center = new Vector3(0.10f, 0.0f, 0.0f);
        }

        else if (_pSpriteRenderer.flipX)
        {
            center = new Vector3(-0.10f, 0.0f, 0.0f);
        }
        
        //Cast a ray straight down
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - center, Vector2.down , 0.8f, _groundLayer );
        Debug.DrawRay( transform.position - center, Vector2.down * 0.8f, Color.green);

        if (hitInfo.collider != null)
        {
            if (resetJumpNeeded == false){
                _playerAnim.Jump(false);
            return true;
        }
    }

        return false;
    }

    void Movement()
    {
        float move = Input.GetAxisRaw("Horizontal");

        grounded = IsGrounded();
        
        /// FLIP
        if (move > 0)
        {
            _pSpriteRenderer.flipX = false;
            _swordArcSpriteRenderer.flipX = false;
            _swordArcSpriteRenderer.flipY = false;

            Vector3 newPosition = _swordArcSpriteRenderer.transform.localPosition;
            newPosition.x = 1.01f;
            _swordArcSpriteRenderer.transform.localPosition = newPosition;
        }
        else if (move < 0)
        {
            _pSpriteRenderer.flipX = true;
            _swordArcSpriteRenderer.flipX = true;
            _swordArcSpriteRenderer.flipY = true;
            Vector3 newPosition = _swordArcSpriteRenderer.transform.localPosition;
            newPosition.x = -1.01f;
            _swordArcSpriteRenderer.transform.localPosition = newPosition;
        }
        ///

        if (IsGrounded() == true && Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, lift_force);
            StartCoroutine(ResetJumpNeededRoutine());
            _playerAnim.Jump(true);
        }
        
        _rigidbody.velocity = new Vector2(move * speed, _rigidbody.velocity.y);
        
        _playerAnim.Move(move);
    }

    
    IEnumerator ResetJumpNeededRoutine()
    {
        resetJumpNeeded = true;
        yield return new WaitForSeconds(0.1f); 
        resetJumpNeeded = false;

    }

    public void Damage()
    {
        Debug.Log("Player is attacked");
    }
}
