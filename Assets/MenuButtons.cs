using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject creditsPanel;

    private void Start()
    {
        infoPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
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
        infoPanel.SetActive(!settingsPanel.activeInHierarchy);
        settingsPanel.SetActive(!settingsPanel.activeInHierarchy);
        creditsPanel.SetActive(false);
    }

    public void Credits()
    {
        infoPanel.SetActive(!creditsPanel.activeInHierarchy);
        creditsPanel.SetActive(!creditsPanel.activeInHierarchy);
        settingsPanel.SetActive(false);
    }
}
