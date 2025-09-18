using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public const int MouseButtonLeft = 0;

    [SerializeField] private Camera _camera;

    public event Action<Ray> PointerClicked;

    private void Update()
    {
        if (Input.GetMouseButtonDown(MouseButtonLeft))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            PointerClicked?.Invoke(ray);
        }
    }
}