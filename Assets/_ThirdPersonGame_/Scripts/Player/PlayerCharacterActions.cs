using UnityEngine;

public class PlayerCharacterActions : MonoBehaviour
{
    //Game Controls
    private GameControls gameControls;
    private GameControls.PlayerActions playerActions;

    //Components
    private PlayerCharacterController playerCharacterController;


    //Door 
    private bool b_isRangeOfDoor;
    private DoubleDoorsController currentDoorController;


    private void Awake()
    {
        
    }

    private void OnEnable()
    {
        //Enable Controls and Assign Functions 

        //Subscribe to Events
        EventManager.StartListening<DoorState>(EventType.DOOR_INRANGE, DoorInRange);
        EventManager.StartListening(EventType.DOOR_OUTRANGE, DoorOutRange);
    }

    private void OnDisable()
    {
        //Disable Controls and Assign Functions 

        //Unsubscribe to Events
        EventManager.StopListening<DoorState>(EventType.DOOR_INRANGE, DoorInRange);
        EventManager.StopListening(EventType.DOOR_OUTRANGE, DoorOutRange);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCharacterController = GetComponent<PlayerCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Door Functions 
    public void SetDoorInteraction(DoubleDoorsController doorController)
    {
        currentDoorController = doorController;
    }

    public void InteractWithDoor()
    {
        if (b_isRangeOfDoor)
        {
            //Trigger Event and Pass Door 
            EventManager.TriggerEvent(EventType.DOOR_INTERACT, currentDoorController.doorState);

            currentDoorController.ToggleDoor();
        }
    }


    //Getters and Setters
   
    public void DoorInRange(DoorState doorState)
    {
        b_isRangeOfDoor = true;
        playerCharacterController.playerState = PlayerState.INTERACT;
    }

    public void DoorOutRange()
    {
        b_isRangeOfDoor = false;
        playerCharacterController.playerState = PlayerState.IDLE;
    }
}
