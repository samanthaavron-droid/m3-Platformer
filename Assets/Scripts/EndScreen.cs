using UnityEngine;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    public Image endScreen;
    void Awake()
    {
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            endScreen.gameObject.SetActive(true);
            Time.timeScale = 0f; //pausing game
        }
    }
}
