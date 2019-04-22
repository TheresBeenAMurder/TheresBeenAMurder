using System.Linq;
using UnityEngine;
using System.Collections;

// Generic Trigger Sensor class, use in combo with Key.cs
public class Sensor : MonoBehaviour
{
    public Material correctColor;
    public string[] correctKeyIDs;
    public Material incorrectColor;
    public Puzzle puzzle;

    private Key currentKey;
    private Material originalColor;

    private bool solved = false;
    private bool isCorrect;

    public SecretDeskButton deskButton;

    public AudioClip scanningSound;
    public AudioClip correctSound;
    public AudioClip incorrectSound;

    public AudioSource scannerAudio;

    private void ChangeColor(Material newColor)
    {
        StartCoroutine(playScannerSounds(newColor));
    }

    IEnumerator playScannerSounds(Material newColor)
    {
        scannerAudio.clip = scanningSound;
        scannerAudio.Play();
        yield return new WaitForSeconds(scanningSound.length);
        scannerAudio.Stop();
        if(isCorrect)
        {
            scannerAudio.clip = correctSound;
        }
        else
        {
            scannerAudio.clip = incorrectSound;
        }
        scannerAudio.Play();
        gameObject.GetComponent<Renderer>().material = newColor;

    }

    public void OnTriggerExit(Collider other)
    {
        Key key = other.gameObject.GetComponent<Key>();

        // Only change color when key exits, nothing else
        if (key != null && key == currentKey && !solved)
        {
            ChangeColor(originalColor);
            currentKey = null;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        Key key = other.gameObject.GetComponent<Key>();
        if (key != null && currentKey == null)
        {
            currentKey = key;
            if (key.PuzzleType() == puzzle && correctKeyIDs.Contains(key.ID))
            {
                //  PUZZLE SOLVED HERE
                isCorrect = true;
                ChangeColor(correctColor);
                key.Solve();
                solved = true;
                deskButton.SolvePuzzle();
            }
            else
            {
                isCorrect = false;
                ChangeColor(incorrectColor);
            }
        }
    }

    public void Start()
    {
        originalColor = gameObject.GetComponent<Renderer>().material;
    }
}
