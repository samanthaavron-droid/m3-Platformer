using UnityEngine;

public class Collectable : MonoBehaviour
{
    private AudioSource _audioSource => GetComponent<AudioSource>();
    void Start()
    {
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.coinPoll += 1;
            _audioSource.Play();
            Destroy(gameObject);
        }
    }
}
