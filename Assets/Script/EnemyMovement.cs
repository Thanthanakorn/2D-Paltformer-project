using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody2D _rgbd2D;
    private Animator _myAnimator;
    private CapsuleCollider2D _myCapsuleCollider;
    
    [SerializeField] private AudioClip dyingSfx;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsDying = Animator.StringToHash("isDying");

    void Start()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
        _rgbd2D.isKinematic = true;
        _myAnimator = GetComponent<Animator>();
        _myCapsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        _rgbd2D.velocity = new Vector2(moveSpeed, 0f);
        Walking();
        
    }

    void Walking()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(_rgbd2D.velocity.x) >  Mathf.Epsilon;
        if(playerHasHorizontalSpeed) 
        {
            _myAnimator.SetBool(IsWalking, true);

        }
        else
        {
            _myAnimator.SetBool(IsWalking, false);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        FlipEnemyFacing();
    }

    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2((Mathf.Sign(_rgbd2D.velocity.x)), 1f);
        
    }
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            _rgbd2D.isKinematic = false;
            _myCapsuleCollider.size = new Vector2(0.4f, 0.03f);
            _myAnimator.SetTrigger(IsDying);
            AudioSource.PlayClipAtPoint(dyingSfx, Camera.main.transform.position, .3f);
            GameObject o;
            (o = gameObject).layer = LayerMask.NameToLayer("Background");
            moveSpeed = 0;
            yield return new WaitForSecondsRealtime(2f);
            Destroy(o);
        }
    }
    
}
