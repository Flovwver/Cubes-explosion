using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField]
    private float _splitChance = 1f;

    [Range(0f, 1f)]
    [SerializeField]
    private float _splitChanceFactor = 0.5f;

    [Range(0.01f, 1f)]
    [SerializeField]
    private float _scaleFactor = 0.5f;

    [SerializeField]
    private Rigidbody _rigidbody;

    public float SplitChance
    {
        get => _splitChance;
        set => _splitChance = Mathf.Clamp01(value);
    }

    public float SplitChanceFactor
    {
        get => _splitChanceFactor;
        set => _splitChanceFactor = Mathf.Clamp01(value);
    }

    public float ScaleFactor
    {
        get => _scaleFactor;
        set => _scaleFactor = Mathf.Clamp(value, 0.01f, 1f);
    }

    public Rigidbody Rigidbody
    {
        get
        {
            if (_rigidbody == null)
                _rigidbody = GetComponent<Rigidbody>();
            return _rigidbody;
        }
    }
}
