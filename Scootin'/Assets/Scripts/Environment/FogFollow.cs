using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFollow : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = new Vector3(player.transform.position.x, player.transform.position.y -28.5f, player.transform.position.z + 168f);
        transform.position = targetPosition;
    }
}
