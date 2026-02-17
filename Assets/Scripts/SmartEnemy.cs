using UnityEngine;

public class SmartEnemy : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private Animator _anim => GetComponent<Animator>();

    private Health playerHealth;

    public GameObject player;

    public int damage = 10;
    public int health = 10;

    public float speed;


    private float _coolDown;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Movement();

        //rotation towards direction of movement
        if (_rb.linearVelocityX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (_rb.linearVelocityX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        if (health <= 0)
        {
            transform.Find("deathSound").GetComponent<AudioSource>().Play();
            Destroy(gameObject, 0.5f);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LayerMask playerLay = LayerMask.GetMask("Player");
            RaycastHit2D hitLeft = Physics2D.CircleCast(transform.position, 0.5f, Vector2.left, 1f, playerLay);
            RaycastHit2D hitRight = Physics2D.CircleCast(transform.position, 0.5f, -Vector2.left, 1f, playerLay);

            if (hitLeft.collider != null || hitRight.collider != null)
            {
                playerHealth = collision.gameObject.GetComponent<Health>();
                playerHealth.TakeDamage(damage);
            }

        }
    }
    void Movement()
    {
        Vector2 direction = player.transform.position - transform.position;

        _rb.linearVelocity = new Vector2(direction.x * speed, _rb.linearVelocityY);
    }
    public void TakeDamage(int damage)
    {
        if (Time.time < _coolDown)
        {
            return;
        }
        _coolDown = Time.time + 0.5f;

        transform.Find("hitSound").GetComponent<AudioSource>().Play();

        health -= damage;
        _anim.SetTrigger("hit");
    }
}
