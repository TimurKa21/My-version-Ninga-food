using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slicing : MonoBehaviour
{
    [SerializeField] private GameObject _whole;
    [SerializeField] private GameObject _sliced;
    private GameScore _gameScore;

    private Rigidbody _fruitRigidBody;
    private Collider _fruitCollider;
    private AudioSource _sliceSound;
    private ParticleSystem _juiceParticleEffect;

    [SerializeField] private int _points = 1;

    private void Awake()
    {
        _fruitRigidBody = GetComponent<Rigidbody>();
        _fruitCollider = GetComponent<Collider>();
        _sliceSound = GetComponentInChildren<AudioSource>();
        _juiceParticleEffect = GetComponentInChildren<ParticleSystem>();

        _gameScore = FindFirstObjectByType<GameScore>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _sliceSound.Play();
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.Direction, blade.transform.position, blade.SliceForce);
        }
    }

    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        _gameScore.AddScore(_points);

        _whole.SetActive(false);
        _sliced.SetActive(true);

        _fruitCollider.enabled = false;
        _juiceParticleEffect.Play();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = _sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices)
        {
            slice.velocity = _fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }
}