using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraFollow : MonoBehaviour
{
    public float yOffset = -1.39f;
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)] public float smoothFactor = 3;
    public Vector3 minValue, maxValue;

    void FixedUpdate()
    {
        Vector3 targetPosition = target.position + offset;
        //Verify if the targetPosition is out of bound or not 
        //Limit it to the min and max values
        Vector3 boundPosition = new Vector3(
            Mathf.Clamp(targetPosition.x, minValue.x, maxValue.x),
            Mathf.Clamp(targetPosition.y, minValue.y, maxValue.y),
            Mathf.Clamp(targetPosition.z, minValue.z, maxValue.z));

        Vector3 smoothPosition = Vector3.Lerp(transform.position, boundPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = smoothPosition;
    }
}