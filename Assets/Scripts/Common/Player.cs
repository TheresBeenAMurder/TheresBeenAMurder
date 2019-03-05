using System.Data;
using UnityEngine;

public class Player : MonoBehaviour
{
    public DatabaseHandler dbHandler;
    public CharacterInfo madelineInfo;
    public CharacterInfo mavisInfo;
    public AudioSource openingDialogue;
    public GameObject outsideFloor;
    public GameObject tablet;
    public OVRInput.Button tabletButton;
    public CharacterInfo victorInfo;

    private int outsideFloorLayer;
    private bool switched = false;

    public void Start()
    {
        outsideFloorLayer = outsideFloor.layer;

        // Switch the outside floor layer so you can't teleport
        outsideFloor.layer = 0;

        ToggleTablet();
    }

    private void ToggleTablet()
    {
        tablet.SetActive(!tablet.activeSelf);

        if (tablet.activeSelf)
        {
            UpdateTablet();
        }
    }

    public void Update()
    {
        if (!switched && !openingDialogue.isPlaying)
        {
            outsideFloor.layer = outsideFloorLayer;
            switched = true;
        }

        if (OVRInput.GetDown(tabletButton))
        {
            ToggleTablet();
        }
    }

    private void UpdateTablet()
    {
        bool shouldClose = !dbHandler.SetUpDatabase();
        for (int i = 2; i <= 4; i++)
        {
            string query = "SELECT Type, Found, Description FROM 'Evidence' WHERE CharacterID == " + i;
            IDataReader reader = dbHandler.ExecuteQuery(query);

            while (reader.Read())
            {
                string type = reader.GetString(0).ToLower();
                bool found = reader.GetBoolean(1);
                string description = reader.GetString(2);

                if (found)
                {
                    switch (i)
                    {
                        case 2:
                            madelineInfo.AddEvidence(type, description);
                            break;
                        case 3:
                            victorInfo.AddEvidence(type, description);
                            break;
                        case 4:
                            mavisInfo.AddEvidence(type, description);
                            break;
                    }
                }
            }
            reader.Close();
        }

        if (shouldClose)
        {
            dbHandler.ShutDownDatabase();
        }
    }
}
