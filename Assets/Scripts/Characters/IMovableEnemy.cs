using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Implemented by enemies that can move.
/// </summary>
public interface IMovableEnemy
{
    void ChangeMoveDirection(Vector2 newMoveDirection);
}
