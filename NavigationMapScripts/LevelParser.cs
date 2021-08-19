using System.Collections.Generic;
using UnityEngine;
public class LevelParser : MonoBehaviour
{
    private Transform[] childrenPositions;
    private List<Transform> platformsPositions;
    public MapArtist line;
    private List<List<MapArtist>> lines;

    private FrameSwitch[] frameSwitch;

    private void Start()
    {
        frameSwitch = GetComponentsInChildren<FrameSwitch>();
     
        
        foreach(var f in frameSwitch)
            f.FrameEntrance += AddFrame;
    }
  
    public void AddFrame(FrameSwitch activeFrame)
    {
        childrenPositions = activeFrame.activeFrame.GetComponentsInChildren<Transform>();
        platformsPositions = new List<Transform>();

        for (int i = 0; i < childrenPositions.Length; i++)
        {
            if (childrenPositions[i].gameObject.layer == 9 )
            {
                platformsPositions.Add(childrenPositions[i]);
            }
        }
        lines = new List<List<MapArtist>>();
        lines.Add(new List<MapArtist>());
        for (int i = 0; i < platformsPositions.Count; i++)
        {
            if (platformsPositions[i].gameObject.GetComponent<BoxCollider2D>()) 
            {
                lines[0].Add(Instantiate(line));
                
                Vector3[] upLines =
                {
                    platformsPositions[i].position - new Vector3(platformsPositions[i].localScale.x / 2, 0, 0) ,
                    platformsPositions[i].position + new Vector3(platformsPositions[i].localScale.x / 2, 0, 0)
                };
                lines[0][i].SetUpLines(upLines, platformsPositions[i].localScale.y);
                lines[0][i].name = $"Box{i}";
            }
            if (platformsPositions[i].gameObject.GetComponent<PolygonCollider2D>())
            {
                if (i != 0)
                    lines.Add(new List<MapArtist>());
                PolygonCollider2D polygonCollider2D = platformsPositions[i].gameObject.GetComponent<PolygonCollider2D>();
                for (int j = 1; j < polygonCollider2D.points.Length; j++)
                {
                    lines[i].Add(Instantiate(line));
                    Vector3[] upLines =
                    {
                         platformsPositions[i].position + new Vector3(polygonCollider2D.points[j-1].x, polygonCollider2D.points[j-1].y, 0) ,
                         platformsPositions[i].position + new Vector3(polygonCollider2D.points[j].x, polygonCollider2D.points[j].y, 0)
                    };
                    lines[i][j-1].SetUpLines(upLines, 0.3f);
                    lines[i][j-1].name = $"Poli{j-1} {i}";
                }
                for (int j = polygonCollider2D.points.Length - 1; j < polygonCollider2D.points.Length; j++)
                {
                    lines[i].Add(Instantiate(line));
                    Vector3[] upLines =
                    {
                         platformsPositions[i].position + new Vector3(polygonCollider2D.points[j].x, polygonCollider2D.points[j].y, 0) ,
                         platformsPositions[i].position + new Vector3(polygonCollider2D.points[0].x, polygonCollider2D.points[0].y, 0)
                    };
                    lines[i][j].SetUpLines(upLines, 0.3f);
                    lines[i][j].name = $"Poli{j} {i}";
                }
            }
        }

        activeFrame.FrameEntrance -= AddFrame;
    }
}
