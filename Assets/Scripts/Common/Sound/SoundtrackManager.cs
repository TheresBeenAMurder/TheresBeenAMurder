using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public SoundtrackTrack[] soundtrackTracks;

    public int currentTrack = 0;

   public float madelineRelationship;
   public float mavisRelationship;
  public float victorRelationship;

    public void Start()
    {
        soundtrackTracks[currentTrack].startTrack(new float[5] { 10, 10, mavisRelationship, victorRelationship, madelineRelationship });
      
    }

    public void nextTrack()
    {
        playTrack(currentTrack + 1);
    }

    public void playTrack(int trackToPlay)
    {
        soundtrackTracks[currentTrack].fadeOut();
        currentTrack = trackToPlay;
        soundtrackTracks[currentTrack].startTrack(new float[5] { 10,10,mavisRelationship,victorRelationship,madelineRelationship});
        //soundtrackTracks[currentTrack].layers[4].audioSource.volume = madelineRelationship / 10f * soundtrackTracks[currentTrack].layers[4].maxFade;
        //soundtrackTracks[currentTrack].layers[2].audioSource.volume = mavisRelationship / 10f * soundtrackTracks[currentTrack].layers[2].maxFade;
       // soundtrackTracks[currentTrack].layers[3].audioSource.volume = victorRelationship / 10f * soundtrackTracks[currentTrack].layers[3].maxFade;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            updateRelationship("Madeline", madelineRelationship + 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            updateRelationship("Mavis", mavisRelationship + 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            updateRelationship("Victor", victorRelationship + 1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            nextTrack();
        }
    }

    public void updateRelationship(string characterName, float value)
    {
        int updateLayerNumber = 0;

        switch (characterName)
        {
            case "Madeline":
       
                {
                    updateLayerNumber = 4;
                    madelineRelationship = value;
                    break;
                }
            case "Mavis":
                {
                    updateLayerNumber = 2;
                    mavisRelationship = value;
                    break;
                }
            case "Victor":
                {
                    updateLayerNumber = 3;
                    victorRelationship = value;
                    break;
                }
            default:
                {
                    break;
                }
                
        }

        //update it in the track
        soundtrackTracks[currentTrack].layers[updateLayerNumber].audioSource.volume = value/ 10f * soundtrackTracks[currentTrack].layers[updateLayerNumber].maxFade;

    }
}
