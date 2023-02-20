using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    [SerializeField] private AudioClip moneyPickupSfx;
    [SerializeField] private int coinValue = 10;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameSession>().AddToScore(coinValue);
            
            AudioSource.PlayClipAtPoint(moneyPickupSfx, Camera.main.transform.position, .3f);
            Destroy(gameObject);
        }
    }
}
