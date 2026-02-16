using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();

    public float maxRangePlus;
    public float maxRangeMinus;
    public float speed;

    private bool reachMin;
    private bool reachMax;
    void Start()
    {
        maxRangeMinus = transform.position.x - maxRangeMinus;
        maxRangePlus = transform.position.x + maxRangePlus;
    }
    void Update()
    {
        Movement();

        if (transform.position.x <= maxRangeMinus)
        {
            reachMin = true;
            reachMax = false;
        }
        else if (transform.position.x >= maxRangePlus)
        {
            reachMin = false;
            reachMax = true;
        }
    }
    void Movement()
    {
        if (reachMin == false)
        {
            _rb.linearVelocity = Vector3.left * speed;
        }
        else if (reachMax == false)
        {
            _rb.linearVelocity = Vector3.right * speed;
        }
    }
}
