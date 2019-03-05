using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    public Text motiveFill;
    public Text meansFill;
    public Text opportunityFill;
    public Text alibiFill;

    private HashSet<string> motives = new HashSet<string>();
    private HashSet<string> means = new HashSet<string>();
    private HashSet<string> opportunitites = new HashSet<string>();
    private HashSet<string> alibis = new HashSet<string>();

	public void AddEvidence(string evidenceType, string evidence)
    {
        bool update = true;
        switch (evidenceType)
        {
            case "motive":
                update = motives.Add(evidence);
                break;
            case "means":
                update = means.Add(evidence);
                break;
            case "opportunity":
                update = opportunitites.Add(evidence);
                break;
            case "alibi":
                update = alibis.Add(evidence);
                break;
        }

        if (update)
        {
            UpdateFills(evidenceType);
        }
    }

    private void UpdateFills(string evidenceType)
    {
        string newFill = "";
        switch (evidenceType)
        {
            case "motive":
                foreach (string evidence in motives)
                {
                    newFill += evidence;
                    newFill += "\n";
                }
                motiveFill.text = newFill;
                break;
            case "means":
                foreach (string evidence in means)
                {
                    newFill += evidence;
                    newFill += "\n";
                }
                meansFill.text = newFill;
                break;
            case "opportunity":
                foreach (string evidence in opportunitites)
                {
                    newFill += evidence;
                    newFill += "\n";
                }
                opportunityFill.text = newFill;
                break;
            case "alibi":
                foreach (string evidence in alibis)
                {
                    newFill += evidence;
                    newFill += "\n";
                }
                alibiFill.text = newFill;
                break;
        }
    }
}
