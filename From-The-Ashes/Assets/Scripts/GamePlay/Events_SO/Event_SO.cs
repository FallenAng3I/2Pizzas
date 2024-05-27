using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event")]
public class Event_SO: ScriptableObject
{
    public event Action OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
