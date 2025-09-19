using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Cube _progenitorCube;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private Vector3 _baseSpawnOffset;

    public IEnumerable<Rigidbody> SpawnChildren(Cube parentCube)
    {
        int count = Random.Range(_minChildren, _maxChildren + 1);

        Vector3 center = parentCube.transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * parentCube.transform.localScale.x + _baseSpawnOffset;
            Cube child = Instantiate(_cubePrefab, center + randomOffset, Random.rotation);
            child.transform.localScale = parentCube.transform.localScale * parentCube.ScaleFactor;

            child.SplitChance = parentCube.SplitChance * parentCube.SplitChanceFactor;
            child.SplitChanceFactor = parentCube.SplitChanceFactor;
            child.ScaleFactor = parentCube.ScaleFactor;

            yield return child.Rigidbody;
        }
    }

    public void DestroyCube(Cube cube)
    {
        if (cube == null)
            throw new ArgumentNullException(nameof(cube));

        Destroy(cube.gameObject);
    }
}
