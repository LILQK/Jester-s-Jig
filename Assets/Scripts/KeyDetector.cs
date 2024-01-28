using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDetector : MonoBehaviour
{
    public int key = 0;

    List<Transform> keys = new List<Transform>();

    private ParticleSystem particleSystem;

    public KeyCode detectionKey;

    [SerializeField]
    private NoteSpawner spawner;

    private Score score;

    public FootController controller;

    public AudioClip wrongClip;
    public AudioClip succesClip;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        score = GetComponentInParent<Score>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
        particleSystem.GetComponent<Renderer>().material.SetColor("_EmissionColor",GetColorByKey(key));
        spawner.onNoteSpawn += onNoteSpawn;
        spawner.onNoteDespawn += onNoteDespawn;
    }

    private void onNoteDespawn(GameObject n)
    {
        score.RemoveScore();
        keys.Remove(n.transform);
    }

    private void onNoteSpawn(int note, GameObject g)
    {
        if (note != key) return;

        keys.Add(g.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(detectionKey)) {
            particleSystem.Play();
            if (controller == null)
            {
                Debug.Log("No hay footcontroller");
            }
            else {
                controller.SetFootOnPose(key, this.transform.position);
            }
            Transform k = isAnyKeyClose();
            if (k != null)
            {
                audioSource.PlayOneShot(succesClip);
                score.AddScore();
                keys.Remove(k);
                k.GetComponent<NoteMovement>().particle.Play();
                k.GetComponent<NoteMovement>().particle.transform.parent = null;
                Destroy(k.gameObject);
            }
            else {
                audioSource.PlayOneShot(wrongClip);
                score.RemoveScore();
            }
        }
    }

    private Transform isAnyKeyClose() {
        foreach (Transform t in keys)
        {
            float d = GetDistanceWithKey(t.position);
            if (d <= 1f) {
                return t;
            }
        }
        return null;
    }

    private float GetDistanceWithKey(Vector3 p) {
        float distance = 999f;

        distance = Vector3.Distance(p,transform.position);
        return distance;
    }

    private Color GetColorByKey(int note)
    {
        switch (note)
        {
            case 0:
                return Color.yellow;
            case 1:
                return Color.green;
            case 2:
                return Color.red;
            case 3:
                return Color.blue;
            default:
                return Color.white;
        }
    }
}
