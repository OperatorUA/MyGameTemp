using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchCmd : BaseCmd
{
    public float range;
    public BaseUnit unitComponent;
    public Vector3Int centerCoords;

    public SearchCmd(float range, BaseUnit unitComponent)
    {
        this.range = range;
        this.unitComponent = unitComponent;
        CmdCompleted.AddListener(BeforeCompleteOrder);
    }
    public override void Execute()
    {

    }

    private void BeforeCompleteOrder()
    {
        if (circle != null) GameObject.Destroy(circle.gameObject);
    }

    private GameObject circle;
    private LineRenderer circleLineRenderer;
    public int circleSegmentsCount;
    private void DrawAreaCircle()
    {
        circleSegmentsCount = Mathf.RoundToInt(20 + range * 2);
        if (circle == null)
        {
            circle = new GameObject("SearchRange");
            circleLineRenderer = circle.AddComponent<LineRenderer>();
            circleLineRenderer.positionCount = circleSegmentsCount + 1;

            circleLineRenderer.startWidth = 0.04f;
            circleLineRenderer.endWidth = 0.04f;

            circleLineRenderer.material = new Material(Shader.Find("Universal Render Pipeline/Unlit"));
            circleLineRenderer.material.color = Color.blue;
        }

        Vector3[] segmentsPositions = new Vector3[circleSegmentsCount + 1];
        for (int i = 0; i <= circleSegmentsCount; i++)
        {
            Vector3 centerPosition = GridNavigation.GetCellCenterPosition(unitComponent.transform.position);

            float angle = 2f * Mathf.PI / circleSegmentsCount * i;

            float xPos = Mathf.Cos(angle) * range;
            float yPos = 0.1f;
            float zPos = Mathf.Sin(angle) * range;

            Vector3 pointPosition = new Vector3(xPos, yPos, zPos);
            pointPosition += centerPosition;

            segmentsPositions[i] = pointPosition;
        }

        
        circleLineRenderer.SetPositions(segmentsPositions);
    }
}
