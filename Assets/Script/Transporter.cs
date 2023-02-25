using UnityEngine;

public class Transporter : MonoBehaviour
{
    [SerializeField] private float backwardDistance = 1f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.position -= new Vector3(backwardDistance, 0f, 0f);
        }
    }
}

