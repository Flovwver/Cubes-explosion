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
        _raycaster.CubeHit += OnCubeHit;
    }

    private void OnDisable()
    {
        _raycaster.CubeHit -= OnCubeHit;
    }

    private void OnCubeHit(GameObject hitObject)
    {
        if (!hitObject.TryGetComponent<CubeBehaviour>(out var cubeData))
            throw new ArgumentException("Hit object does not have CubeBehaviour component", nameof(hitObject));

        if (Random.value > cubeData.SplitChance)
            return;

        var bodies = _spawner.SpawnChildren(hitObject);

        var center = hitObject.transform.position;

        _explosioner.Explode(bodies, center);
    }
}
