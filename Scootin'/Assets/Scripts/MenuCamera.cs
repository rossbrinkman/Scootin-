using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    //array that will contain the camera positions/rotations
    public Transform[] Positions;

    //identifies the current index of the positions array
    private int mCurrentIndex = 0;


    public float Speed = 2.0f;

    private void Update()
    {

        Vector3 currentPos = Positions[mCurrentIndex].position;
        Quaternion currentRot = Positions[mCurrentIndex].rotation;

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //checks to make sure upper limit of array wasn't reached
            if(mCurrentIndex < Positions.Length - 1)
            {
                mCurrentIndex++;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(mCurrentIndex > 0)
            {
                mCurrentIndex--;
            }
        }

        transform.position = Vector3.Lerp(transform.position, currentPos, Speed * Time.deltaTime);
        transform.rotation = currentRot;
    }
}
