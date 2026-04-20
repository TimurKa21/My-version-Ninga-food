using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public Vector3 Direction { get; private set; }
    public float SliceForce = 8f;

    private Camera _mainCamera;
    private Collider _bladeCollider;
    private TrailRenderer _bladeTrail;

    private bool _slicing;

    private Vector3 _startPosition;

    private void Awake()
    {
        _mainCamera = Camera.main;
        _bladeTrail = GetComponentInChildren<TrailRenderer>();
        _bladeCollider = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StopSlicing();
    }

    private void OnDisable()
    {
        StopSlicing();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartSlicing();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopSlicing();
        }
        else if (_slicing)
        {
            ContinueSlicing();
        }
    }

    private void StartSlicing()
    {
        _startPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        _startPosition.z = 0f;
        transform.position = _startPosition;

        _slicing = true;
        _bladeCollider.enabled = true;
        _bladeTrail.enabled = true;
        _bladeTrail.Clear();
    }

    private void StopSlicing()
    {
        _slicing &= false;
        _bladeCollider.enabled = false;
        _bladeTrail.enabled = false;
    }

    private void ContinueSlicing()
    {
        Vector3 newPosition = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
        newPosition.z = 0f;

        Direction = newPosition - _startPosition;

        //Debug.Log($"Blade Direction: {Direction}");

        transform.position = newPosition;
    }
}
