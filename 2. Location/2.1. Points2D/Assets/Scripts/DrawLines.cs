using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLines : MonoBehaviour
{
    readonly Coords point = new(10, 20);
    readonly Coords startPointXAxis = new(-160, 0);
    readonly Coords endPointXAxis = new(160, 0);
    readonly Coords startPointYAxis = new(0, 100);
    readonly Coords endPointYAxis = new(0, -100);

    readonly Coords[] leo = { new(0, 20), new(20, 30), new(80, 30), new(30, 50), new(80, 50), new(70, 60), new(70, 80), new(80, 90), new(95, 80) };

    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log(point.ToString());
        // Coords.DrawPoint(new Coords(0, 0), 2, Color.red);
        // Coords.DrawPoint(point, 2, Color.green);

        Coords.DrawLine(startPointXAxis, endPointXAxis, 0.5f, Color.red);
        Coords.DrawLine(startPointYAxis, endPointYAxis, 0.5f, Color.yellow);

        foreach (var item in leo)
        {
            Coords.DrawPoint(item, 2, Color.yellow);
        }
        Coords.DrawLine(leo[0], leo[1], 0.4f, Color.white);
        Coords.DrawLine(leo[1], leo[2], 0.4f, Color.white);
        Coords.DrawLine(leo[0], leo[3], 0.4f, Color.white);
        Coords.DrawLine(leo[3], leo[5], 0.4f, Color.white);
        Coords.DrawLine(leo[2], leo[4], 0.4f, Color.white);
        Coords.DrawLine(leo[4], leo[5], 0.4f, Color.white);
        Coords.DrawLine(leo[5], leo[6], 0.4f, Color.white);
        Coords.DrawLine(leo[6], leo[7], 0.4f, Color.white);
        Coords.DrawLine(leo[7], leo[8], 0.4f, Color.white);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
