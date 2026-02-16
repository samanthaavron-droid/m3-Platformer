using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            playerHealth.TakeDamage(10);

            MovementPlayer playerMovement = collision.gameObject.GetComponent<MovementPlayer>();
            playerMovement.Respawn();
        }
    }
}
