using System;
using System.Collections.Generic;
using System.Linq;

namespace KhoBau_BE.Application
{
    public record Cell(int R, int C);

    public class SolveResult
    {
        public double TotalFuel { get; set; }
        public List<(int r, int c, int value)> Path { get; set; } = new();
    }

    public class TreasureSolver
    {
        private static double Dist(Cell a, Cell b)
        {
            var dr = a.R - b.R;
            var dc = a.C - b.C;
            return Math.Sqrt(dr * dr + dc * dc);
        }

        public SolveResult Solve(int[][] matrix, int n, int m, int p)
        {
            var buckets = new List<Cell>[p + 1];
            for (int i = 0; i <= p; i++) buckets[i] = new List<Cell>();

            for (int r = 0; r < n; r++)
                for (int c = 0; c < m; c++)
                {
                    int val = matrix[r][c];
                    if (val < 1 || val > p) throw new ArgumentException($"Giá trị ngoài khoảng: {val}");
                    buckets[val].Add(new Cell(r, c));
                }

            var start = new Cell(0, 0);
            var prevCells = new List<Cell> { start };
            var prevCost = new List<double> { 0.0 };
            var parentIdx = new List<Dictionary<int, int>>();
            parentIdx.Add(new Dictionary<int, int>());

            for (int val = 1; val <= p; val++)
            {
                var curCells = buckets[val];
                if (curCells.Count == 0) throw new InvalidOperationException($"Không có ô có giá trị {val}");

                var curCost = new double[curCells.Count];
                var curParent = new Dictionary<int, int>();
                for (int i = 0; i < curCells.Count; i++) curCost[i] = double.PositiveInfinity;

                for (int i = 0; i < curCells.Count; i++)
                {
                    for (int j = 0; j < prevCells.Count; j++)
                    {
                        double cand = prevCost[j] + Dist(prevCells[j], curCells[i]);
                        if (cand < curCost[i])
                        {
                            curCost[i] = cand;
                            curParent[i] = j;
                        }
                    }
                }

                prevCells = curCells;
                prevCost = curCost.ToList();
                parentIdx.Add(curParent);
            }

            int bestIdx = 0;
            double best = prevCost[0];
            for (int i = 1; i < prevCost.Count; i++)
                if (prevCost[i] < best) { best = prevCost[i]; bestIdx = i; }

            var path = new List<(int r, int c, int value)>();
            int idx = bestIdx;
            for (int val = p; val >= 1; val--)
            {
                var cell = buckets[val][idx];
                path.Add((cell.R + 1, cell.C + 1, val)); 
                if (val > 1)
                {
                    idx = parentIdx[val][idx];
                }
                else
                {
                    path.Add((1, 1, 0));
                }
            }
            path.Reverse();

            return new SolveResult { TotalFuel = best, Path = path };
        }
    }
}