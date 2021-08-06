using System.Collections.Generic;
using UnityEngine;

namespace DataVisualization.Charts
{
    [SelectionBase]
    public class Chart : MonoBehaviour
    {
        [Header("Horizontal")]
        public float minXValue;
        public float maxXValue;
        public Transform startX;
        public Transform endX;
        public List<Transform> pointsOnX;



        [Header("Vertical")]
        public float minYValue;
        public float maxYValue;
        public Transform startY;
        public Transform endY;
        public List<Transform> pointsOnY;



        [Header("Data")]
        public List<TwoDimensionalData> chartData = new List<TwoDimensionalData>();
        public LineRenderer lineRenderer;



        public void AddData(TwoDimensionalData data,bool renderChart = true)
        {
            chartData.Add(data);

            if (renderChart) RenderChart();
        }



        [ContextMenu("Chart/Render Chart")]
        public void RenderChart()
        {
            lineRenderer.positionCount = chartData.Count;

            for (int i = 0; i < chartData.Count; i++)
            {
                //Prevent user from inserting numbers off-limits
                chartData[i].dataValue = new Vector2(Mathf.Clamp(chartData[i].dataValue.x, minXValue, maxXValue), Mathf.Clamp(chartData[i].dataValue.y, minYValue, maxYValue));

                var x = ValueToChartPosition(startX.localPosition, endX.localPosition, maxXValue, chartData[i].dataValue.x);
                var y = ValueToChartPosition(startY.localPosition, endY.localPosition, maxYValue, chartData[i].dataValue.y);

                //Inserting values
                lineRenderer.SetPosition(i, new Vector2(x, y));
            }
        }



        /// <summary>
        /// Manipulates point on X & Y lines of the chart
        /// </summary>
        [ContextMenu("Chart/Manipulate")]
        private void DoManipulation()
        {
            ManipulatePoints(startX.position, endX.position);
            ManipulatePoints(startX.position, endX.position, false);
        }



        /// <summary>
        /// Manipulates point in given direction
        /// </summary>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="horizontaly"></param>
        private void ManipulatePoints(Vector3 startPoint, Vector3 endPoint, bool horizontaly = true)
        {
            var distance = Vector3.Distance(startPoint, endPoint);
            var spaceBetweenEachPoint = distance / pointsOnX.Count;


            var pose = new Vector3(horizontaly ? startPoint.x + spaceBetweenEachPoint : startPoint.x, horizontaly ? startPoint.y : startPoint.y + spaceBetweenEachPoint, startPoint.z);


            int numberOfPoints = horizontaly ? pointsOnX.Count : pointsOnY.Count;

            for (int i = 0; i < numberOfPoints; i++)
            {
                if (horizontaly)
                {
                    pointsOnX[i].position = pose;
                    pose.x += spaceBetweenEachPoint;
                }
                else
                {
                    pointsOnY[i].position = pose;
                    pose.y += spaceBetweenEachPoint;
                }
            }
        }


        /// <summary>
        /// Defines how much of the given line should be occupied by the data X or Y
        /// </summary>
        /// <param name="startpoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="max"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private float ValueToChartPosition(Vector3 startpoint, Vector3 endPoint, float max, float value)
        {
            var percentageInChart = value * 100 / maxXValue;

            var distanceVetweenTwoPointsOnChart = Vector3.Distance(startpoint, endPoint);

            return distanceVetweenTwoPointsOnChart * percentageInChart / 100; ;
        }


        private void OnDrawGizmos()
        {
            // X Axis
            Gizmos.color = Color.red;

            Gizmos.matrix = startX.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);

            Gizmos.matrix = endX.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);



            // Y Axis
            Gizmos.color = Color.green;

            Gizmos.matrix = startY.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);

            Gizmos.matrix = endY.localToWorldMatrix;
            Gizmos.DrawCube(Vector3.zero, Vector3.one * 10);
        }
    }
}