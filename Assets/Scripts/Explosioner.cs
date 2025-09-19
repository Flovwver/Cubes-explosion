using UnityEngine;
using System;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 5f;
    [SerializeField] private float _baseExplosionRadius = 2f;

    public void Explode(CubeBehaviour parentCube)
    {
        if (parentCube == null)
            throw new ArgumentNullException(nameof(parentCube));

        var explosionForce = _baseExplosionForce / parentCube.transform.localScale.x;
        var explosionRadius = _baseExplosionRadius / parentCube.transform.localScale.x;
        var explosionCenter = parentCube.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

        foreach (var collider in colliders)
            if (collider.transform.TryGetComponent<CubeBehaviour>(out var cube))
                cube.Rigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius); 
    }
}
