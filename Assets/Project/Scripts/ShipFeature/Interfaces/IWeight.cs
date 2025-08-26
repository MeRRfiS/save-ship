using System;
using UnityEngine;

public interface IWeight
{
    public float Weight { get; }

    public event Action<IWeight> OnWeightChanged;
}
