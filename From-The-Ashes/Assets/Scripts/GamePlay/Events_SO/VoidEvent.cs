using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Void Event")]
public class VoidEvent: ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
