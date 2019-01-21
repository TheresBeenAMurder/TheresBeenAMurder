using UnityEngine;

public class StickyComponent : MonoBehaviour
{
    public PianoCylinder parent;
    public PianoCylinderManager parentManager;
    public Transform snapPoint;
    public float tolerance = .5f;
    public int topOrBottomIndex;
    public GameObject attachedCyl;
    public int attachedIndex;

    private void Start()
    {
        attachedCyl = null;
    }


    private void OnTriggerExit(Collider other)
    {
        //if (other.gameObject.CompareTag("StickyComponent"))
        //{
        //    if (parentManager.isRotating && parent.attached != null && parent.attached.Length > 0 && parent.attached[topOrBottomIndex] != null)
        //    {
        //        parent.RemoveAttachedCylinder(topOrBottomIndex, other.GetComponent<StickyComponent>().topOrBottomIndex);
        //    }
        //    else if(parent.attached[topOrBottomIndex] != null)
        //    {
        //     //   other.gameObject.GetComponent<StickyComponent>().parent.transform.position = snapPoint.position;
        //    }
        //}
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
                attachedIndex = other.GetComponent<StickyComponent>().topOrBottomIndex;
                attachedCyl = other.gameObject.GetComponent<StickyComponent>().parent.gameObject;
              }
        }
    }

    private void Update()
    {
        if (attachedCyl != null)
        {

            if((Vector3.Distance(transform.position, attachedCyl.transform.position) > tolerance) && parentManager.isRotating)
            {
                attachedCyl = null;
                parent.RemoveAttachedCylinder(topOrBottomIndex, attachedIndex);
                
            }

        }
    }
}
