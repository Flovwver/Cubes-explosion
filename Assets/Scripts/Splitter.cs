using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Splitter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Explosioner _explosioner;
    [SerializeField] private Raycaster _raycaster;

    private void OnEnable()
    {
        if (_raycaster != null)
            _raycaster.CubeHit += OnCubeHit;
    }

    private void OnDisable()
    {
        if (_raycaster != null)
            _raycaster.CubeHit -= OnCubeHit;
    }

    private void OnCubeHit(CubeBehaviour cubeBehaviour)
    {
        if (Random.value > cubeBehaviour.SplitChance)
            return;

        var bodies = _spawner.SpawnChildren(cubeBehaviour);

        var center = cubeBehaviour.transform.position;

        _explosioner.Explode(bodies, center);
    }
}
