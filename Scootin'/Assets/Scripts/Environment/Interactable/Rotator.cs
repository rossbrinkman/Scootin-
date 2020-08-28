//using System.Collections;
//using System.Collections.Generic;
//5/15/2020 These namespaces can be removed. -Seth

using UnityEngine;

//5/15/2020 The single responsibility should be listed here. -Seth
// this script is allowing any coins to rotate 
public class Rotator : MonoBehaviour
{
    // this script is allowing any coins to rotate

    //5/15/2020 In my opinion it's best to make this private and set it to a strict value. 
    //That way objects will never rotate at different speeds if something is messed up in the editor. 
    //Though I get what you were going for. -Seth
    private int rotateSpeed;

    private void Start()
    {
        rotateSpeed = 1;
    }

    private void Update()
    {
        transform.Rotate(0, rotateSpeed, 0, Space.Self);
    }

}
