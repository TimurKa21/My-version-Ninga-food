using UnityEngine;

public class FruitRotator : MonoBehaviour
{
    [SerializeField] private float _minRotationSpeed = 100f;
    [SerializeField] private float _maxRotationSpeed = 150f;
    [SerializeField] private bool _direction = true;

    private float _rotationSpeed;

    private void Start()
    {
        _rotationSpeed = Random.Range(_minRotationSpeed, _maxRotationSpeed) * (_direction ? 1f : -1f);
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime);
    }
}
