using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtrackTrack : MonoBehaviour {

    public float fadeTime = 1f;

    public SoundtrackManager parent;

    public SoundtrackLayer[] layers; //0 = drums, 1 = bass, 2 = piano, 3 = synth, 4 = brass


    // Use this for initialization
    void Start() {

    }

    public void fadeOut()
    {
        for(int i = 0; i < layers.Length; i++)
        {
            StartCoroutine(fadeOutCoroutine(i));
        }
    }

    public IEnumerator fadeOutCoroutine(int layer)
    {

        float increment = layers[layer].maxFade / fadeTime;

        while (layers[layer].audioSource.volume > 0)
        {
            layers[layer].audioSource.volume -= increment * Time.deltaTime;
        }

        yield return null;
    }

    public IEnumerator fadeInCoroutine(int layer, float max)
    {

        float increment = layers[layer].maxFade/fadeTime;

        while(layers[layer].audioSource.volume < max)
        {
            layers[layer].audioSource.volume += increment * Time.deltaTime;
        }

        yield return null;
    }

    void fadeIn(float[] values)
    {
        for(int i = 0; i < values.Length; i++)
        {
            //do a calculation since relVal is out of 10
            float maxValue = values[i] / 10f * layers[i].maxFade;
            StartCoroutine(fadeInCoroutine(i, maxValue));
        }
    }

	public void startTrack(float[] relValuesAll)
    {
        foreach(SoundtrackLayer layer in layers)
        {
            layer.audioSource.Play();
            layer.audioSource.volume = 0;
       }
        fadeIn(relValuesAll);
       
    }
  
}
