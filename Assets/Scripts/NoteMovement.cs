using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMovement : MonoBehaviour
{

    public NoteSpawner Spawner;

    public ParticleSystem particle;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        StartCoroutine(EscalarObjeto());
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * (GameManager.instance.gameSpeed * 2));
    }
    private void OnTriggerEnter(Collider other)
    {
        particle.Play();
        particle.transform.parent = null;
        Spawner.DespawnNote(this.gameObject);
    }

    IEnumerator EscalarObjeto()
    {
        Vector3 escalaInicial = Vector3.zero; // Escala inicial (0,0,0)
        Vector3 escalaFinal = new Vector3(1,0.1f,1);   // Escala final (1,1,1)
        float tiempoPasado = 0f;

        while (tiempoPasado < .5f)
        {
            transform.localScale = Vector3.Lerp(escalaInicial, escalaFinal, tiempoPasado / .5f);
            tiempoPasado += Time.deltaTime;
            yield return null;
        }

        // Asegurarse de que la escala sea exactamente 1 al finalizar la animación
        transform.localScale = escalaFinal;
    }

}
