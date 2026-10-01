using NUnit.Framework;
using SnakeGame;
using UnityEngine;

public class SnakeModelTests
{
    [Test]
    public void InitialFoodIsOutsideSnake()
    {
        for (int seed = 0; seed < 100; seed++)
        {
            var model = new SnakeModel(seed);
            foreach (var cell in model.Cells) Assert.AreNotEqual(cell, model.Food);
        }
    }

    [Test]
    public void CannotReverseOrQueueTwoTurnsInOneStep()
    {
        var model = new SnakeModel(1);
        model.Turn(Vector2Int.left);
        model.Step();
        Assert.AreEqual(Vector2Int.right, model.Direction);
        model.Turn(Vector2Int.up);
        model.Turn(Vector2Int.left);
        model.Step();
        Assert.AreEqual(Vector2Int.up, model.Direction);
    }

    [Test]
    public void WallEndsGameAndStopsMovement()
    {
        var model = new SnakeModel(1);
        for (int i = 0; i < 10; i++) model.Step();
        Assert.IsTrue(model.IsOver);
        Vector2Int last = model.Cells[0];
        model.Step();
        Assert.AreEqual(last, model.Cells[0]);
    }

    [Test]
    public void EatingGrowsSnakeAndIncreasesScore()
    {
        var model = new SnakeModel(7);
        // Primero una columna libre a la derecha; luego alcanza la fila de comida.
        int targetX = model.Food.x;
        int targetY = model.Food.y;
        if (targetY == 10 && targetX < 10)
        {
            model.Turn(Vector2Int.up); model.Step();
            model.Turn(Vector2Int.left);
            while (model.Cells[0].x != targetX) model.Step();
            model.Turn(Vector2Int.down); model.Step();
        }
        else
        {
            if (targetY != 10)
            {
                model.Turn(targetY > 10 ? Vector2Int.up : Vector2Int.down);
                while (model.Cells[0].y != targetY) model.Step();
            }
            if (targetX != model.Cells[0].x)
            {
                model.Turn(targetX > model.Cells[0].x ? Vector2Int.right : Vector2Int.left);
                while (model.Cells[0].x != targetX) model.Step();
            }
        }
        Assert.IsFalse(model.IsOver);
        Assert.AreEqual(1, model.Score);
        Assert.AreEqual(4, model.Cells.Count);
        foreach (var cell in model.Cells) Assert.AreNotEqual(cell, model.Food);
    }
}
