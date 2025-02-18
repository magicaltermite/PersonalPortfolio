using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AI.StateMachine {
public class BaseStateMachine : MonoBehaviour {
    public BaseState CurrentState { get; set; }
    
    [SerializeField] private BaseState initialStateMachine;
    private Dictionary<Type, Component> cachedComponents;

    private void Awake() {
        CurrentState = initialStateMachine;
        cachedComponents = new Dictionary<Type, Component>();
    }

    private void Update() {
        CurrentState.Execute(this);
    }

    public new T GetComponent<T>() where T : Component {
        if (cachedComponents.ContainsKey(typeof(T))) return cachedComponents[typeof(T)] as T;

        var component = base.GetComponent<T>();
        if (component != null) cachedComponents.Add(typeof(T), component);
        return component;
    }
}
}