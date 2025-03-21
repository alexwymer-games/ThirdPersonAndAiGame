using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameViewController : MonoBehaviour
{

    //UI Components
    [Header("Player UI Components")]
    [SerializeField] private Slider playerHealthSlider;
    [SerializeField] private Slider playerStaminaSlider;

    [SerializeField] private Image stateIcon;
    [SerializeField] private TextMeshProUGUI ammoText;


    [Header("Door UI Components")]
    private DoorState currentDoorState;
    [SerializeField] private GameObject doorInteractTextObject;
    [SerializeField] private TextMeshProUGUI doorInteractText;


    #region LIFECYCLE



    private void OnEnable()
    {
        //Subscribe to Events
        EventManager.StartListening<DoorState>(EventType.DOOR_INRANGE, ShowDoorInteractText);
        EventManager.StartListening(EventType.DOOR_OUTRANGE, HideDoorInteractText);

        EventManager.StartListening<DoorState>(EventType.DOOR_INTERACT, UpdateDoorText);

    }
    private void OnDisable()
    {
        //Unsubscribe to Events
        EventManager.StopListening<DoorState>(EventType.DOOR_INRANGE, ShowDoorInteractText);
        EventManager.StopListening(EventType.DOOR_OUTRANGE, HideDoorInteractText);

        EventManager.StopListening<DoorState>(EventType.DOOR_INTERACT, UpdateDoorText);
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


    private void UpdateDoorText(DoorState _doorState)
    {
        ShowDoorInteractText(_doorState);
    }

    public void ShowDoorInteractText(DoorState _doorState)
    {

        Debug.Log("Update View");

        if (_doorState == DoorState.OPEN)
        {
            doorInteractText.text = "Press 'Interact' to Close";
        }
        else if (_doorState == DoorState.CLOSED)
        {
            doorInteractText.text = "Press 'Interact' to Open";
        }

        doorInteractTextObject.SetActive(true);
    }


    public void HideDoorInteractText()
    {
        doorInteractTextObject.SetActive(false);
    }
}
