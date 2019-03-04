public class Evidence
{
    public string audioFile;
    public string description;
    public bool found;
    public int id;

    public Evidence(string audio, string desc, bool found, int id)
    {
        this.audioFile = audio;
        this.description = desc;
        this.found = found;
        this.id = id;
    }
}
