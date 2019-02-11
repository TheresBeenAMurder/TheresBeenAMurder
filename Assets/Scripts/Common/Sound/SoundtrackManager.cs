using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public SoundtrackTrack[] soundtrackTracks;

    public int currentTrack = 0;

    public int madelineRelationship;
    public int mavisRelationship;
    public int victorRelationship;

    public void Start()
    {
        soundtrackTracks[currentTrack].startTrack();
    }

    public void nextTrack()
    {
        soundtrackTracks[currentTrack].Fade();
        currentTrack += 1;
        soundtrackTracks[currentTrack].startTrack();

    }

    public void updateRelationship(string characterName, int relVal)
    {
        int updateLayerNumber = 0;

        switch (characterName)
        {
            case "Madeline":
       
                {
                    updateLayerNumber = 4;
                    madelineRelationship = relVal;
                    break;
                }
            case "Mavis":
                {
                    updateLayerNumber = 2;
                    mavisRelationship = relVal;
                    break;
                }
            case "Victor":
                {
                    updateLayerNumber = 3;
                    victorRelationship = relVal;
                    break;
                }
            default:
                {
                    break;
                }
                
        }

        //update it in the track
        soundtrackTracks[currentTrack].updateLayer(updateLayerNumber, relVal);

    }
}
