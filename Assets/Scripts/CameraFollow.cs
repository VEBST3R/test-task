using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 1.5f, -10f);
    
    private bool canFollow = true;
    private bool returningToZero = false;

    public void ResetToZero()
    {
        canFollow = false;
        returningToZero = true;
    }

    void LateUpdate()
    {
        if (returningToZero)
        {
            Vector3 targetZero = new Vector3(0, 0, offset.z);
            transform.position = Vector3.Lerp(transform.position, targetZero, smoothSpeed * Time.deltaTime);
            return;
        }

        if (target == null || !canFollow)
            return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
