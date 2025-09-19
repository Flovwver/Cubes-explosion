using UnityEngine;

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

    private void OnCubeHit(Cube parentCube)
    {
        if (Random.value > parentCube.SplitChance)
        {
            _explosioner.Explode(parentCube);
        }
        else
        {
            var bodies = _spawner.SpawnChildren(parentCube);
            var center = parentCube.transform.position;

            _explosioner.Explode(bodies, center);
        }

        _spawner.DestroyCube(parentCube);
    }
}
