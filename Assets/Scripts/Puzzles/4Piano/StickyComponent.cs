using UnityEngine;

public class StickyComponent : MonoBehaviour
{
    public PianoCylinder parent;
    public PianoCylinderManager parentManager;
    public Transform snapPoint;
    public float tolerance = .1f;
    public int topOrBottomIndex;

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("StickyComponent"))
        {
            if (parentManager.isRotating)
            {
                parent.RemoveAttachedCylinder(topOrBottomIndex, other.GetComponent<StickyComponent>().topOrBottomIndex);
            }
            else if(parent.attached[topOrBottomIndex] != null)
            {
             //   other.gameObject.GetComponent<StickyComponent>().parent.transform.position = snapPoint.position;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("StickyComponent"))
        {
            if(parentManager.isRotating && 
                ((parent.attached.Length > 0 && parent.attached[topOrBottomIndex] == null) || 
                parent.attached.Length == 0))
             {
                parent.AttachCylinder(other.gameObject.GetComponent<StickyComponent>().parent.gameObject,
                    topOrBottomIndex,
                    other.GetComponent<StickyComponent>().topOrBottomIndex);

                if (Vector3.Distance(other.gameObject.GetComponent<StickyComponent>().parent.transform.position,
                    snapPoint.position) > tolerance)
                {
                    other.gameObject.GetComponent<StickyComponent>().parent.transform.position = snapPoint.position;
                }
              }
        }
    }
}
