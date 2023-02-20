using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class EmemyMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    private Rigidbody2D _rgbd2D;
    private Animator _myAnimator;
    private CapsuleCollider2D _myCapsuleCollider;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsDying = Animator.StringToHash("isDying");

    void Start()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
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
            _myCapsuleCollider.size = new Vector2(0.4f, 0f);
            _myAnimator.SetTrigger(IsDying);
            gameObject.layer = LayerMask.NameToLayer("Background");
            moveSpeed = 0;
            
            yield return new WaitForSecondsRealtime(3f);
            Destroy(gameObject);
        }
    }
    
}
