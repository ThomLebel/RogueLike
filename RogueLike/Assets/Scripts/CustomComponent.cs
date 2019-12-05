using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EventManager))]
public class CustomComponent : MonoBehaviour
{
	[Tooltip("Component name")]
	public int componentID;
	[Tooltip("What is this component reacting to ?")]
	public string[] reactions;

	protected EventManager eventManager;

	protected virtual void Awake()
	{
		eventManager = GetComponent<EventManager>();
	}

	public virtual void ProcessEvent(string id, object value) { }
}
