using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static Vector3 player1Pos = new Vector3(-6f, -2.49f, 0);
    private static Vector3 player2Pos = new Vector3(6f, -2.49f, 0);
    private static int newParentPlayer;
    public static int ballHolder;
    public static int scoreOne = 0;
    public static int scoreTwo = 0;
    public static float timeLeft;
    [SerializeField] private Text scoreOneText;
    [SerializeField] private Text scoreTwoText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private static float scoreTimeout = 0.5f;
    public static float waitTime;
    public static bool gameStarted = false;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private AudioMixer musicMixer;
    [SerializeField] private Slider volume;
    [SerializeField] private AudioMixer sfxMixer;
    [SerializeField] private static Score scoreObj;
    private bool musicEnabled = true;
    private bool sfxEnabled = true;

    [SerializeField] private GameObject characterSelectPanel;
    [SerializeField] private GameObject mainUIPanel;
    [SerializeField] private ToggleGroup maskToggleGroup;
    [SerializeField] private ToggleGroup noMaskToggleGroup;

    private void Start()
    {
        scoreOne = 0;
        scoreTwo = 0;
        mainUIPanel.SetActive(false);
        characterSelectPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        pauseMenu.SetActive(false);
        GameObject.Find("Player1").GetComponent<Movement>().enabled = false;
        GameObject.Find("Player2").GetComponent<Movement>().enabled = false;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().simulated = false;
        Time.timeScale = 1;
        scoreObj = GameObject.Find("Goal1").GetComponent<Score>();
    }

    void Update()
    {
        scoreOneText.text = scoreOne.ToString();
        scoreTwoText.text = scoreTwo.ToString();
        timeText.text = timeLeft <= 0 ? "0.0" : timeLeft.ToString("F1");
        timeLeft -= timeLeft <= 0 && gameStarted ? 0 : Time.deltaTime;
        if (timeLeft <= 0 && gameStarted)
        {
            scoreObj.GameEnd(gameOverPanel, scoreOne, scoreTwo);
        }

        if (Input.GetButtonDown("Pause"))
        {
            pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
            Time.timeScale = pauseMenu.activeInHierarchy ? 0 : 1;
        }
    }

    public static void ChangeScore(int team, int score)
    {
        newParentPlayer = team;
        waitTime = scoreTimeout;
        if (team == 1)
        {
            scoreTwo += score;
        }
        else
        {
            scoreOne += score;
        }
        scoreObj.DisplayText(score + " point!", player1Pos, player2Pos, newParentPlayer);
    }
    
    public IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(2);
        GameObject.Find("Player1").transform.position = player1Pos;
        GameObject.Find("Player2").transform.position = player2Pos;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject.Find("Player1").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Player2").GetComponent<Movement>().jumpedWithBall = false;
        GameObject.Find("Ball").transform.GetChild(0).GetComponent<Ball>().ParentBall(GameObject.Find("Player" + newParentPlayer).transform);
    }

    private void FixedUpdate()
    {
        waitTime -= waitTime <= 0f ? 0 : Time.deltaTime;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
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

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        GameObject.Find("Player1").transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = maskToggleGroup.GetFirstActiveToggle().gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        GameObject.Find("Player2").transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = noMaskToggleGroup.GetFirstActiveToggle().gameObject.transform.GetChild(0).GetComponent<Image>().sprite;
        timeLeft = 60;
        gameStarted = true;
        mainUIPanel.SetActive(true);
        characterSelectPanel.SetActive(false);
        GameObject.Find("Player1").GetComponent<Movement>().enabled = true;
        GameObject.Find("Player2").GetComponent<Movement>().enabled = true;
        GameObject.Find("Ball").GetComponent<Rigidbody2D>().simulated = true;
    }
}
