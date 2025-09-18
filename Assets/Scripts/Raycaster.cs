using System;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Ray _ray;
    [SerializeField] private float _maxRayDistance = 10f;

    public event Action<GameObject> CubeHit;

    private void Update()
    {
        _ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (!Input.GetMouseButtonDown(0)) 
            return;

        if (Physics.Raycast(_ray, out RaycastHit hit, _maxRayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            if (hitObject.TryGetComponent<CubeBehaviour>(out var cubeBehaviour))
            {
                CubeHit?.Invoke(hitObject);
                cubeBehaviour.NotifyClicked();
            }
        }
    }
}
