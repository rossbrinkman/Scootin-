using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    private Movement movement;
    private Movement_Mobile movement_M;
    public float smoothTime = 0, followDistance = 5;
    private Vector3 velocity = Vector3.zero;
    private Quaternion startRotation;
    //[HideInInspector]
    public bool flatPlatform = false;
    private bool done = false;

    private void Start()
    {
        //player = transform.parent;
        movement = player.GetComponent<Movement>();
        movement_M = player.GetComponent<Movement_Mobile>();
        startRotation = transform.rotation;
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - followDistance);
    }

    void FixedUpdate()
    {
        if (movement.transition || movement_M.transition)
        {
            if (followDistance < 7)
                followDistance = followDistance + Time.fixedDeltaTime * 2f;// .025f;
        }
        else
        {
            if (followDistance > 5)
                followDistance = followDistance - Time.fixedDeltaTime * 2f;//.025f;
        }
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - followDistance);
        Quaternion targetRotation = player.parent.rotation;
        targetRotation *= Quaternion.Euler(15, 0, 0);
        transform.position = targetPosition; //Vector3.Slerp(transform.position, targetPosition, Time.fixedDeltaTime * 2.5f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime*2.5f);
    }
    //    if (!flatPlatform)
    //    {
    //        //transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.deltaTime * 2);
    //        //// Define a target position above and behind the target transform
    //        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 5f);

    //        //// Smoothly move the camera towards that target position
    //        //transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 2); ; //Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    //        Quaternion target;
    //        target = startRotation;
    //        if(transform.rotation != target)
    //            StartCoroutine(ChangeRotation(flatPlatform, target));
    //        if(transform.position != targetPosition)
    //            StartCoroutine(ChangePosition(flatPlatform, targetPosition));
    //    }
    //    else
    //    {
    //        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 10f);
    //        ////if (transform.position != targetPosition)
    //        ////    transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime);
    //        ////else
    //        //transform.position = targetPosition;

    //        //transform.rotation = Quaternion.Slerp(transform.rotation, player.parent.transform.rotation, Time.deltaTime * 2);
    //        Quaternion target;
    //        target = player.parent.transform.rotation;
    //        if (transform.rotation != target)
    //            StartCoroutine(ChangeRotation(flatPlatform, target));

    //        if (transform.position != targetPosition)
    //            StartCoroutine(ChangePosition(flatPlatform, targetPosition));
    //    }
    //}

    IEnumerator ChangeRotation(bool fp, Quaternion target)
    {
        if (!fp)
        {
            target = startRotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2);
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 5f);
            transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 2);
        }
        else
        {
            
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 2);
            Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y + 5, player.transform.position.z - 10f);
            transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 2);
        }
        yield return new WaitUntil(() => transform.rotation == target);
        transform.rotation = target;
    }

    IEnumerator ChangePosition(bool fp, Vector3 targetPosition)
    {
        if (!fp)
        {
            transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 2);
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, targetPosition, Time.deltaTime * 2);
        }
        yield return new WaitUntil(() => transform.position == targetPosition);
        transform.position = targetPosition;
    }
}
