using System.Collections;
using UnityEngine;

class MoveState : State
{
    Vector2Int direction;
    public MoveState(AIController controller, Vector2Int direction) : base(controller)
    {
        this.direction = direction;
    }

    public override IEnumerator Start()
    {
        controller.MoveInDirection(direction);
        while (controller.isMoving)
        {
            yield return new WaitForFixedUpdate();
        }
        controller.SetState(new IdleState(controller));
    }
}