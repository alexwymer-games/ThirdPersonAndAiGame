using Unity.AI.Navigation.Samples;
using UnityEngine;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject settingsMenuCanvas;
    [SerializeField] private bool b_gameIsPaused = false;

    [Header("UI Components")]
    [SerializeField] private GameObject settingsButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Pause/Unpause Functions
    public void TogglePause()
    {
        if (!b_gameIsPaused) 
        {
            PauseGame();
           
        }
        else
        {
            ResumeGame();
        }
    }
    private void PauseGame()
    {
        pauseMenuCanvas.SetActive(true);
        b_gameIsPaused = true;
        Time.timeScale = 0.0f;

        

    }
    private void ResumeGame()
    {
        pauseMenuCanvas.SetActive(false);
        b_gameIsPaused = false;
        Time.timeScale = 1.0f;
        
    }


    //Button Functions
    public void ResumeGameButtonPressed()
    {
        ResumeGame();
    }

    public void SettingsButtonPressed()
    {
        //Show Settings Screen
        pauseMenuCanvas.SetActive(false);
        settingsMenuCanvas.SetActive(true);


        //Update Volume Values
        //audioController.UpdateSliderValues();

        //Set Selected Object to Toggle Music Button
        //EventSystem.current.SetSelectedGameObject(toggleMusicButton);
    }

    public void MainMenuButtonPressed()
    {

        //Play Time and Exit to StartMenu
        Time.timeScale = 1.0f;
        SceneNavManager.sceneNavManagerInstance.LoadStartScene();
    }


    public void ShowPauseScreen()
    {
        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);

        //Show Pause Screen
        pauseMenuCanvas.SetActive(true);
        settingsMenuCanvas.SetActive(false);

        //Set Selected Object to Null
        EventSystem.current.SetSelectedGameObject(settingsButton);
    }


    private void BackButtonPressed()
    {
        if (!pauseMenuCanvas.activeSelf)
        {
            //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_BACK);
            ShowPauseScreen();
        }
        else
        {
            Debug.Log("Start Menu Active Already");
        }
    }

}
