using UnityEngine;

public class GameViewController : MonoBehaviour
{

    //UI Components
    [Header("UI Components")]
    [SerializeField] private GameObject doorInteractTextObject;



    #region LIFECYCLE



    private void OnEnable()
    {
        //Subscribe to Events
        EventManager.StartListening<bool>(EventType.DOOR_INRANGE, ToggleDoorInteractText);
        EventManager.StartListening<bool>(EventType.DOOR_OUTRANGE, ToggleDoorInteractText);
    }
    private void OnDisable()
    {
        //Unsubscribe to Events
        EventManager.StopListening<bool>(EventType.DOOR_INRANGE, ToggleDoorInteractText);
        EventManager.StopListening<bool>(EventType.DOOR_OUTRANGE, ToggleDoorInteractText);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion


    public void ToggleDoorInteractText(bool _toggle)
    {
        doorInteractTextObject.SetActive(_toggle);
    }
}
