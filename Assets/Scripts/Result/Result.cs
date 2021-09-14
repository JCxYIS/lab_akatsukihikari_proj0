using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] Text _scoreText;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        _scoreText.text = "Score: "+GameManager.Score;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Game");
    }
}