using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    [SerializeField] private float _maxRayDistance = 10f;
    [SerializeField] private InputReader _inputReader;

    public event Action<CubeBehaviour> CubeHit;

    private void OnEnable()
    {
        if (_inputReader != null)
            _inputReader.PointerClicked += OnPointerClicked;
    }

    private void OnDisable()
    {
        if (_inputReader != null)
            _inputReader.PointerClicked -= OnPointerClicked;
    }

    private void OnPointerClicked(Ray ray)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, _maxRayDistance))
        {
            GameObject hitObject = hit.collider.gameObject;
            
            if (hitObject.TryGetComponent<CubeBehaviour>(out var cubeBehaviour))
            {
                CubeHit?.Invoke(cubeBehaviour);
                cubeBehaviour.NotifyClicked();
            }
        }
    }
}
