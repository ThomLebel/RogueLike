using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_ClassCommunication : MonoBehaviour
{
	public string classToAdd = "ClassTest";
	public bool addComponent;
	public bool removeComponent;
	public bool useComponent;
	public bool sendToOther;
	public EventManager target;

	private EventManager eventManager;

	[SerializeField] private List<Component> componentList;

    // Start is called before the first frame update
    void Start()
    {
		eventManager = GetComponent<EventManager>();
		componentList = new List<Component>();
    }

    // Update is called once per frame
    void Update()
    {
		if (addComponent)
		{
			addComponent = false;
			AddComponent();
		}   
		if(removeComponent)
		{
			removeComponent = false;
			RemoveComponent();
		}
		if(useComponent)
		{
			useComponent = false;
			UseComponent();
		}
		if (sendToOther)
		{
			sendToOther = false;
			SendToOther();
		}
    }

	private void AddComponent()
	{
		Type mType = Type.GetType(classToAdd);
		Component component = gameObject.AddComponent(mType);
		componentList.Add(component);
	}

	private void RemoveComponent()
	{
		Component component = componentList[0];
		componentList.Remove(component);
		Destroy(component);
	}

	private void UseComponent()
	{
		CustomEvent customEvent = new CustomEvent();
		customEvent.id = "Test";
		customEvent.data = new Dictionary<string, object>();
		customEvent.data.Add("test1", 5);
		customEvent.data.Add("test2", new int[1,2,3,4,5]);
		customEvent.data.Add("test3", true);
		eventManager.SendEvent(eventManager, customEvent);
	}

	private void SendToOther()
	{
		CustomEvent customEvent = new CustomEvent();
		customEvent.id = "Test";
		customEvent.data = new Dictionary<string, object>();
		customEvent.data.Add("test1", 1);
		customEvent.data.Add("test2", "the other one");
		customEvent.data.Add("test3", false);
		eventManager.SendEvent(target, customEvent);
	}
}
