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

    private void OnCubeHit(CubeBehaviour cubeBehaviour)
    {
        if (Random.value <= cubeBehaviour.SplitChance)
            _spawner.SpawnChildren(cubeBehaviour);

        _explosioner.Explode(cubeBehaviour);
    }
}
