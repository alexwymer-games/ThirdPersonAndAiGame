using UnityEngine;

public class DoubleDoorsController : MonoBehaviour
{
    //Variables
    [SerializeField] private bool b_DoorOpening;
    [SerializeField] private DoorState doorState;
    [SerializeField] private DoorStatus doorStatus = DoorStatus.LOCKED;

    //Game Objects 
    [SerializeField] private GameObject doorLeftObject;
    [SerializeField] private GameObject doorRightObject;

    [SerializeField] private float changeInPosition = 2.5f;

    private float openDoorLeftPositionX;
    private float openDoorRightPositionX;

    private float closeDoorLeftPositionX;
    private float closeDoorRightPositionX;
     
    [SerializeField] private float openSpeed;

    private void Start()
    {
        openDoorLeftPositionX = doorLeftObject.transform.position.x - changeInPosition;
        openDoorRightPositionX = doorRightObject.transform.position.x + changeInPosition;

        closeDoorLeftPositionX = doorLeftObject.transform.position.x;
        closeDoorRightPositionX = doorRightObject.transform.position.x;
    }


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenDoor();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            CloseDoor();
        }

        HandleDoorMovement();

        
    }


    public void HandleDoorMovement()
    {
        Vector3 currentLeftPosition = doorLeftObject.transform.position;
        Vector3 currentRightPosition = doorRightObject.transform.position;

        if (b_DoorOpening)
        {
           doorLeftObject.transform.position = Vector3.Lerp(currentLeftPosition, new Vector3(openDoorLeftPositionX, currentLeftPosition.y, currentLeftPosition.z) , openSpeed * Time.deltaTime);
           doorRightObject.transform.position = Vector3.Lerp(currentRightPosition, new Vector3(openDoorRightPositionX, currentRightPosition.y, currentRightPosition.z), openSpeed * Time.deltaTime);
        }
        else
        {
            doorLeftObject.transform.position = Vector3.Lerp(currentLeftPosition, new Vector3(closeDoorLeftPositionX, currentLeftPosition.y, currentLeftPosition.z), openSpeed * Time.deltaTime);
            doorRightObject.transform.position = Vector3.Lerp(currentRightPosition, new Vector3(closeDoorRightPositionX, currentRightPosition.y, currentRightPosition.z), openSpeed * Time.deltaTime);
        }
    }


    public void OpenDoor()
    {
        b_DoorOpening = true;
    }

    public void CloseDoor()
    {
        b_DoorOpening = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && doorStatus == DoorStatus.LOCKED)
        {

            other.gameObject.GetComponent<PlayerCharacterActions>().SetDoorInteraction(this);
            EventManager.TriggerEvent(EventType.DOOR_INRANGE, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && doorStatus == DoorStatus.LOCKED)
        {
            other.gameObject.GetComponent<PlayerCharacterActions>().SetDoorInteraction(null);
            EventManager.TriggerEvent(EventType.DOOR_OUTRANGE, false);
        }
    }
}
