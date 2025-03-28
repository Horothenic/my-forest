using System.Collections.Generic;

namespace UnityEngine
{
    public static partial class PoissonDiskSampler
    {
        public static class Circle
        {
            public static List<Vector2> GeneratePoints(float radius, float minDistance, int maxAttempts = 30,  int borderPoints = 500)
            {
                // Cell size for grid based on minDistance
                var cellSize = minDistance / Mathf.Sqrt(2);
                var gridRadius = Mathf.CeilToInt((radius + 2 * minDistance) / cellSize); // Ensure sufficient grid space

                var points = new List<Vector2>();
                var activeList = new List<Vector2>();

                // Grid to store points in cells for neighbor optimization
                var grid = new Vector2?[2 * gridRadius + 1, 2 * gridRadius + 1];

                // Place the initial point at the center
                var initialPoint = Vector2.zero;
                points.Add(initialPoint);
                activeList.Add(initialPoint);
                grid[gridRadius, gridRadius] = initialPoint;

                // Add random sample points.
                while (activeList.Count > 0)
                {
                    var activeIndex = Random.Range(0, activeList.Count);
                    var point = activeList[activeIndex];
                    var pointAdded = false;

                    for (var i = 0; i < maxAttempts; i++)
                    {
                        var newPoint = GenerateRandomPointAround(point, minDistance);

                        if (!(newPoint.magnitude <= radius) || !IsValidPoint(newPoint, minDistance, grid, gridRadius, cellSize))
                            continue;

                        points.Add(newPoint);
                        activeList.Add(newPoint);

                        // Calculate grid indices and clamp to avoid out-of-bounds errors
                        var gridX = Mathf.Clamp(gridRadius + Mathf.FloorToInt(newPoint.x / cellSize), 0, grid.GetLength(0) - 1);
                        var gridY = Mathf.Clamp(gridRadius + Mathf.FloorToInt(newPoint.y / cellSize), 0, grid.GetLength(1) - 1);

                        grid[gridX, gridY] = newPoint;
                        pointAdded = true;
                        break;
                    }

                    if (!pointAdded)
                    {
                        activeList.RemoveAt(activeIndex);
                    }
                }

                // Add border points for smoothness.
                var angleStep = 2 * Mathf.PI / borderPoints;

                for (var i = 0; i < borderPoints; i++)
                {
                    var angle = i * angleStep;
                    var x = Mathf.Cos(angle) * radius;
                    var y = Mathf.Sin(angle) * radius;
                    points.Add(new Vector2(x, y));
                }

                return points;
            }

            private static Vector2 GenerateRandomPointAround(Vector2 point, float minDistance)
            {
                var angle = Random.Range(0f, 2f * Mathf.PI);
                var distance = minDistance * (1 + Random.Range(0f, 1f)); // Adjust distance multiplier
                return point + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            }

            private static bool IsValidPoint(Vector2 point, float minDistance, Vector2?[,] grid, int gridRadius, float cellSize)
            {
                // Calculate grid indices and clamp
                var gridX = Mathf.Clamp(gridRadius + Mathf.FloorToInt(point.x / cellSize), 0, grid.GetLength(0) - 1);
                var gridY = Mathf.Clamp(gridRadius + Mathf.FloorToInt(point.y / cellSize), 0, grid.GetLength(1) - 1);

                // Check neighboring cells for too-close points
                for (var y = Mathf.Max(0, gridY - 2); y <= Mathf.Min(grid.GetLength(1) - 1, gridY + 2); y++)
                {
                    for (var x = Mathf.Max(0, gridX - 2); x <= Mathf.Min(grid.GetLength(0) - 1, gridX + 2); x++)
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
