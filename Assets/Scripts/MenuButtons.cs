using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    [SerializeField] private Dropdown graphicsQuality;
    [SerializeField] private Toggle music;
    [SerializeField] private Toggle sfx;
    [SerializeField] private Slider volume;

    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private AudioMixer sfxMixer;
    
    private bool musicEnabled = true;
    private bool sfxEnabled = true;
    
    private void Start()
    {
        settingsPanel.SetActive(false);
        graphicsQuality.value = QualitySettings.GetQualityLevel();
        volume.value = 0;
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Settings()
    {
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void MusicEnabled(bool enabled)
    {
        musicEnabled = enabled;
        musicMixer.SetFloat("volume", enabled ? volume.value : -80);
    }

    public void SfxEnabled(bool enabled)
    {
        sfxEnabled = enabled;
        sfxMixer.SetFloat("volume", enabled ? volume.value : -80);
    }

    public void SetVolume(float vol)
    {
        musicMixer.SetFloat("volume", musicEnabled ? vol : -80);
        sfxMixer.SetFloat("volume", sfxEnabled ? vol : -80);
    }
}
