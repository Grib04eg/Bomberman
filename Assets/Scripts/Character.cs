using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Grid grid;
    [SerializeField] protected float speed;
    public Vector2Int gridPosition;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    Dictionary<Vector2Int, int> directions = new Dictionary<Vector2Int, int>() { { Vector2Int.up, 0 }, { Vector2Int.left, 1 }, { Vector2Int.down, 3 }, { Vector2Int.right, 2 } };
    protected bool Move(Vector2Int pos, TweenCallback onComplete = null)
    {

        if (!grid.EmptyCellOnGrid(pos))
            return false;
        if (directions.ContainsKey(pos - gridPosition))
            animator.SetInteger("direction", directions[pos - gridPosition]);
        gridPosition = pos;
        GetComponent<SpriteRenderer>().sortingOrder = grid.height - pos.y;
        transform.DOMove(grid.GetPositionOnGrid(pos), 1f / speed).onComplete += onComplete;
        return true;
    }

    public virtual void Die()
    {

    }
}
