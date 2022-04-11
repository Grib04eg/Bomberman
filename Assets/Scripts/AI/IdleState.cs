using System.Collections;
using UnityEngine;

class IdleState : State
{
    public IdleState(AIController controller) : base(controller)
    {

    }
    Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right };
    public override IEnumerator Start()
    {
        if (!controller.isDead)
        {
            yield return new WaitForSeconds(Random.Range(1f, 3f));
            Vector2Int direction = Vector2Int.zero;
            while (direction == Vector2Int.zero)
            {
                direction = directions[Random.Range(0, 4)];
                if (!controller.CanMove(direction))
                    direction = Vector2Int.zero;
                yield return new WaitForFixedUpdate();
            }
            controller.SetState(new MoveState(controller, direction));
        }
    }
}
