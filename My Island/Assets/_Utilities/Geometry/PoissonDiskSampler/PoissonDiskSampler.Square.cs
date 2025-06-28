using System.Collections.Generic;

namespace UnityEngine
{
    public static partial class PoissonDiskSampler
    {
        public static class Square
        {
            public static List<Vector2> GeneratePoints(float width, float height, float minDistance, int maxAttempts = 30)
            {
                // Cell size for the grid
                var cellSize = minDistance / Mathf.Sqrt(2);
                var gridWidth = Mathf.CeilToInt(width / cellSize);
                var gridHeight = Mathf.CeilToInt(height / cellSize);

                var points = new List<Vector2>();
                var activeList = new List<Vector2>();

                // Grid to store points in cells for neighbor optimization
                var grid = new Vector2?[gridWidth, gridHeight];

                // Place the initial point randomly within the square
                var initialPoint = new Vector2(Random.Range(0, width), Random.Range(0, height));
                points.Add(initialPoint);
                activeList.Add(initialPoint);

                var initialGridX = Mathf.FloorToInt(initialPoint.x / cellSize);
                var initialGridY = Mathf.FloorToInt(initialPoint.y / cellSize);
                grid[initialGridX, initialGridY] = initialPoint;

                while (activeList.Count > 0)
                {
                    var activeIndex = Random.Range(0, activeList.Count);
                    var point = activeList[activeIndex];
                    var pointAdded = false;

                    for (var i = 0; i < maxAttempts; i++)
                    {
                        var newPoint = GenerateRandomPointAround(point, minDistance);

                        // Ensure the point is within the square and valid
                        if (!IsWithinBounds(newPoint, width, height) ||
                            !IsValidPoint(newPoint, minDistance, grid, gridWidth, gridHeight, cellSize)) continue;

                        points.Add(newPoint);
                        activeList.Add(newPoint);

                        var gridX = Mathf.FloorToInt(newPoint.x / cellSize);
                        var gridY = Mathf.FloorToInt(newPoint.y / cellSize);
                        grid[gridX, gridY] = newPoint;
                        pointAdded = true;
                        break;
                    }

                    if (!pointAdded)
                    {
                        activeList.RemoveAt(activeIndex);
                    }
                }

                return points;
            }

            private static Vector2 GenerateRandomPointAround(Vector2 point, float minDistance)
            {
                var angle = Random.Range(0f, 2f * Mathf.PI);
                var distance = minDistance * (1 + Random.Range(0f, 1f)); // Randomize distance multiplier
                return point + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            }

            private static bool IsWithinBounds(Vector2 point, float width, float height)
            {
                return point.x >= 0 && point.x <= width && point.y >= 0 && point.y <= height;
            }

            private static bool IsValidPoint(Vector2 point, float minDistance, Vector2?[,] grid, int gridWidth, int gridHeight, float cellSize)
            {
                var gridX = Mathf.FloorToInt(point.x / cellSize);
                var gridY = Mathf.FloorToInt(point.y / cellSize);

                // Check neighboring cells for too-close points
                for (var y = Mathf.Max(0, gridY - 2); y <= Mathf.Min(gridHeight - 1, gridY + 2); y++)
                {
                    for (var x = Mathf.Max(0, gridX - 2); x <= Mathf.Min(gridWidth - 1, gridX + 2); x++)
                    {
                        if (grid[x, y].HasValue && (grid[x, y].Value - point).sqrMagnitude < minDistance * minDistance)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }
    }
}
