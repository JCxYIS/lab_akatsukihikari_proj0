using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour
{
    [SerializeField] Text _scoreText;
    [SerializeField] Text _hpText;

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        _scoreText.text = "Score: "+GameManager.Score;
        _hpText.text = "HP: "+GameManager.Hp;
    }
}