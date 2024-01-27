using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public int loser = 0;

    [SerializeField]
    private Transform particleT;

    private ParticleSystem particle;

    [SerializeField]
    private Transform loserPlayer;

    [SerializeField]
    private Transform frog;

    private Animation animation;

    public float waitTime = 2f;

    [SerializeField]
    private GameObject restartCanvas;
    void Start()
    {
        particle = particleT.GetComponentInChildren<ParticleSystem>();
        animation = GetComponent<Animation>();
        restartCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationEnded() {
        particle.Play();
        StartCoroutine(waitAndSwitch());
    }

    IEnumerator waitAndSwitch() {
        yield return new WaitForSeconds(waitTime);

        loserPlayer.gameObject.SetActive(false);
        frog.gameObject.SetActive(true);
        restartCanvas.SetActive(true);
    }
}
