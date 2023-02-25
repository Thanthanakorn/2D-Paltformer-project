using System.Collections;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Vector3 _playerPosition;
    private Rigidbody2D _rb2d;

    void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _playerPosition = (GameObject.FindWithTag("Player").transform.position - transform.position).normalized;
        StartCoroutine(DestroyBullet());
    }

    void Update()
    {
        _rb2d.velocity = _playerPosition * bulletSpeed;
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.collider.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}