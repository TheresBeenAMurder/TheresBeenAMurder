using UnityEngine;

public class Invitation : OVRGrabbable
{

    public Transform mavisHandItem;
    float[] positionOffset;
    float[] rotationOffset;
    public bool hasBeenGrabbed = false;

    public NPCAnimator mavisAnimator;

    public Rigidbody invitationRigidbody;
    bool hasBeenReleased = false;


    protected override void Start()
    {

        base.Start();
        positionOffset = new float[3] { 0, 0, 0 };
        rotationOffset = new float[3] { 0, 0, 0 };

        //positionOffset[0] = transform.position.x - mavisHand.position.x;
        //positionOffset[1] = transform.position.y - mavisHand.position.y;
        //positionOffset[2] = transform.position.z - mavisHand.position.z;

       // rotationOffset[0] = transform.rotation.x - mavisHand.rotation.x;
       // rotationOffset[1] = transform.rotation.y - mavisHand.rotation.y;
       // rotationOffset[2] = transform.rotation.z - mavisHand.rotation.z;
        // rotationOffset[0] = rotationOffset[0] * Mathf.Deg2Rad;
        //rotationOffset[1] = rotationOffset[1] * Mathf.Deg2Rad;
        //rotationOffset[2] = rotationOffset[2] * Mathf.Deg2Rad;

        gameObject.SetActive(false);

    }

    private void Update()
    {
        if(!hasBeenGrabbed)
        {
            transform.position = mavisHandItem.position;
            transform.rotation = mavisHandItem.rotation;
        }
    }

    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity, bool useGravity = false)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        if(!hasBeenReleased)
        {
            hasBeenReleased = true;

            invitationRigidbody.useGravity = true;
        }
    }

    public override void GrabBegin(OVRGrabber hand, Collider grabPoint, bool useGravity = false)
    {
        base.GrabBegin(hand, grabPoint);
        if (!hasBeenGrabbed)
        {
            hasBeenGrabbed = true;
            
            mavisAnimator.changeState(NPCAnimator.CHARACTERSTATE.IDLE);
        }
    }

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        gameObject.GetComponent<Rigidbody>().useGravity = true;
    //        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    //    }
    //}
}
