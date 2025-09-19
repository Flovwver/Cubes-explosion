using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce = 5f;
    [SerializeField] private float _baseExplosionRadius = 2f;

    public void Explode(IEnumerable<Rigidbody> bodies, Vector3 explosionCenter)
    {
        if (bodies == null)
            throw new ArgumentNullException(nameof(bodies));

        foreach (var body in bodies)
        {
            body.AddExplosionForce(_baseExplosionForce, explosionCenter, _baseExplosionRadius);
        }
    }

    public void Explode(Cube parentCube)
    {
        if (parentCube == null)
            throw new ArgumentNullException(nameof(parentCube));

        var explosionForce = _baseExplosionForce / parentCube.transform.localScale.x;
        var explosionRadius = _baseExplosionRadius / parentCube.transform.localScale.x;
        var explosionCenter = parentCube.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionCenter, explosionRadius);

        foreach (var collider in colliders)
            if (collider.transform.TryGetComponent<Cube>(out var cube))
                cube.Rigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius); 
    }
}
