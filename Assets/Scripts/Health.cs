using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health;
    public Image deathEffect;
    public Image healthbar;

    private Animator _anim => GetComponent<Animator>();

    [SerializeField]
    private GameObject _camera;

    private Vector3 _originalPos;

    [SerializeField]
    private float _shakeIntensity = 1f;

    public bool isShaking = false;

    public int coinPoll;

    private float _coolDown;

    void Awake()
    {
        deathEffect.gameObject.SetActive(false);
    }
    void Update()
    {
        _originalPos = _camera.transform.position;

        if (isShaking)
        {
            StartCoroutine(CameraShakeRoutire());
        }

        healthbar.fillAmount = health / 100f;
    }
    public void TakeDamage(int damage)
    {
        if (Time.time < _coolDown)
        {
            return;
        }
        
        StartCoroutine(CameraShakeRoutire());

        health -= damage;
        _anim.SetTrigger("damage");
        isShaking = true;

        _coolDown = Time.time + 1f;

        transform.Find("hitSound").GetComponent<AudioSource>().Play();

        if (health <= 0)
        {
            deathEffect.gameObject.SetActive(true);
            Time.timeScale = 0f; //pausing game
        }
    }
    System.Collections.IEnumerator CameraShakeRoutire()
    {
        _camera.transform.position = _originalPos + (Random.insideUnitSphere * _shakeIntensity);
        yield return new WaitForSeconds(0.25f);
        _camera.transform.position = _originalPos;
        isShaking = false;
    }
}
