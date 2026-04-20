using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider _spawner;

    [SerializeField] private GameObject[] _fruitPrefab;
    [SerializeField] private GameObject _bombPrefab;
    [SerializeField] private float _spawnBombChance = 0.05f;

    [SerializeField] private float _minDelay = 0.3f;
    [SerializeField] private float _maxDalay = 1f;

    [SerializeField] private float _minAngle = -13f;
    [SerializeField] private float _maxAngle = 13f;

    [SerializeField] private float _minForce = 14f;
    [SerializeField] private float _maxForce = 17f;

    [SerializeField] private float _lifeTime = 4f;

    private void Awake()
    {
        _spawner = GetComponent<Collider>();
    }

    private void OnEnable()
    {
        StartCoroutine(Spawn());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0.5f);

        while (enabled)
        {
            GameObject prefab = _fruitPrefab[Random.Range(0, _fruitPrefab.Length)];

            if (Random.value < _spawnBombChance)
            {
                prefab = _bombPrefab;
            }

            Vector3 position = new Vector3();
            position.x = Random.Range(_spawner.bounds.min.x, _spawner.bounds.max.x);
            position.y = Random.Range(_spawner.bounds.min.y, _spawner.bounds.max.y);
            position.z = Random.Range(_spawner.bounds.min.z, _spawner.bounds.max.z);

            Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(_minAngle, _maxAngle));

            GameObject fruit = Instantiate (prefab, position, rotation);
            Destroy(fruit, _lifeTime);

            float force = Random.Range(_minForce, _maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(fruit.transform.up * force, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(_minDelay, _maxDalay));
        }
    }
}
