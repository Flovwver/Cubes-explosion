using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private CubeBehaviour _cubePrefab;
    [SerializeField] private CubeBehaviour _progenitorCube;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private Vector3 _baseSpawnOffset;

    private void OnEnable()
    {
        if (_progenitorCube != null)
            _progenitorCube.Clicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        if (_progenitorCube != null)
            _progenitorCube.Clicked -= OnCubeClicked;
    }

    public void SpawnChildren(CubeBehaviour parentCube)
    {
        int count = Random.Range(_minChildren, _maxChildren + 1);

        Vector3 center = parentCube.transform.position;

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * parentCube.transform.localScale.x + _baseSpawnOffset;
            CubeBehaviour child = Instantiate(_cubePrefab, center + randomOffset, Random.rotation);
            child.transform.localScale = parentCube.transform.localScale * parentCube.ScaleFactor;

            var renderer = child.GetComponent<Renderer>();
            renderer.material.SetColor("_EmissionColor", new(Random.value, Random.value, Random.value));

            child.SplitChance = parentCube.SplitChance * parentCube.SplitChanceFactor;
            child.SplitChanceFactor = parentCube.SplitChanceFactor;
            child.ScaleFactor = parentCube.ScaleFactor;

            child.Clicked += OnCubeClicked;
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
