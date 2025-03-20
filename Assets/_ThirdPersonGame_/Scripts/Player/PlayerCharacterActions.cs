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
        EventManager.StartListening<bool>(EventType.DOOR_INRANGE, SetInRangeOfDoor);
        EventManager.StartListening<bool>(EventType.DOOR_OUTRANGE, SetInRangeOfDoor);
    }

    private void OnDisable()
    {
        //Disable Controls and Assign Functions 

        //Unsubscribe to Events
        EventManager.StopListening<bool>(EventType.DOOR_INRANGE, SetInRangeOfDoor);
        EventManager.StopListening<bool>(EventType.DOOR_OUTRANGE, SetInRangeOfDoor);
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
            //EventManager.TriggerEvent(EventType.DOOR_INTERACT, currentDoorController);

            currentDoorController.OpenDoor();
        }
    }


    //Getters and Setters
    public void SetInRangeOfDoor(bool inRange)
    {
        b_isRangeOfDoor = inRange;

        if (b_isRangeOfDoor)
        {
            playerCharacterController.playerState = PlayerState.INTERACT;
        }
        else
        {
            playerCharacterController.playerState = PlayerState.IDLE;
        }
    }

    

    
}
