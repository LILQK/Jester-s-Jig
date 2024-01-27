using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitPlayersReady : MonoBehaviour
{
    private bool player1ready = false;
    private bool player2ready = false;

    [SerializeField]
    private TMPro.TMP_Text player1text;
    [SerializeField]
    private TMPro.TMP_Text player2text;

    [SerializeField]
    Transform gameStarting;

    [SerializeField]
    private TMPro.TMP_Text timeText;

    [SerializeField]
    private Transform waitForPlayer;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        waitForPlayer.gameObject.SetActive(true);
        gameStarting.gameObject.SetActive(false);
        timer = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!(player1ready && player2ready))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                player1ready = true;
                player1text.text = "READY";
                player1text.color = Color.green;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                player2ready = true;
                player2text.text = "READY";
                player2text.color = Color.green;
            }
        }
        else {
            waitForPlayer.gameObject.SetActive(false);
            gameStarting.gameObject.SetActive(true);
            timer -= Time.deltaTime;
            timeText.text = timer.ToString("F0");
            if (timer <= 0) {
                GameManager.instance.gameStarted = true;
                Destroy(gameObject);
            }
        }
    }
}
