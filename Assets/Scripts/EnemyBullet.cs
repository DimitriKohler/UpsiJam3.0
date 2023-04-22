using UnityEngine;
using UnityEngine.Serialization;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float directionScale = 1.5f;
    Vector3 _targetPosition;

    private void Update()
    {
        transform.position = Vector2.Lerp(transform.position, _targetPosition, Time.deltaTime * speed);
    }

    public void Initialize(Vector3 target, float angle)
    {
        var direction = Quaternion.Euler(0, 0, angle) * (target - transform.position);
        _targetPosition = transform.position + direction * directionScale;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}