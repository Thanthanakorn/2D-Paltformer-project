using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D _rgbd2D;
    private Animator _myAnimator;

    [SerializeField] float speed = 4;
    [SerializeField] float jumpSpeed = 4.0f;
    [SerializeField] private Vector2 deathKick = new Vector2(0, 2f);

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    
    private Vector2 _moveInput;
    private CapsuleCollider2D _myCapsuleCollider;
    private CircleCollider2D _myCircleCollider;
    private BoxCollider2D _myBoxCollider;
    private bool _isAlive = true;
    private bool _isMoving;
    
    [SerializeField] private AudioClip shootingSfx;
    [SerializeField] private AudioClip dyingSfx;
    
    private static readonly int IsJumping = Animator.StringToHash("isJumping");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsShooting = Animator.StringToHash("isShooting");
    private static readonly int Dying = Animator.StringToHash("Dying");

    void Start()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        _myCircleCollider = GetComponent<CircleCollider2D>();
        _myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if(!_isAlive) {return;}
        Run();
        FlipSprite();
        Die();
        Transporter();
        if (!_myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            _myAnimator.SetBool(IsJumping, false);
        }
    }
    
    void FlipSprite()
    {
        bool playHasHorizontalSpeed = Mathf.Abs(_rgbd2D.velocity.x) > Mathf.Epsilon; 
        if(playHasHorizontalSpeed) 
        {
            transform.localScale = new Vector2 (Mathf.Sign(_rgbd2D.velocity.x), 1f);
        }
    }
    
    void OnMove(InputValue value)
    {
        if(!_isAlive) {return;}
        _moveInput = value.Get<Vector2>();
    }
    
    void Run()
    {
        if (_myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) && !_myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Transporter")))
        {
            return;
        }
        var velocity = _rgbd2D.velocity;
        Vector2 playerVelocity = new Vector2 (_moveInput.x * speed, velocity.y);
        velocity = playerVelocity;
        _rgbd2D.velocity = velocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(velocity.x) >  Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            _isMoving = true;
            _myAnimator.SetBool(IsRunning, true);

        }
        else
        {
            _isMoving = false;
            _myAnimator.SetBool(IsRunning, false);
        }
    }

    void OnJump(InputValue value)
    {
        if(!_isAlive) {return;}

        if (!_myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Transporter")))
        {
            return;
        }
        if(value.isPressed)
        {
            _rgbd2D.velocity += new Vector2 (0f, jumpSpeed);
            _myAnimator.SetBool(IsJumping, true);
        }
    }

    void OnFire(InputValue value)
    {
        if(!_isAlive){ return;}
        _myAnimator.SetTrigger(IsShooting);
        AudioSource.PlayClipAtPoint(shootingSfx, Camera.main.transform.position, .3f);
        Instantiate(bullet, gun.position, transform.rotation);
    }

    void Die()
    {
        if (_myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards","Enemy's bullet", "NotAllow"))||_myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards","Enemy's bullet", "NotAllow")))
        {
            _isAlive = false;
            _myAnimator.SetTrigger(Dying);
            _rgbd2D.velocity = deathKick;
            _myCapsuleCollider.size = new Vector2(0.5f, 0.7f);
            _myCircleCollider.radius = 0;
            AudioSource.PlayClipAtPoint(dyingSfx, Camera.main.transform.position, .3f);
            //Get the Session object
            StartCoroutine(InformGameSession());
        }
    }

    IEnumerator InformGameSession()
    {
        yield return new WaitForSecondsRealtime(1f);
        
        FindObjectOfType<GameSession>().ProcessPlayerDeath();
    }
    
    private void Transporter()
    {
        if (!_myCircleCollider.IsTouchingLayers(LayerMask.GetMask("Transporter")))
        {
            return;
        }

        if (!_isMoving)
        {
            _rgbd2D.velocity = new Vector2(-2f, _rgbd2D.velocity.y);
        }
        else
        {
            float tempSpeed = speed * 0.5f;
            var velocity = _rgbd2D.velocity;
            Vector2 playerVelocity = new Vector2(_moveInput.x * tempSpeed, velocity.y);
            velocity = playerVelocity;
            _rgbd2D.velocity = velocity;
        }
    }
}
