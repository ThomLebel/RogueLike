using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassTest : CustomComponent
{
    // Start is called before the first frame update
    void Start()
    {
		reactions = new string[]{ "test1", "test2", "test3"};
		eventManager.AddRemoveToDictionary(this);
		Debug.Log("Component start");
    }

	private void OnEnable()
	{
		Debug.Log("Component added !");
	}

	private void OnDisable()
	{
		Debug.Log("Component removed !");
		if (eventManager != null)
		{
			eventManager.AddRemoveToDictionary(this, false);
		}
	}

	public override void ProcessEvent(string id, object value)
	{
		switch (id)
		{
			case "test1":
				Debug.Log(value);
				break;
			case "test2":
				Debug.Log(value);
				break;
			case "test3":
				Debug.Log(value);
				break;
		}
		
	}
}
