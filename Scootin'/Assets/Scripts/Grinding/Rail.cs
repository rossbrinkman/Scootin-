using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum MoverPlayMode { Linear, Catmull }

[ExecuteInEditMode]
public class Rail : MonoBehaviour
{
    [HideInInspector]
    public Transform[] nodes;

    [Range(0f, 15f)]
    public float heightBoost;

    private RailObjectMover railObjectMover;
    private RailObjectMover_Mobile railObjectMoverMobile;
    private GameObject player;

    private void Start()
    {
        nodes = GetComponentsInChildren<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");

        railObjectMover = player.GetComponent<RailObjectMover>();

        
        railObjectMoverMobile = player.GetComponent<RailObjectMover_Mobile>();
    }

    public Vector3 PositionOnRail(int segment, float ratio, MoverPlayMode mode)
    {
        switch (mode)
        {
            default:
            case MoverPlayMode.Linear:
                return LinearPosition(segment, ratio);
            case MoverPlayMode.Catmull:
                return CatmullPosition(segment, ratio);
        }
    }

    public Vector3 LinearPosition(int segment, float ratio)
    {
        Vector3 position1 = nodes[segment].position;
        position1.y += heightBoost;

        Vector3 position2 = nodes[segment + 1].position;
        position2.y += heightBoost;

        return Vector3.Lerp(position1, position2, ratio);
    }

    public Vector3 CatmullPosition(int segment, float ratio)
    {
        Vector3 point1, point2, point3, point4;

        if(segment == 0)
        {
            point1 = nodes[segment].position;
            point2 = point1;
            point3 = nodes[segment + 1].position;
            point4 = nodes[segment + 2].position;
        }
        else if(segment == nodes.Length - 2)
        {
            point1 = nodes[segment - 1].position;
            point2 = nodes[segment].position;
            point3 = nodes[segment + 1].position;
            point4 = point3;
        }
        else
        {
            point1 = nodes[segment - 1].position;
            point2 = nodes[segment].position;
            point3 = nodes[segment + 1].position;
            point4 = nodes[segment + 2].position;
        }

        float t2 = ratio * ratio;
        float t3 = t2 * ratio;

        //Catmull smoothing formula
        float x = 
            0.5f * ((2.0f * point2.x)
            + (-point1.x + point3.x)
            * ratio + (2.0f * point1.x - 5.0f * point2.x + 4 * point3.x - point4.x)
            * t2 + (-point1.x + 3.0f * point2.x - 3.0f * point3.x + point4.x)
            * t3);

        //Catmull smoothing formula
        float y =
            0.5f * ((2.0f * point2.y)
            + (-point1.y + point3.y)
            * ratio + (2.0f * point1.y - 5.0f * point2.y + 4 * point3.y - point4.y)
            * t2 + (-point1.y + 3.0f * point2.y - 3.0f * point3.y + point4.y)
            * t3);

        //Catmull smoothing formula
        float z =
            0.5f * ((2.0f * point2.z)
            + (-point1.z + point3.z)
            * ratio + (2.0f * point1.z - 5.0f * point2.z + 4 * point3.z - point4.z)
            * t2 + (-point1.z + 3.0f * point2.z - 3.0f * point3.z + point4.z)
            * t3);

        return new Vector3(x, y, z);
    }

    public Quaternion Orientation(int segment, float ratio)
    {
        Quaternion quaternion1 = nodes[segment].rotation;
        Quaternion quaternion2 = nodes[segment + 1].rotation;

        return Quaternion.Lerp(quaternion1, quaternion2, ratio);
    }

    //Creates some dotted lines between our Nodes
    private void OnDrawGizmos()
    {
        for (int i = 0; i < nodes.Length - 1; i++)
        {
            Handles.DrawDottedLine(nodes[i].position, nodes[i + 1].position, 3.0f);
        }
    }

    //When something (the player) hits the rail
    private void OnTriggerEnter(Collider other)
    {
        //Turn on RailObjectMover
        railObjectMover.start = true;
        railObjectMover.rail = this;
        railObjectMover.isCompleted = false;

        railObjectMoverMobile.start = true;
        railObjectMoverMobile.rail = this;
        railObjectMoverMobile.isCompleted = false;
    }
}
