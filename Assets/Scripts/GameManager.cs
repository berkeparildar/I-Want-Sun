using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] planetPrefabs;
    [SerializeField] private bool gameOver;
    [SerializeField] private int score;
    [SerializeField] private TextMeshProUGUI scoreText;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        scoreText.text = score.ToString();
    }

    public GameObject GetPlanetAtIndex(int index)
    {
        return planetPrefabs[index];
    }

    public void LimitHit()
    {
        gameOver = true;
        
    }

    public bool GetGameState()
    {
        return gameOver;
    }

    public void IncreaseScore(int s)
    {
        score += s;
    }
    
}
