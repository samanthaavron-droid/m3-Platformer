using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldMap : MonoBehaviour
{
    public GameObject worldMapUI;

    private int levelCompleted;

    public Button a1, a2, a3, a4, a5;
    void Start()
    {
        levelCompleted = PlayerPrefs.GetInt("LevelCompleted", 0);

        transform.position = levelCompleted switch
        {
            0 => worldMapUI.transform.Find("1").transform.position,
            1 => worldMapUI.transform.Find("2").transform.position,
            2 => worldMapUI.transform.Find("3").transform.position,
            3 => worldMapUI.transform.Find("4").transform.position,
            4 => worldMapUI.transform.Find("5").transform.position,
            _ => throw new System.NotImplementedException()
        };

        a1.onClick.AddListener(() => LoadLevel("FirstLevel"));
        a2.onClick.AddListener(() => LoadLevel(""));
        a3.onClick.AddListener(() => LoadLevel(""));
        a4.onClick.AddListener(() => LoadLevel(""));
        a5.onClick.AddListener(() => LoadLevel(""));
    }
    void Update()
    {
        
    }
    void LoadLevel(string level)
    { 
        SceneManager.LoadScene(level);
    }
}
