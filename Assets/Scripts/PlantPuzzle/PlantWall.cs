using UnityEngine;

public class PlantWall : MonoBehaviour
{
    public PuzzleSolution art;

    private PlantPot[] plants;

    public void CheckForSolution()
    {
        // can't solve the puzzle more than once
        if (!art.IsSolved())
        {
            foreach (PlantPot plant in plants)
            {
                if (!plant.IsCorrect())
                {
                    return;
                }
            }
            
            art.PuzzleSolve();
        }
    }

    public void Start()
    {
        plants = GetComponentsInChildren<PlantPot>();
    }
}
