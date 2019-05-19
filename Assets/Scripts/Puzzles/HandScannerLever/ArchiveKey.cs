using UnityEngine;

public class ArchiveKey : Key
{
    public AudioClip audioClip;
    public AudioSource audioSource;
    public DatabaseHandler dbHandler;
    public Material emptyColor;
    public Material fullColor;

    private bool meansFound = false;

    // Removes information from archive key cannister
    public void Empty()
    {
        ID = "";
        audioClip = null;
        GetComponent<Renderer>().material = emptyColor;
    }

    public void Fill(string id, AudioClip audio)
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

            if (!meansFound && audioClip.name.Equals("MavisMeans"))
            {
                dbHandler.FindEvidence(new int[] { 11 });
                meansFound = true;
            }
        }
    }

    public void Start()
    {
        if (ID != "")
        {
            GetComponent<Renderer>().material = fullColor;
        }
        else
        {
            GetComponent<Renderer>().material = emptyColor;
        }
    }
}
