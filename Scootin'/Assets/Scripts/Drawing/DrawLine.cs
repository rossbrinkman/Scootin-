using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DrawLine : MonoBehaviour
{
    public GameObject currentImage;
    public RectTransform canvas;
    
    public GameObject image;

    public List<GameObject> images;
    public List<Vector2> fingerPositions;

    public static bool holding;
    public static bool swipedUp;
    public static bool swipedDown;
    public static bool swipedLeft;
    public static bool swipedRight;
    public static bool tap;
    public static bool circleDrawn;

    private CanvasScaler canvasScaler;
    private RectTransform imageRect;

    //private float stopPosition, totalPixels;
    private Vector2 startPosition;
    private Vector2 anchoredPos;
    private Vector2 tempFingerPos = default;
    private bool outOfRange = false;

    void Start()
    {
        imageRect = image.GetComponent<RectTransform>();
        canvasScaler = canvas.GetComponent<CanvasScaler>();
        //stopPosition = -(canvasScaler.referenceResolution.x / 2) / 4;
        //totalPixels = canvasScaler.referenceResolution.x * canvasScaler.referenceResolution.y;
    }

    // Update is called once per frame
    void Update()
    {

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out anchoredPos);
        imageRect.anchoredPosition = anchoredPos;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Stationary:
                    print("Primed");
                    holding = true;
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            startPosition = anchoredPos;
            if (anchoredPos.x > -240)
            {
                CreateLine();
            }
            else
            {
                outOfRange = true;
            }
        }
        if (Input.GetMouseButton(0) && !outOfRange)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, Input.mousePosition, null, out anchoredPos);
            imageRect.anchoredPosition = anchoredPos;
            if (Vector2.Distance(tempFingerPos, fingerPositions[fingerPositions.Count - 1]) > .001f && anchoredPos.x > -240)
            {
                UpdateLine(Input.mousePosition);
            }
            else { outOfRange = true; }
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipedUp = false;
            swipedDown = false;
            swipedLeft = false;
            swipedRight = false;
            tap = false;
            circleDrawn = false;
            holding = false;

            if (!outOfRange)
            {
                DecideSwipe();
                Invoke("RemoveLastElementInList", .01f);
            }
            else
            {
                if (startPosition.x > -240 && outOfRange)
                {
                    DecideSwipe();
                    Invoke("RemoveLastElementInList", .01f);
                }
                else { print("Out of Range"); Invoke("RemoveLastElementInList", .01f); }
            }
        }
    }

    void CreateLine()
    {
        currentImage = Instantiate(image, Input.mousePosition, Quaternion.identity);        
        currentImage.transform.SetParent(canvas.transform);
        images.Add(currentImage);
        fingerPositions.Clear();
        fingerPositions.Add(Input.mousePosition);
        fingerPositions.Add(Input.mousePosition);
    }

    void UpdateLine(Vector2 newFingerPos)
    {
        currentImage = Instantiate(image, Input.mousePosition, Quaternion.identity);
        currentImage.transform.SetParent(canvas.transform);
        images.Add(currentImage);
        fingerPositions.Add(newFingerPos);
    }

    void RemoveLastElementInList()
    {
        foreach (GameObject image in images)
        {
            Destroy(image);
        }
        outOfRange = false;
    }

    void DecideSwipe()
    {
        //Make array length even
        int length = fingerPositions.Count;

        if (length % 2 != 0)
        {
            length--;
        }


        //Get 8 points evenly spread across array
        List<Vector2> points = new List<Vector2>();

        Vector2 startPoint = fingerPositions[0];
        Vector2 point2 = fingerPositions[Mathf.Clamp(Mathf.RoundToInt(length / 8), 0, length - 1)];
        Vector2 point3 = fingerPositions[Mathf.Clamp(Mathf.RoundToInt(length / 4), 0, length - 1)];
        Vector2 point4 = fingerPositions[Mathf.Clamp(Mathf.RoundToInt((length / 4)) + Mathf.RoundToInt((length / 8)), 0, length - 1)];
        Vector2 point5 = fingerPositions[length / 2];
        Vector2 point6 = fingerPositions[Mathf.Clamp(length - (Mathf.RoundToInt((length / 4)) + Mathf.RoundToInt((length / 8))), 0, length - 1)];
        Vector2 point7 = fingerPositions[Mathf.Clamp(length - (Mathf.RoundToInt(length / 4)), 0, length - 1)];
        Vector2 point8 = fingerPositions[Mathf.Clamp(length - (Mathf.RoundToInt(length / 8)), 0, length - 1)];
        Vector2 endPoint = fingerPositions[length - 1];

        points.Add(startPoint); points.Add(point2); points.Add(point3); points.Add(point4); points.Add(point5); points.Add(point6); points.Add(point7); points.Add(point8); points.Add(endPoint);


        //Get total distance of line
        float totalLineLength = 0;
        for (int i = 0; i < fingerPositions.Count - 1; i++)
        {
            float distance = Vector2.Distance(fingerPositions[i], fingerPositions[i + 1]);
            totalLineLength += distance;
        }

        //Get center point of coordinates (not used currently)
        Vector2 centerPosition;
        float totalX = 0;
        float totalY = 0;

        for (int i = 0; i < fingerPositions.Count - 1; i++)
        {
            totalX += fingerPositions[i].x;
            totalY += fingerPositions[i].y;
        }
        centerPosition = new Vector2(totalX / fingerPositions.Count, totalY / fingerPositions.Count);


        //Get Distance between points (not used currently)
        List<float> distances = new List<float>();
        distances.Add(Vector2.Distance(points[0], points[1]));
        distances.Add(Vector2.Distance(points[1], points[2]));
        distances.Add(Vector2.Distance(points[2], points[3]));
        distances.Add(Vector2.Distance(points[3], points[4]));
        distances.Add(Vector2.Distance(points[4], points[5]));
        distances.Add(Vector2.Distance(points[5], points[6]));
        distances.Add(Vector2.Distance(points[6], points[7]));
        distances.Add(Vector2.Distance(points[7], points[8]));


        //Get distance between points on x
        List<float> xDistances = new List<float>();
        xDistances.Add(startPoint.x - point2.x);
        xDistances.Add(point2.x - point3.x);
        xDistances.Add(point3.x - point4.x);
        xDistances.Add(point4.x - point5.x);
        xDistances.Add(point5.x - point6.x);
        xDistances.Add(point6.x - point7.x);
        xDistances.Add(point7.x - point8.x);
        xDistances.Add(point8.x - endPoint.x);


        //Get distance between points on y
        List<float> yDistances = new List<float>();
        yDistances.Add(startPoint.y - point2.y);
        yDistances.Add(point2.y - point3.y);
        yDistances.Add(point3.y - point4.y);
        yDistances.Add(point4.y - point5.y);
        yDistances.Add(point5.y - point6.y);
        yDistances.Add(point6.y - point7.y);
        yDistances.Add(point7.y - point8.y);
        yDistances.Add(point8.y - endPoint.y);


        //Make sure line is straight
        bool xValid = true;
        bool yValid = true;

        //float straightLineTolerance = totalPixels / 27648;
        
        foreach (float distance in xDistances)
        {
            if (distance > 75 || distance < -75) { xValid = false; }
        }
        foreach (float distance in yDistances)
        {
            if (distance > 75 || distance < -75) { yValid = false; }
        }


        //For when the line is real small and differences are minimal
        float priorityCheck_Up = endPoint.y - startPoint.y;
        float priorityCheck_Down = startPoint.y - endPoint.y;
        float priorityCheck_Left = startPoint.x - endPoint.x;
        float priorityCheck_Right = endPoint.x - startPoint.x;


        //Get slope between start and mid point
        Vector2 midPoint = (startPoint + point5) / 2;
        float radius = Vector2.Distance(startPoint, point5) / 2;
        float rise = -(point5.y - startPoint.y);
        float run = (point5.x - startPoint.x);
        float perpSlope = run / rise;

        //Where side points of circle should be
        Vector2 sideProjection1 = new Vector2(midPoint.x + (radius * (Mathf.Sqrt(1 / (1 + Mathf.Pow(perpSlope, 2)) ))), midPoint.y + (radius * perpSlope * (Mathf.Sqrt(1 / (1 + Mathf.Pow(perpSlope, 2))))));
        Vector2 sideProjection2 = new Vector2(midPoint.x - (radius * (Mathf.Sqrt(1 / (1 + Mathf.Pow(perpSlope, 2))))), midPoint.y - (radius * perpSlope * (Mathf.Sqrt(1 / (1 + Mathf.Pow(perpSlope, 2))))));

        
        //A few distances (not used currently)
        float xDistStartToMid = startPoint.x - point5.x;
        float xDistMidtoEnd = point5.x - endPoint.x;
        float yDistSidetoSide = point3.y - point7.y;


        //Circle?
        bool circle = false;

        float distance1 = Vector2.Distance(sideProjection1, point7);
        float distance2 = Vector2.Distance(sideProjection2, point3);
        float distance3 = Vector2.Distance(startPoint, endPoint);

        //float circleT1 = totalPixels / 7405.7f;
        //float circleT2 = totalPixels / 5184f;

        if ((distance3 < 280 && distance3 > -280) && (distance1 < 400 && distance1 > -400) && (distance2 < 400 && distance2 > -400))
        {
            circle = true;
        }
        
        //float maxLineLength_Circ = totalPixels / 6912f;
        //float maxLineLength_Line = totalPixels / 13824f;

        //Check swipe type
        if (totalLineLength == 0)
        {
            tap = true;
            print("Tap");
        }
        else if ((totalLineLength < 300 && circle) || (totalLineLength < 100 && !circle))
        {
            print("Line not long enough");
        }
        else if (xValid && endPoint.y > startPoint.y && priorityCheck_Up > priorityCheck_Left && priorityCheck_Up > priorityCheck_Right)
        {
            swipedUp = true;
            print("Swiped up");
        }
        else if (xValid && endPoint.y < startPoint.y && priorityCheck_Down > priorityCheck_Left && priorityCheck_Down > priorityCheck_Right)
        {
            swipedDown = true;
            print("SwipedDown");
        }
        else if (yValid && endPoint.x < startPoint.x && priorityCheck_Left > priorityCheck_Up && priorityCheck_Left > priorityCheck_Down)
        {
            swipedLeft = true;
            print("SwipedLeft");
        }
        else if (yValid && endPoint.x > startPoint.x && priorityCheck_Right > priorityCheck_Up && priorityCheck_Right > priorityCheck_Down)
        {
            swipedRight = true;
            print("SwipedRight");
        }
        else if (circle)
        {
            circleDrawn = true;
            print("Circle");
        }        
        else { print("Invalid Input"); }
    }
}
