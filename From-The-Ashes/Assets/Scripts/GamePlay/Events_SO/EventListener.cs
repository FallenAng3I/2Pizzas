using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ListenerModule
{
    [SerializeField] private VoidEvent eventChanel;
    [SerializeField] private UnityEvent listenerEvent;

    public VoidEvent EventChanel { get => eventChanel; }
    public UnityEvent ListenerEvent { get => listenerEvent; }
}

public class EventListener : MonoBehaviour
{
    [SerializeField] private List<ListenerModule> listenerModules;

    private void Awake()
    {
        foreach (ListenerModule module in listenerModules)
        {
            module.EventChanel.OnEventRaised += () => module.ListenerEvent.Invoke();
        }
    }
}
