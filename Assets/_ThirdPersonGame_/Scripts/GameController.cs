using UnityEngine;

public class GameController : MonoBehaviour
{
    //Game Controls
    private GameControls gameControls;
    private GameControls.UserInterfaceActions userInterfaceActions;

    //Components 
    private PauseMenuController pauseMenuController;



    #region LIFECYCLE

    private void Awake()
    {
        //Setup Controls 
        gameControls = new GameControls();
        userInterfaceActions = gameControls.UserInterface;

        //Get components
        pauseMenuController = GetComponent<PauseMenuController>();

    }

    private void OnEnable()
    {
        //Enable Controls 
        userInterfaceActions.Enable();
        userInterfaceActions.PauseGame.performed += ctx => PauseGame();
        
    }

    private void OnDisable()
    {
        //Disable Controls 
        userInterfaceActions.Disable();
        userInterfaceActions.PauseGame.performed -= ctx => PauseGame();
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


    private void PauseGame()
    {
        pauseMenuController.TogglePause();
    }
}
