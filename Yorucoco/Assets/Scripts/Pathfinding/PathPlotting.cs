using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PathPlotting : MonoBehaviour
{
    public NavMeshPath path;
    public NavMeshAgent Node;
    public Transform destination;

    public LineRenderer line;

    public Material ValidPath;
    public Material InvalidPath;

    public bool PathLocated = false;

    // Start is called before the first frame update
    void Start()
    {
        Node = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        Node.CalculatePath(destination.position, path);
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Node.CalculatePath(destination.position, path);
        Vector3[] corners = path.corners;
        for (int i = 0; i < path.corners.Length - 1; i++)
        {
            Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
            CreateLines(path.corners);
        }

        if (path.status == NavMeshPathStatus.PathPartial)
        { 
            print("WE CANT FIND SHIT NAVMESH ONEE CHAN "  + path.status);
            line.material = InvalidPath;
            PathLocated = false;
        } else {
            print("PathFound " + path.status);
            line.material = ValidPath;
            PathLocated = true;
        }
    }

    void CreateLines(Vector3[] positions)
    {
        line.positionCount = positions.Length;
        for (int i = 0; i < positions.Length; i++)
        {
            line.SetPosition(i, positions[i]);
        }
    }
}
