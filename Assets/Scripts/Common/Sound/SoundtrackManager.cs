using UnityEngine;

public class SoundtrackManager : MonoBehaviour
{
    public SoundtrackLayer[] soundtrackLayers;

    //currently set to work with one track, will add functionlity to switch tracks
    public void SwitchTrack(int track)
    {
        foreach(SoundtrackLayer sl in soundtrackLayers)
        {
            sl.SwitchTrack(track);
        }
    }
}
