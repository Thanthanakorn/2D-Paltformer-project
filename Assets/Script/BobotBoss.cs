using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BobotBoss : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    private Rigidbody2D _rgbd2D;
    [SerializeField] private Transform gun;
    [SerializeField] private float shootInterval = 2f;
    private bool _isShooting;
    [SerializeField] private int hp = 10;
    public GameObject explosion;
    
    void Start()
    {
        _rgbd2D = GetComponent<Rigidbody2D>();
        _rgbd2D.isKinematic = true;
    }
    void Update()
    {
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
    
    IEnumerator OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            hp -= 1;
            if (hp == 0)
            {
                explosion.SetActive(true);
                GameObject o;
                (o = gameObject).layer = LayerMask.NameToLayer("Background");
                yield return new WaitForSecondsRealtime(2.5f);
                Destroy(o);
                SceneManager.LoadScene("Credit");
            }
        }
    }
}
