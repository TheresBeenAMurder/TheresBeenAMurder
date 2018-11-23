using UnityEngine;

public class MurderBoard : MonoBehaviour {
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "BoardPhoto")
        {
            // reparent photo to board
            other.gameObject.transform.parent = transform;
            other.gameObject.transform.localScale = new Vector3(.15f, .25f, .005f);
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("BoardPhoto"))
        {
            other.gameObject.transform.parent = null;
            other.gameObject.transform.localScale = new Vector3(.108f, .105f, .004f);
            other.gameObject.GetComponent<Photo>().purgeLines();
        }
    }
}
