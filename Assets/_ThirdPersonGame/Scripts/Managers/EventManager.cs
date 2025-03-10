using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    
}

public class EventManager : MonoBehaviour
{
    //Event Dictionary - List of all events and listeners 
    private Dictionary<EventType, UnityEventBase> eventDictionary;

    //Singleton Setup 
    private static EventManager eventManager;
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                // eventManager = FindAnyObjectByType(typeof(EventManager)) as EventManager;
                eventManager = Object.FindFirstObjectByType<EventManager>();

                if (!eventManager)
                {
                    Debug.LogError("There must be one active EventManager script on a GameObject in your scene");
                }
                else
                {
                    eventManager.Init();
                }
            }
            return eventManager;
        }
    }

    //Init Func - Sets up Event Dictionary 
    private void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<EventType, UnityEventBase>();
        }
    }

    #region START_LISTENING 

    //Start Listening 1) - Registers a listener for an event with a parameter of type T
    public static void StartListening<T>(EventType eventType, UnityAction<T> listener)
    {
        UnityEvent<T> thisEvent = null;

        //If event exists, add the listener
        if (instance.eventDictionary.TryGetValue(eventType, out var baseEvent))
        {
            thisEvent = baseEvent as UnityEvent<T>;
        }
        else // If event does not exist, create a new event and add listen, add to dictionary 
        {
            thisEvent = new UnityEvent<T>();
            instance.eventDictionary.Add(eventType, thisEvent);
        }

        //Add Listener 
        if (thisEvent != null)
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            Debug.LogError($"Event Type Mismatch for {eventType}. Expected UnityEvent<{typeof(T)}>");
        }
    }

    //Start Listening 2) - Registers a listener for an event with no parameters 
    public static void StartListening(EventType eventType, UnityAction listener)
    {
        UnityEvent thisEvent = null;

        // Check if the event already exists
        if (instance.eventDictionary.TryGetValue(eventType, out var baseEvent))
        {
            thisEvent = baseEvent as UnityEvent;
        }
        else
        {
            // Create a new UnityEvent and add it to the dictionary
            thisEvent = new UnityEvent();
            instance.eventDictionary.Add(eventType, thisEvent);
        }

        // Add the listener to the event
        if (thisEvent != null)
        {
            thisEvent.AddListener(listener);
        }
    }

    #endregion

    #region STOP_LISTENING

    //Stop Listening 1) - Unregisters a listener for an event with a parameters of type T
    public static void StopListening<T>(EventType eventType, UnityAction<T> listener)
    {

        if (eventManager == null)
        {
            return; //If no EventManager, exit early 
        }

        //Check if event exists in EventDictionary 
        if (instance.eventDictionary.TryGetValue(eventType, out var baseEvent))
        {
            //Cast and Check value
            var thisEvent = baseEvent as UnityEvent<T>;
            if (thisEvent != null)
            {
                thisEvent.RemoveListener(listener); //Remove Listener from the Event
            }
            else
            {
                Debug.LogError($"Event Type Mismatch for {eventType}. Expected UnityEvent<{typeof(T)}>");
            }
        }
    }

    //Stop Listening 2) - Unregister a listener for an event with no parameters
    public static void StopListening(EventType eventType, UnityAction listener)
    {
        if (eventManager == null) return;

        // Try to find the event in the dictionary
        if (instance.eventDictionary.TryGetValue(eventType, out var baseEvent))
        {
            var thisEvent = baseEvent as UnityEvent;
            if (thisEvent != null)
            {
                thisEvent.RemoveListener(listener);
            }
        }
    }

    #endregion

    #region TRIGGER_EVENT

    //Trigger Event 1) - Triggers an Event with parameter of type T. Passing data to all registered listeners 
    public static void TriggerEvent<T>(EventType eventType, T parameter)
    {
        //Check if the triggered event exists in the eventDictionary 
        if (instance.eventDictionary.TryGetValue(eventType, out var baseEvent))
        {
            //Cast event and check value
            var thisEvent = baseEvent as UnityEvent<T>;
            if (thisEvent != null)
            {
                thisEvent.Invoke(parameter); //Invoke event and pass parameter
            }
            else
            {
                Debug.LogError($"Event Type Mismatch for {eventType}. Expected UnityEvent<{typeof(T)}>");
            }
        }
    }

    // Trigger Event 2) - Triggers Event with No parameters
    public static void TriggerEvent(EventType eventType)
    {
        // Try to find the event in the dictionary
        if (instance.eventDictionary.TryGetValue(eventType, out var baseEvent))
        {
            var thisEvent = baseEvent as UnityEvent;
            if (thisEvent != null)
            {
                thisEvent.Invoke();
            }
        }
    }

    #endregion
}



//How to setup Broadcasting and Listening to Events 

//Broadcasting 
//EventManager.TriggerEvent<T>(EventType.type, val);

//Listening 
//EventManager.StartListening<T>(EventType.event, OnEvent);
//EventManager.StopListening<T>(EventType.event, OnEvent);

//void OnEvent(T val)