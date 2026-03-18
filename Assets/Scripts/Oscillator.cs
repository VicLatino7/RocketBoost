using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Vector3 movementVector;
    Vector3 startPosition;
    Vector3 endPosition;
    float movementFactor;
    void Start()
    {
        startPosition = transform.position; // cache the starting position
        endPosition = startPosition + movementVector;
    }

    private void FixedUpdate() 
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPosition, endPosition, movementFactor);
    }
}
