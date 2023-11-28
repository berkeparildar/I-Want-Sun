using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private GameObject planetBasket;
    [SerializeField] private GameObject sun;
    [SerializeField] private bool gameOver;
    [SerializeField] private int score;
    [SerializeField] private int sunCount;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI sunCountText;
    [SerializeField] private GameObject endScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private PlanetGun planetGun;
    [SerializeField] private AudioSource musicAudio;
    [SerializeField] private Image musicIconRenderer;
    [SerializeField] private Image audioIconRenderer;
    [SerializeField] private Sprite audioOnIcon;
    [SerializeField] private Sprite audioOffIcon;
    [SerializeField] private Sprite musicOnIcon;
    [SerializeField] private Sprite musicOffIcon;
    [SerializeField] private TextMeshProUGUI highScoreTextPause;
    [SerializeField] private TextMeshProUGUI sunCountTextPause;
    [SerializeField] private TextMeshProUGUI highScoreTextEnd;
    [SerializeField] private TextMeshProUGUI sunCountTextEnd;
    [SerializeField] private TextMeshProUGUI scoreTextEnd;

    private void Update()
    {
        scoreText.text = score.ToString();
        sunCountText.text = sunCount.ToString();
    }

    private IEnumerator RemovePlanets()
    {
        Debug.Log("disabling physics");
        DisablePhysics();
        while (planetBasket.transform.childCount > 0)
        {
            planetBasket.transform.GetChild(0).GetComponent<Planet>().DestroyAfterGame();
            yield return new WaitForSeconds(0.2f);
        }
    }
    
    public void EndLevel()
    {
        gameOver = true;
        StartCoroutine(RemovePlanets());
        DOTween.KillAll();
        Destroy(planetGun.gameObject);
        Debug.Log("Running limit hit.");
        var currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        var currentSunCount = PlayerPrefs.GetInt("SunCount", 0);
        PlayerPrefs.SetInt("SunCount", currentSunCount + sunCount);
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
        highScoreTextEnd.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        sunCountTextEnd.text = PlayerPrefs.GetInt("SunCount", sunCount).ToString();
        scoreTextEnd.text = score.ToString();
        endScreen.SetActive(true);
    }

    public GameObject GetPlanetAtIndex(int index)
    {
        if (index == planetPrefabs.Length)
        {
            sunCount++;
            return sun;
        }
        return planetPrefabs[index];
    }
    
    public bool GetGameState()
    {
        return gameOver;
    }

    public void IncreaseScore(int s)
    {
        score += s;
    }

    public void ShowPauseScreen()
    {
        pauseScreen.SetActive(true);
        planetGun.GamePaused();
        highScoreTextPause.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
        sunCountTextPause.text = PlayerPrefs.GetInt("SunCount", 0).ToString();
    }

    public void ClosePauseScreen()
    {
        pauseScreen.SetActive(false);
        planetGun.GamePaused();
    }

    public void MuteAudio()
    {
        var pause = AudioListener.pause;
        pause = !pause;
        AudioListener.pause = pause;
        audioIconRenderer.sprite = pause ? audioOffIcon : audioOnIcon;
    }

    public void MuteMusic()
    {
        musicAudio.mute = !musicAudio.mute;
        musicIconRenderer.sprite = musicAudio.mute ? musicOffIcon : musicOnIcon;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void DisablePhysics()
    {
        for (int i = 0; i < planetBasket.transform.childCount; i++)
        {
            var currentChild = planetBasket.transform.GetChild(i);
            var childRb = currentChild.GetComponent<Rigidbody2D>();
            childRb.velocity = Vector2.zero;
            childRb.freezeRotation = true;
            childRb.isKinematic = true;
        }
    }
}
