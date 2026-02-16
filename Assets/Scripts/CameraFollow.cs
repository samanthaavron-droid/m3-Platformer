using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offSet;
    [Range(0, 10)]
    public float smoothTime;

    void Start()
    {

    }
    void Update()
    {
        Follow();
    }
    void Follow()
    {
        Vector3 targetPosition = target.position + offSet;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothTime * Time.fixedDeltaTime);
        transform.position = smoothedPosition;
    }
}
