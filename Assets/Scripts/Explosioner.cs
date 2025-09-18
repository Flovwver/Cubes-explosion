using System;
using System.Collections.Generic;
using UnityEngine;

public class Explosioner : MonoBehaviour
{
    [SerializeField] private float _explosionForce = 5f;
    [SerializeField] private float _explosionRadius = 2f;

    public void Explode(IEnumerable<Rigidbody> bodies, Vector3 explosionCenter)
    {
        if (bodies == null)
            throw new ArgumentNullException(nameof(bodies));

        foreach (var body in bodies)
        {
            body.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius);
        }
    }
}
