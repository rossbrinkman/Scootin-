using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewChunk : MonoBehaviour
{
    public Transform nextSpawn;
    public GameObject chunk;

    private GameObject[] chunkTriggers;

    public void SpawnNew()
    {
        Vector3 adjustedSpawn = new Vector3(nextSpawn.position.x, nextSpawn.position.y, nextSpawn.position.z + 85);
        Instantiate(chunk, adjustedSpawn, nextSpawn.rotation);
        StartCoroutine(WaitAndDestroy());
    }

    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.transform.parent.gameObject);
    }

}
