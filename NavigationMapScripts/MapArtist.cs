using UnityEngine;

public class MapArtist : MonoBehaviour
{
    private LineRenderer lr;
    private Vector3[] points;
    private float sizeLine; 

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.loop = true;
        lr.startWidth = sizeLine;
        lr.endWidth = sizeLine;
       // lr.positionCount = points.Length;
    }

    public void SetUpLines(Vector3[] points, float sizeLine)
    {
        this.points = points;
        this.sizeLine = sizeLine;
    }

    private void Update()
    {
        if (points == null)
            return;
        lr.SetPositions(points);
    }
}
