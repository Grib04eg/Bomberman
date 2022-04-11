using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Character
{
    State currentState;
    public bool isMoving = false;
    public bool isDead = false;
    private void Start()
    {
        Move(gridPosition);
        SetState(new IdleState(this));
    }

    public bool CanMove(Vector2Int direction)
    {
        return grid.EmptyCellOnGrid(gridPosition + direction);
    }

    public void MoveInDirection(Vector2Int direction)
    {
        isMoving = true;
        Move(gridPosition + direction, () => { isMoving = false; });
    }

    Coroutine currentStateCoroutine;
    public void SetState(State state)
    {
        if (currentStateCoroutine != null)
            StopCoroutine(currentStateCoroutine);
        currentState = state;
        currentStateCoroutine = StartCoroutine(currentState.Start());
    }

    public override void Die()
    {
        isDead = true;
        SetState(new DieState(this));
    }

    public void SetAnim(string name, bool value)
    {
        GetComponent<Animator>().SetBool(name, value);
    }
}
