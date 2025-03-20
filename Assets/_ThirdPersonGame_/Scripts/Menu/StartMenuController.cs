using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StartMenuController : MonoBehaviour
{
    //Controls - UI Input 
    private GameControls gameControls;
    private GameControls.UserInterfaceActions userInterfaceActions;

    //Menu Canvases
    [Header("Menu Canvases")]
    [SerializeField] private GameObject startMenuCanvas;
    [SerializeField] private GameObject instructionsMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private GameObject levelSelectorMenuCanvas;

    [Header("Main Menu Buttons")]
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject instructionsButton;
    [SerializeField] private GameObject settingsButton;

    [Header("Other Menu Buttons")]
    [SerializeField] private GameObject toggleButton;
    [SerializeField] private GameObject levelOneButton;

    [SerializeField] private GameObject lastButtonInteractedWith;

    private SettingsMenuController settingsMenuController;


    #region LIFECYCLE

    private void Awake()
    {
        //Setup Game Controls 
        gameControls = new GameControls();
        userInterfaceActions = gameControls.UserInterface;


    }

    private void OnEnable()
    {
        //Enable Controls and Assign Functions 
        userInterfaceActions.Enable();
        userInterfaceActions.Back.performed += ctx => BackButtonPressed();
    }

    private void OnDisable()
    {
        //Disable Controls and Unassign Functions 
        userInterfaceActions.Disable();
        userInterfaceActions.Back.performed -= ctx => BackButtonPressed();
    }


    void Start()
    {
        settingsMenuController = GameObject.FindAnyObjectByType<SettingsMenuController>();

        lastButtonInteractedWith = startButton;
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        ShowStartCanvas();
    }

    #endregion

    //SHOW CANVAS FUNCTIONS 

    public void ShowStartCanvas()
    {
        startMenuCanvas.SetActive(true);
        instructionsMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        levelSelectorMenuCanvas.SetActive(false);

        //Set Selected Button 
        EventSystem.current.SetSelectedGameObject(startButton);

        lastButtonInteractedWith = instructionsButton;

        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);
    }


    public void ShowInstructionsCanvas() 
    {
        startMenuCanvas.SetActive(false);
        instructionsMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);
        levelSelectorMenuCanvas.SetActive(false);

        //Set Selected Button 
        //EventSystem.current.SetSelectedGameObject(startButton);

        lastButtonInteractedWith = instructionsButton;

        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);
    }

    public void ShowSettingsCanvas() 
    {

        startMenuCanvas.SetActive(false);
        instructionsMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);
        levelSelectorMenuCanvas.SetActive(false);

        //Set Selected Button 
        EventSystem.current.SetSelectedGameObject(toggleButton);

        lastButtonInteractedWith = settingsButton;

        settingsMenuController.UpdateSliderValues();

        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);
    }

    public void ShowLevelSelectCanvas()
    {
        startMenuCanvas.SetActive(false);
        instructionsMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(false);
        levelSelectorMenuCanvas.SetActive(true);

        //Set Selected Button 
        EventSystem.current.SetSelectedGameObject(levelOneButton);

        lastButtonInteractedWith = startButton;

        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);
    }

    //QUIT GAME 
    public void QuitButtonPressed()
    {
        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_BACK);

        SceneNavManager.sceneNavManagerInstance.QuitGame();
    }


    //LEVEL LOAD FUNCTION - Loads Specific Game Scene
    public void SetChosenLevel(string levelName)
    {
        SceneNavManager.sceneNavManagerInstance.LoadLevelScene(levelName);
    }


    //INPUT FUNCTIONS 

    private void BackButtonPressed()
    {
        if (!startMenuCanvas.activeSelf)
        {
            //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_BACK);

            ShowStartCanvas();
        }
        else
        {
            Debug.Log("Start Menu Active Already");
        }
    }
}
