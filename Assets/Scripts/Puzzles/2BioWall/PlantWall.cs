using UnityEngine;

public class PlantWall : MonoBehaviour
{
    //public PuzzleSolution art;
    public GalleryDisplay gallery;
    private PlantPot[] plants;
    public AudioSource win;
    bool isSolved = false;

    public void CheckForSolution()
    {
        // can't solve the puzzle more than once
        if (!isSolved)
        {
            foreach (PlantPot plant in plants)
            {
                if (!plant.IsCorrect())
                {
                    return;
                }
            }
            win.Play();
            gallery.activateImages();
            isSolved = true;
            //art.PuzzleSolve();
        }
    }

    public void Start()
    {
        plants = GetComponentsInChildren<PlantPot>();
    }
}
