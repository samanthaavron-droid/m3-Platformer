using UnityEngine;

public class Respawn : MonoBehaviour
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
            collision.gameObject.GetComponent<MovementPlayer>().lastPos = collision.gameObject.transform.position; //recording last position before death     
        }
    }
}
