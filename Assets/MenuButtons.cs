using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    bool settingsShown;

    private void Start()
    {
        HideSettings();
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
        if (settingsShown)
        {
            HideSettings();
        }
        else
        {
            ShowSettings();
        }
    }

    private void ShowSettings()
    {

        settingsShown = true;
    }

    private void HideSettings()
    {

        settingsShown = false;
    }
}
