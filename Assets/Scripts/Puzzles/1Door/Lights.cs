using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    public enum LightOptions
    {
        Default,
        Correct,
        Incorrect,
        IncorrectPlace
    }

    public Material correctColor;
    public Material defaultColor;
    public Material incorrectColor;
    public Material incorrectPlaceColor;
    public MeshRenderer[] lights = new MeshRenderer[3];

    private Dictionary<LightOptions, Material> colors = new Dictionary<LightOptions, Material>();

    // Sets all lights to default
    public void ResetLights()
    {
        // Resets lights to default color
        foreach (Renderer renderer in lights)
        {
            renderer.material = colors[LightOptions.Default];
        }
    }

    public void SetLight(int index, LightOptions color)
    {
        lights[index].material = colors[color];
    }

    public void Start()
    {
        // Initialize material dictionary that controls the color of the lights
        colors.Add(LightOptions.Default, defaultColor);
        colors.Add(LightOptions.Correct, correctColor);
        colors.Add(LightOptions.Incorrect, incorrectColor);
        colors.Add(LightOptions.IncorrectPlace, incorrectPlaceColor);
    }
}
