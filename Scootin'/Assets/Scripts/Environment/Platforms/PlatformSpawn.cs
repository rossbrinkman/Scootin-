using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatformSpawn : MonoBehaviour
{
    public Transform nextSpawn;

    PlatformPooler platPooler;

    private GameObject platTrigger;

    public float platChangeDelay;
    public float platSpawnDelay;

    private bool tranplat = false;
    private bool firstSpawn = false;

    
    private void Start()
    {
        platPooler = PlatformPooler.Instance;

        tranplat = false;
        firstSpawn = true;

        //StartCoroutine(TranTimer());
    }

    //private void Update()
    //{
    //    //print(tranplat);
    //}

    //calls next platform here and sets next spawn point
    private void OnTriggerEnter(Collider other)
    {
        Vector3 moveSpawnPoint = new Vector3(nextSpawn.position.x, nextSpawn.position.y, nextSpawn.position.z + 85);

        if (other.CompareTag("Player"))
        {
            PlatSpawn();
        }
    }

    //spawns platfrom
    public void PlatSpawn()
    {
        Vector3 moveSpawnPoint = new Vector3(nextSpawn.position.x, nextSpawn.position.y, nextSpawn.position.z);

        //if (Time.unscaledTime <= 30f)
        //{
        if (tranplat == false)
        {
            //Debug.Log("City Triggered");
            int num = Random.Range(0, 5);
            switch (num)
            {
                case 0:
                    platPooler.SpawnFromPool("Mountain", moveSpawnPoint, nextSpawn.rotation);
                    break;
                case 1:
                    platPooler.SpawnFromPool("Mountain3", moveSpawnPoint, nextSpawn.rotation);
                    break;
                case 2:
                    platPooler.SpawnFromPool("Mountain4", moveSpawnPoint, nextSpawn.rotation);
                    break;
                case 3:
                    platPooler.SpawnFromPool("Mountain5", moveSpawnPoint, nextSpawn.rotation);
                    break;
                case 4:
                    if(Random.Range(0,2) == 1)
                        platPooler.SpawnFromPool("Transition", moveSpawnPoint, nextSpawn.rotation);
                    else
                        platPooler.SpawnFromPool("Mountain", moveSpawnPoint, nextSpawn.rotation);
                    break;
                default:
                    platPooler.SpawnFromPool("Mountain", moveSpawnPoint, nextSpawn.rotation);
                    break;
            }      

            if(firstSpawn == true)
            {
                Debug.Log("Coroutine Started");

                StartCoroutine(TranTimer());
                firstSpawn = false;
            }

        }

        //else if (Time.unscaledTime >= 30f && Time.unscaledTime <= 45f)
        //{
        if (tranplat == true)
        {
            //Debug.Log("Transfer Triggered");

            platPooler.SpawnFromPool("Transition", moveSpawnPoint, nextSpawn.rotation);
        }

        //else if (Time.unscaledTime >= 45.1f)
        //{
        //    Debug.Log("Wood Triggered");

        //    platPooler.SpawnFromPool("Mountain", moveSpawnPoint, nextSpawn.rotation);
        //}
    }

    public IEnumerator TranTimer()
    {

        yield return new WaitForSeconds(20f);
        
        tranplat = true;

        yield return new WaitForSeconds(10f);

        tranplat = false;

        firstSpawn = true;

        StopCoroutine(TranTimer());
    }
}
