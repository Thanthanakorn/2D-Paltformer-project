using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    private Rigidbody2D _rgbd;
    PlayerMovement _player;
    private float _xSpeed;
    
    void Start()
    {
        _rgbd = GetComponent<Rigidbody2D>();
        _player = FindObjectOfType<PlayerMovement>();
        _xSpeed = _player.transform.localScale.x * bulletSpeed;
        Destroy(gameObject,0.5f);
    }

    void Update()
    {
        _rgbd.velocity = new Vector2(_xSpeed, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
    
}
