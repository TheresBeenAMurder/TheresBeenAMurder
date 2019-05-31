using UnityEngine;
using UnityEngine.UI;

public class ImageSelector : MonoBehaviour
{
    public Sprite[] sprites;

    public int[] GenerateSpriteOrder(int numOpts)
    {
        int[] order = new int[numOpts];

        if (sprites.Length == 1)
        {
            for (int i = 0; i < numOpts; i++)
            {
                order[i] = 0;
            }
        }
        else
        {
            int prev = -1;
            for (int i = 0; i < numOpts; i++)
            {
                int current = prev;
                while (current == prev)
                {
                    current = (int)(Random.value * (sprites.Length - 1));
                }

                order[i] = current;
                prev = current;
            }
        }

        return order;
    }
}
