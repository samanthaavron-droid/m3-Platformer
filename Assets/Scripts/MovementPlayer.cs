using UnityEngine;
using UnityEngine.InputSystem;

public class MovementPlayer : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private Animator _anim => GetComponent<Animator>();
    private Health _health => GetComponent<Health>();

    private float _rx; //directional movement record

    public float speed;
    public float jumpForce;

    private int _jumpCount; //record for number of jumps

    public InputActionReference movement; //movement input
    public InputActionReference jump; //jump input

    private float _attackCooldownTime;

    [HideInInspector]
    public Vector3 lastPos;
    void Start()
    {
        
    }
    void Update()
    {
        Movement(); //calling movement method
        jump.action.performed += Jump; //calling jump method

        float jumpDir = _rb.linearVelocityY;
        _anim.SetFloat("jumpDir", jumpDir); // falling anim
    }
    private void FixedUpdate()
    {
        _anim.SetBool("walkingState", false);
    }
    void Movement()
    {
        if (movement.action.ReadValue<Vector2>() != Vector2.zero)
        {
            if (!_anim.GetBool("walkingState"))
            {
                //enabling walking anim
                _anim.SetBool("walkingState", true);
            }
            //movement in x direction record
            _rx = movement.action.ReadValue<Vector2>().x;

            //applying directional movement
            _rb.linearVelocity = new Vector2(_rx * speed, _rb.linearVelocityY);
            transform.Find("WalkSound").GetComponent<AudioSource>().Play();

            //rotation towards direction of movement
            if (_rx > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (_rx < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
        }
    }
    private void Jump(InputAction.CallbackContext obj)
    {
        _jumpCount++;

        if (_jumpCount < 3)
        {
            _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce); // jump force
            _anim.SetInteger("jumpCount", _jumpCount); //jump anim true
            transform.Find("jumpSound").GetComponent<AudioSource>().Play();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            GroundChecker();
        } 
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (Time.time < _attackCooldownTime)
            {
                return;
            }
            
            AttackEnemy(collision.gameObject);
            _attackCooldownTime = Time.time + 1f;
        }
    }
    private void GroundChecker()
    {
        LayerMask groundLayer = LayerMask.GetMask("Ground");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        if (hit.collider != null && hit.collider.CompareTag("Ground"))
        {
            _jumpCount = 0; //reset jumpCount for double Jump
            _anim.SetInteger("jumpCount", _jumpCount); //jump anim true
            Debug.Log("Grounded");
        } else
        {
            Debug.Log("Not Grounded");
        }
        Debug.DrawRay(transform.position, Vector2.down, Color.red, 1f);
    }
    public void Respawn()
    {
        _rb.linearVelocity = Vector3.zero;
        _rb.position = lastPos; //respawn at last position
    }
    private void AttackEnemy(GameObject enemy)
    {
        LayerMask enemyLay = LayerMask.GetMask("Enemies");
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.2f, Vector2.down, 1f, enemyLay);
        Debug.DrawRay(transform.position, Vector2.down, Color.blue, 1f);
        if (hit.collider != null)
        {
            if (enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeDamage(10);
                _health.isShaking = true;
                _jumpCount = 1; //reset jumpCount for double Jump

                //Jump after hitting enemy
                _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce); // jump force
                _anim.SetInteger("jumpCount", _jumpCount); //jump anim true
            }
            if (enemy.GetComponent<SmartEnemy>() != null)
            {
                enemy.GetComponent<SmartEnemy>().TakeDamage(10);
                _health.isShaking = true;
                _jumpCount = 1; //reset jumpCount for double Jump

                //Jump after hitting enemy
                _rb.linearVelocity = new Vector2(_rb.linearVelocityX, jumpForce); // jump force
                _anim.SetInteger("jumpCount", _jumpCount); //jump anim true
            }
        }       
    }
}
