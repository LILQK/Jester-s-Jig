using System.Collections;
using System.Linq;
using UnityEngine;

public class MidiNoteSpawner : MonoBehaviour
{
    public TextAsset midiJson;  // Assign your MIDI JSON file in the Unity Inspector
    public GameObject notePrefab;  // Assign your note GameObject in the Unity Inspector


    public NoteSpawner spawner1;
    public NoteSpawner spawner2;
    private void Start()
    {
        if (midiJson == null)
        {
            Debug.LogError("Please assign the MIDI JSON file and note prefab in the inspector.");
            return;
        }

        SpawnNotes();
    }

    private void SpawnNotes()
    {
        MidiData midiData = JsonUtility.FromJson<MidiData>(midiJson.text);

        foreach (var track in midiData.tracks)
        {
            foreach (var note in track.notes)
            {
                // Calculate position based on note time (you may need to adjust this based on your game's needs)
                float xPos = note.time;
                float yPos = track.id * 2.0f;  // Adjust the vertical position based on the track ID
                float zPos = 0.0f;

                Vector3 spawnPosition = new Vector3(xPos, yPos, zPos);

                // Spawn the note GameObject
                //GameObject spawnedNote = Instantiate(notePrefab, spawnPosition, Quaternion.identity);

                // Assign a key to the note (adjust as needed)
                int key = (int)Mathf.Repeat(MapearNota(note.name), 4.0f);  // Using modulo to get a key in the range [0, 3]

                //spawnedNote.name = "Note_Key_" + key;
                spawner1.SpawNote(key,note.time);
                spawner2.SpawNote(key,note.time);

            }
        }
    }

    [System.Serializable]
    public class MidiData
    {
        public Track[] tracks;
    }

    [System.Serializable]
    public class Track
    {
        public int id;
        public Note[] notes;
    }

    [System.Serializable]
    public class Note
    {
        public string name;
        public int midi;
        public float time;
        public float velocity;
        public float duration;
    }

    private int MapearNota(string name)
    {
        // Eliminar la letra de la nota
        string numeroNotaString = new string(name.Where(char.IsDigit).ToArray());

        // Convertir el número de la nota a entero
        int numeroNota;
        if (int.TryParse(numeroNotaString, out numeroNota))
        {
            // Mapear el número de la nota a un valor entre 0 y 3
            int valorMapeado = numeroNota % 4;

            return valorMapeado;
        }
        else
        {
            Debug.LogError("No se pudo convertir el número de la nota a entero.");
            return -1; // O cualquier otro valor que indique un error
        }
    }
}
