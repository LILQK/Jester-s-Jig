using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private NoteSpawner spawner;

    [SerializeField]
    private NoteSpawner spawner2;

    public float cooldown = 1f;
    float timer = 0f;

    public static GameManager instance;

    public float gameSpeed = 2f;

    public Animator wizard;
    public ParticleSystem wizardParticle;

    public float songDuration = 60f;
    private float gameTimer = 0f;

    public bool gameStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        gameTimer = songDuration;
    }
    void Update()
    {
        if (!gameStarted) return;

        if (gameTimer > 0)
        {
            gameSpeed += Time.deltaTime * .05f;
            if (timer <= 0)
            {
                int r = Random.Range(0, 4);
                spawner.SpawNote(r, 0);
                spawner2.SpawNote(r, 0);
                timer = (cooldown + Random.Range(0f, 2f)) / gameSpeed;
                wizard.SetBool("attack", true);
                wizardParticle.Play();
            }
            else
            {
                timer -= Time.deltaTime;
                wizard.SetBool("attack", false);

            }
        }
        else {
        
        }
    }

    public void EndGame(int lost = 0) {
        if (lost == 0)
        {
            SceneManager.LoadScene("EndGame2");
        }
        else {
            SceneManager.LoadScene("EndGame1");
        }
    }
}
