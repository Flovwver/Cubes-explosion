using System.Collections.Generic;
using UnityEngine;

public class RayCastCloner : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private Ray _ray;
    [SerializeField] private int _minChildren = 2;
    [SerializeField] private int _maxChildren = 6;
    [SerializeField] private Vector3 _childrenOffset;
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 2f;
    [SerializeField] private float _maxRayDistance = 10f;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(_ray.origin, _ray.direction * _maxRayDistance, Color.magenta);

        if (!Input.GetMouseButtonDown(0)) 
            return;

        if (Physics.Raycast(_ray, out RaycastHit hit, _maxRayDistance))
        {
            GameObject hitObj = hit.collider.gameObject;
            
            if (hitObj.TryGetComponent<CubeBehaviour>(out var cubeBehaviour))
                HandleSplit(hit.point, hitObj, cubeBehaviour);
        }
    }

    private void HandleSplit(Vector3 center, GameObject parentCube, CubeBehaviour parentData)
    {
        if (Random.value > parentData.SplitChance)
        {
            Destroy(parentCube);
            return;
        }

        int count = Random.Range(_minChildren, _maxChildren + 1);
        List<Rigidbody> spawnedBodies = new(count);

        for (int i = 0; i < count; i++)
        {
            Vector3 randomOffset = Random.insideUnitSphere * parentCube.transform.localScale.x + _childrenOffset;
            GameObject child = Instantiate(_cubePrefab, center + randomOffset, Random.rotation);
            child.transform.localScale = parentCube.transform.localScale * parentData.ScaleFactor;

            if (child.TryGetComponent<Renderer>(out var renderer))
                renderer.material.SetColor("_EmissionColor", new(Random.value, Random.value, Random.value));

            if (child.TryGetComponent<CubeBehaviour>(out var childData))
            {
                childData.SplitChance = parentData.SplitChance * parentData.SplitChanceFactor;
                childData.SplitChanceFactor = parentData.SplitChanceFactor;
                childData.ScaleFactor = parentData.ScaleFactor;
            }

            if (child.TryGetComponent<Rigidbody>(out var body))
                spawnedBodies.Add(body);
        }

        foreach (var body in spawnedBodies)
            body.AddExplosionForce(_explosionForce, center, _explosionRadius);

        Destroy(parentCube);
    }

}
