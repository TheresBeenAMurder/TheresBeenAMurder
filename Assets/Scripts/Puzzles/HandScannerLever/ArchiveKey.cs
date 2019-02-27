using UnityEngine;

public class ArchiveKey : Key
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public Material emptyColor;
    public Material fullColor;

    // Removes information from archive key cannister
    public void Empty()
    {
        ID = 0;
        audioClip = null;
        GetComponent<Renderer>().material = emptyColor;
    }

    public void Fill(int id, AudioClip audio)
    {
        ID = id;
        audioClip = audio;
        GetComponent<Renderer>().material = fullColor;
    }

    public override Puzzle PuzzleType()
    {
        return Puzzle.Archive;
    }

    public override void Solve()
    {
        if (audioClip != null)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            Empty();
        }
    }

    public void Start()
    {
        if (ID != 0)
        {
            GetComponent<Renderer>().material = fullColor;
        }
    }
}
