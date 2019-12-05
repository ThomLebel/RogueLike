using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EventManager : MonoBehaviour
{
	[SerializeField] private Dictionary<string, List<CustomComponent>> listeners;

    // Start is called before the first frame update
    void Start()
    {
		listeners = new Dictionary<string, List<CustomComponent>>();
		CustomComponent[] customComponentsArray = GetComponents<CustomComponent>();
		foreach(CustomComponent component in customComponentsArray)
		{
			AddRemoveToDictionary(component);
		}
    }
	
	public void ReceiveEvent(EventManager sender, CustomEvent e)
	{
		ProcessEvent(e);
	}

	public void SendEvent(EventManager target, CustomEvent e)
	{
		if (target == this)
		{
			ProcessEvent(e);
		}
		else
		{
			target.ReceiveEvent(this, e);
		}
	}

	private void ProcessEvent(CustomEvent e)
	{
		//Comparer les key de data de l'event avec les key dans le listeners dictionary
		//Pour chaque composant écoutant un event envoyé, lui transmettre le nom de l'event ainsi que sa value contenu dans data
		//Utilisez un event queue pour les composants réagissant au même event
		//surement nécessaire d'utiliser un système de priorité pour l'event queue
		//+ créer une fonction ProcessEvent dans les component prenant en paramètre le nom de l'event ainsi que sa valeur (contenu dans data -> string, object)

		IEnumerable<string> events = e.data.Keys.Intersect(listeners.Keys);
		foreach(string id in events)
		{
			List<CustomComponent> tempComponentArray = listeners[id];
			for (int i = 0; i < tempComponentArray.Count; i++)
			{
				tempComponentArray[i].ProcessEvent(id, e.data[id]);
			}
		}
	}

	public void AddRemoveToDictionary(CustomComponent component, bool add = true)
	{
		if (component.reactions.Length <= 0)
		{
			return;
		}

		for (int i = 0; i < component.reactions.Length; i++)
		{
			string key = component.reactions[i];
			Debug.Log(key);
			//Is the reaction already present in the dictionary
			if (listeners.ContainsKey(key))
			{
				List<CustomComponent> tempComponentArray = listeners[key];
				//If yes, check if the component is registered for this reaction
				if (tempComponentArray.Count > 0)
				{
					//If he already is, do nothing
					if (tempComponentArray.Contains(component))
					{
						if (add)
						{
							Debug.Log("component already registered for this reaction");
						}
						else
						{
							tempComponentArray.Remove(component);
							if (tempComponentArray.Count <= 0)
							{
								listeners.Remove(key);
							}
						}
					}
					//Else add the component to the list
					else
					{
						if (add)
						{
							tempComponentArray.Add(component);
						}
					}
				}
				else
				{
					if (!add)
					{
						listeners.Remove(key);
					}
				}
			}
			//If not create the new pair key/value
			else
			{
				if (add)
				{
					listeners.Add(key, new List<CustomComponent>() { component });
				}
			}
		}
	}
}
