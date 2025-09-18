using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private CubeBehaviour _progenitorCube;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private Vector3 _baseSpawnOffset;

    private void OnEnable()
    {
        _progenitorCube.Clicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        _progenitorCube.Clicked -= OnCubeClicked;
    }

    public IEnumerable<Rigidbody> SpawnChildren(GameObject parentCube)
    {
        if (!parentCube.TryGetComponent<CubeBehaviour>(out var parentData))
            yield break;

        int count = Random.Range(_minChildren, _maxChildren + 1);

        Vector3 center = parentCube.transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * parentCube.transform.localScale.x + _baseSpawnOffset;
            GameObject child = Instantiate(_cubePrefab, center + randomOffset, Random.rotation);
            child.transform.localScale = parentCube.transform.localScale * parentData.ScaleFactor;

            var renderer = child.GetComponent<Renderer>();
            renderer.material.SetColor("_EmissionColor", new(Random.value, Random.value, Random.value));

            var childData = child.GetComponent<CubeBehaviour>();
            childData.SplitChance = parentData.SplitChance * parentData.SplitChanceFactor;
            childData.SplitChanceFactor = parentData.SplitChanceFactor;
            childData.ScaleFactor = parentData.ScaleFactor;
            
            childData.Clicked += OnCubeClicked;

            var body = child.GetComponent<Rigidbody>();
            yield return body;
        }
    }

    private void OnCubeClicked(CubeBehaviour cube)
    {
        if (cube == null)
            throw new ArgumentNullException(nameof(cube));

        cube.Clicked -= OnCubeClicked;

        Destroy(cube.gameObject);
    }
}
