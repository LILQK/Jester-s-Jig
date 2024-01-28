using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int score = 100;

    public Slider scoreSlider;

    private int row = 0;
    [SerializeField]
    private TMPro.TextMeshPro rowText;

    [SerializeField]
    private ParticleSystem rowParticles;

    public int player = 0;

    private void Update()
    {
        scoreSlider.value = score;
        if (score <= 0) {
            GameManager.instance.EndGame(player);
        }
        rowText.text = row.ToString();
        
    }

    public void AddScore()
    {
        score++;
        row++;
        if (row > 5)
        {
            rowText.enabled = true;
        }
        if (row > 10) {
            rowParticles.Play();
        }
    }

    public void RemoveScore()
    {
        score--;
        row = 0;
        rowText.enabled = false;
        rowParticles.Stop();

    }
}
