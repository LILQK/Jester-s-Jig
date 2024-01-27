using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField]private GameObject prefab;


    [SerializeField]
    private Transform[] spawners;

    public delegate void OnNoteSpawn(int note,GameObject n);
    public OnNoteSpawn onNoteSpawn;

    public delegate void OnNoteDespawn(GameObject n);
    public OnNoteDespawn onNoteDespawn;

    List<GameObject> spawned = new List<GameObject>();

    public void SpawNote(int note,float offset) {
        Vector3 pos = spawners[note].position + (Vector3.right * (offset * 4 * GameManager.instance.gameSpeed));
        GameObject g = Instantiate(prefab, pos,Quaternion.identity);
        g.GetComponent<NoteMovement>().Spawner = this;
        Color c = GetColorByKey(note);

        Material mymat = g.GetComponent<Renderer>().material;
        mymat.color = c;
        mymat.SetColor("_EmissionColor", c);

        spawned.Add(g);

        if(onNoteSpawn != null)
            onNoteSpawn(note,g);
    }

    public void DespawnNote(GameObject g) {

        spawned.Remove(g);
        Destroy(g);
        if (onNoteDespawn != null)
            onNoteDespawn(g);

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
