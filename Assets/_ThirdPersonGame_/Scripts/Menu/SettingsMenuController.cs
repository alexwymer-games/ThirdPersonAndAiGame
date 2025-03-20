using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI toggleMusicText;
    [SerializeField] private TextMeshProUGUI toggleSFXText;

    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        SetToggleMusicButtonText();
        SetToggleSFXButtonText();
    }


    public void UpdateSliderValues()
    {
        musicSlider.value = AudioManager.audioManagerInstance.GetMusicVolume();
        sfxSlider.value = AudioManager.audioManagerInstance.GetSFXVolume();
    }


    public void ToggleMusic()
    {
        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);

        AudioManager.audioManagerInstance.ToggleMusic();

        SetToggleMusicButtonText();
    }

    public void ToggleSFX()
    {
        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);

        AudioManager.audioManagerInstance.ToggleSFX();

        SetToggleSFXButtonText();
    }



    public void ChangeMusicVolume()
    {
        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);

        AudioManager.audioManagerInstance.ChangeMusicVolume(musicSlider.value);
    }

    public void ChangeSFXVolume()
    {
        //AudioManager.audioManagerInstance.PlaySFX(SoundType.UI_SELECT);

        AudioManager.audioManagerInstance.ChangeSFXVolume(sfxSlider.value);
    }


    private void SetToggleMusicButtonText()
    {
        bool muted = AudioManager.audioManagerInstance.musicSource.mute;

        if (muted)
        {
            toggleMusicText.text = "Music: Off";
        }
        else
        {
            toggleMusicText.text = "Music: On";
        }
    }

    private void SetToggleSFXButtonText()
    {
        bool muted = AudioManager.audioManagerInstance.sfxSource.mute;

        if (muted)
        {
            toggleSFXText.text = "SFX: Off";
        }
        else
        {
            toggleSFXText.text = "SFX: On";
        }
    }
}
