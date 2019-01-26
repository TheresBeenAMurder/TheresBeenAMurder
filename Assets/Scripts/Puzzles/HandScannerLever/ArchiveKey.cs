using UnityEngine;

public class ArchiveKey : Key
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public override Puzzle PuzzleType()
    {
        return Puzzle.Archive;
    }

    public override void Solve()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
