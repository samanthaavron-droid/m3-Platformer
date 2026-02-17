using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class EndScreen : MonoBehaviour
{
    public Image endScreen;

    [Range(0, 4)]
    public int nextLevel;

    GameObject[] gems;
    int gemsl;
    void Awake()
    {
        endScreen.gameObject.SetActive(false);
    }

    void Start()
    {
        gems = GameObject.FindGameObjectsWithTag("Gem");
        Debug.Log("Gems needed: " + gems.Length);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (collision.gameObject.GetComponent<Health>().coinPoll == gems.Length)
            {
                endScreen.gameObject.SetActive(true);
                Debug.Log("level complete");
                PlayerPrefs.SetInt("LevelCompleted", nextLevel);
                StartCoroutine(MapLoadr());
            } else
            {
                transform.Find("failSound").GetComponent<AudioSource>().Play();
                Debug.Log("You have: " + collision.gameObject.GetComponent<Health>().coinPoll + "; Needed: " + gems.Length);
            }
            
        }
    }
    System.Collections.IEnumerator MapLoadr()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("WorldMap");
    }
}
