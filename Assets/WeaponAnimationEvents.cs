using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : UnityEvent<string>
{

}

public class WeaponAnimationEvents : MonoBehaviour
{ 
    public AnimEvent WeaponAnimationEvent = new AnimEvent();
    public void OnAnimationEvent(string eventName)
    {
        WeaponAnimationEvent.Invoke(eventName);
    }
}
