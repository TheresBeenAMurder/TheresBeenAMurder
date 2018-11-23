public class Evidence
{
    public string audioFile;
    public string description;
    public bool found;

    public Evidence(string audio, string desc, bool found)
    {
        this.audioFile = audio;
        this.description = desc;
        this.found = found;
    }
}
