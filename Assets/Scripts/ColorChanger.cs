using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private void Start()
    {
        var renderer = GetComponent<Renderer>();
        renderer.material.SetColor("_EmissionColor", new(Random.value, Random.value, Random.value));
    }
}
