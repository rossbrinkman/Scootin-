using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnWithBlockPoints : MonoBehaviour
{
    [SerializeField]
    public List<Transform> blockPoints;
    public List<Block> blocks;
    [HideInInspector]
    public List<int> usedInts;
    [HideInInspector]
    public List<GameObject> usedBlocks;
    [HideInInspector]
    public List<string> names;


    private int stopCount;
    private Vector3 parentTrans;
    private bool rowUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        parentTrans = transform.position;
        FindBlockPoints();

        int am = Random.Range(10, 20);
        for (int i = 0; i < am; i++)
            SpawnRandomBlocks();
    }

    void Update()
    {
        if (parentTrans != transform.position)
        {
            parentTrans = transform.position;
            ResetBlocks();
        }
    }

    void FindBlockPoints()
    {
        Transform[] temp;
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);

            temp = child.GetComponentsInChildren<Transform>();
            foreach (Transform tran in temp)
            {
                if (tran.tag == "BlockPoint")
                    blockPoints.Add(tran);
            }
        }
    }

    void SpawnRandomBlocks()
    {
        int blockNum = Random.Range(0, blocks.Count), pointNum = Random.Range(0, blockPoints.Count);
        Block block = blocks[blockNum];
        Transform spawnPoint = blockPoints[pointNum];
        //AdjustForWidth(block, pointNum, spawnPoint);

        if (!CheckInArray(pointNum) && !IsRowUsed(spawnPoint))
        {            
            if (block.blockLength == 1 && AdjustForWidth(block, pointNum, spawnPoint))
            {
                GameObject g = Instantiate(block.prefab, spawnPoint);
                g.transform.position = Vector3.zero + spawnPoint.position;
                usedInts.Add(pointNum);
                usedBlocks.Add(g);
                names.Add(spawnPoint.name);
            }
            else if (block.blockLength == 2 && spawnPoint.name != "Point4" && AdjustForWidth(block, pointNum, spawnPoint) && AdjustForWidth(block, pointNum + 1, spawnPoint))
            {
                if (!CheckInArray(pointNum + 1))
                {
                    GameObject g = Instantiate(block.prefab, spawnPoint);
                    g.transform.position = Vector3.zero + spawnPoint.position;
                    usedInts.Add(pointNum); usedInts.Add(pointNum + 1);
                    usedBlocks.Add(g);
                    names.Add(spawnPoint.name); names.Add(blockPoints[pointNum+1].name);
                }
            }
            else if (block.blockLength == 3 && (spawnPoint.name != "Point4" && spawnPoint.name != "Point3") && AdjustForWidth(block, pointNum, spawnPoint) && AdjustForWidth(block, pointNum+1, spawnPoint) && AdjustForWidth(block, pointNum + 2, spawnPoint))
            {
                if (!CheckInArray(pointNum + 1) && !CheckInArray(pointNum + 2))
                {
                    GameObject g = Instantiate(block.prefab, spawnPoint);
                    g.transform.position = Vector3.zero + spawnPoint.position;
                    usedInts.Add(pointNum); usedInts.Add(pointNum + 1); usedInts.Add(pointNum + 2);
                    usedBlocks.Add(g);
                    names.Add(spawnPoint.name); names.Add(blockPoints[pointNum + 1].name); names.Add(blockPoints[pointNum + 2].name);
                }
            }
        }
    }

    bool CheckInArray(int n)
    {
        if (usedInts.Count > 0)
        {
            for (int i = 0; i < usedInts.Count; i++)
            {
                if (n == usedInts[i])
                {
                    
                    return true;
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }

    bool IsRowUsed(Transform sp)
    {
        int i = 0;
        foreach (string n in names)
        {
            if (sp.name == n)
                i++;
        }
        if (i >= 2)
            return true;
        else
            return false;
    }

    public void ResetBlocks()
    {
        foreach (GameObject g in usedBlocks)
        {
            Destroy(g);
        }
        usedBlocks.Clear();
        usedInts.Clear();
        blockPoints.Clear();
        names.Clear();
        Start();
    }

    bool AdjustForWidth(Block block, int spn, Transform sp)
    {
        switch (block.blockWidth)
        {
            case 1:
                break;
            case 2:
                if (sp.parent.name != "Lane6" && !CheckInArray(spn + 4))
                { usedInts.Add(spn + 4); names.Add(blockPoints[spn + 4].name); }
                else
                    return false;
                break;
            case 3:
                if (sp.parent.name != "Lane6" && sp.parent.name != "Lane5" && !CheckInArray(spn + 4) && !CheckInArray(spn + 8))
                {
                    usedInts.Add(spn + 4); usedInts.Add(spn + 8);
                    names.Add(blockPoints[spn + 4].name); names.Add(blockPoints[spn + 8].name);
                }
                else
                    return false;
                break;
            case 4:
                if (sp.parent.name != "Lane6" && sp.parent.name != "Lane5" && sp.parent.name != "Lane4" && !CheckInArray(spn + 4) && !CheckInArray(spn + 8) && !CheckInArray(spn + 12))
                {
                    usedInts.Add(spn + 4); usedInts.Add(spn + 8); usedInts.Add(spn + 12);
                    names.Add(blockPoints[spn + 4].name); names.Add(blockPoints[spn + 8].name); names.Add(blockPoints[spn + 12].name);
                }
                else
                    return false;
                break;
            case 5:
                if (sp.parent.name == "Lane1" || sp.parent.name != "Lane2" && !CheckInArray(spn + 4) && !CheckInArray(spn + 8) && !CheckInArray(spn + 12) && !CheckInArray(spn + 16))
                {
                    usedInts.Add(spn + 4); usedInts.Add(spn + 8); usedInts.Add(spn + 12); usedInts.Add(spn + 16);
                    names.Add(blockPoints[spn + 4].name); names.Add(blockPoints[spn + 8].name); names.Add(blockPoints[spn + 12].name); names.Add(blockPoints[spn + 16].name);
                }
                else
                    return false;
                break;
            case 6:
                if (sp.parent.name == "Lane1" && !CheckInArray(spn + 4) && !CheckInArray(spn + 8) && !CheckInArray(spn + 12) && !CheckInArray(spn + 16) && !CheckInArray(spn + 20))
                {
                    usedInts.Add(spn + 4); usedInts.Add(spn + 8); usedInts.Add(spn + 12); usedInts.Add(spn + 16); usedInts.Add(spn + 20);
                    names.Add(blockPoints[spn + 4].name); names.Add(blockPoints[spn + 8].name); names.Add(blockPoints[spn + 12].name); names.Add(blockPoints[spn + 16].name); names.Add(blockPoints[spn + 20].name);
                }
                else
                    return false;
                break;
            default:
                Debug.LogError("Block width not within acceptable range.");
                return false;
        }
        return true;
    }

}

[System.Serializable]
public class Block
{
    public GameObject prefab;

    public int blockLength;
    public int blockWidth;
}