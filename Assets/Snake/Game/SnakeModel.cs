using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnakeGame
{
    // Reglas independientes del dibujo: cada casilla mide una unidad.
    public sealed class SnakeModel
    {
        public const int Size = 20;
        private readonly List<Vector2Int> cells = new List<Vector2Int>();
        private readonly System.Random random;
        private Vector2Int direction = Vector2Int.right;
        private Vector2Int queuedDirection = Vector2Int.right;
        private bool turnQueued;

        public IReadOnlyList<Vector2Int> Cells => cells;
        public Vector2Int Direction => direction;
        public Vector2Int Food { get; private set; }
        public int Score { get; private set; }
        public bool IsOver { get; private set; }
        public bool Won { get; private set; }

        public SnakeModel(int? seed = null)
        {
            random = seed.HasValue ? new System.Random(seed.Value) : new System.Random();
            cells.Add(new Vector2Int(10, 10));
            cells.Add(new Vector2Int(9, 10));
            cells.Add(new Vector2Int(8, 10));
            PlaceFood();
        }

        public void Turn(Vector2Int next)
        {
            // Una sola orden entre pasos impide invertir al pulsar dos teclas rapido.
            if (IsOver || turnQueued || Mathf.Abs(next.x) + Mathf.Abs(next.y) != 1
                || next == direction || next == -direction) return;
            queuedDirection = next;
            turnQueued = true;
        }

        // Devuelve true solamente cuando come.
        public bool Step()
        {
            if (IsOver) return false;
            direction = queuedDirection;
            turnQueued = false;
            Vector2Int head = cells[0] + direction;
            bool eats = head == Food;
            int occupied = eats ? cells.Count : cells.Count - 1;
            bool hits = head.x < 0 || head.y < 0 || head.x >= Size || head.y >= Size;
            for (int i = 0; i < occupied; i++)
                hits |= cells[i] == head;
            if (hits)
            {
                IsOver = true;
                return false;
            }

            cells.Insert(0, head);
            if (!eats) cells.RemoveAt(cells.Count - 1);
            else
            {
                Score++;
                if (cells.Count == Size * Size) { IsOver = true; Won = true; }
                else PlaceFood();
            }
            return eats;
        }

        private void PlaceFood()
        {
            // Lista finita: nunca deja un bucle infinito cuando el tablero se llena.
            var free = new List<Vector2Int>();
            for (int y = 0; y < Size; y++)
                for (int x = 0; x < Size; x++)
                {
                    var cell = new Vector2Int(x, y);
                    if (!cells.Contains(cell)) free.Add(cell);
                }
            Food = free[random.Next(free.Count)];
        }
    }
}
