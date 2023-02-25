using System.Collections;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public Transform player;
    [SerializeField] private GameObject bulletPrefab;
    public float shootingRange = 50f;
    private Rigidbody2D _rgbd2D;
    private Animator _myAnimator;
    [SerializeField] private Transform gun;
    [SerializeField] private float shootInterval = 2f;
    private bool _isShooting;
    
    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsDying = Animator.StringToHash("isDying");
    
    void Start()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
        _rgbd2D.isKinematic = true;
        _myAnimator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Vector2.Distance(transform.position, player.position) < shootingRange)
        {
            //_rgbd2D.velocity = new Vector2(moveSpeed, 0f);
            FlipEnemyFacing();
        }
        if (!_isShooting)
        {
            StartCoroutine(Shoot());
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    IEnumerator Shoot()
    {
        _isShooting = true;
        yield return new WaitForSecondsRealtime(shootInterval);
        Instantiate(bulletPrefab, gun.position, transform.rotation);
        _isShooting = false;
    }
    
    void FlipEnemyFacing()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        transform.localScale = new Vector2(Mathf.Sign(directionToPlayer.x), 1f);
    }
    
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            gun.position = new Vector3(-6, -6, 0);
            shootingRange = 0;
            _myAnimator.SetTrigger(IsDying);
            GameObject o;
            (o = gameObject).layer = LayerMask.NameToLayer("Background");
            yield return new WaitForSecondsRealtime(2.5f);
            Destroy(o);
        }
    }
}
