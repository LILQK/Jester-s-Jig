using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int score = 100;

    public Slider scoreSlider;
    private void Update()
    {
        scoreSlider.value = score;
        if (score <= 0) {
            GameManager.instance.EndGame();
        }
    }
}
