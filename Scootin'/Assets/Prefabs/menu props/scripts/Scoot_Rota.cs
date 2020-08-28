using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoot_Rota : MonoBehaviour
{

    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
    }
}
