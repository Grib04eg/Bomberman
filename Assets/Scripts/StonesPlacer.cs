using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class StonesPlacer : MonoBehaviour
{
    [SerializeField] Grid grid;

    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var stone = transform.GetChild(i);
            var x = 1 + i % (grid.width / 2) * 2;
            var y = 1 + i / (grid.width / 2) * 2;
            stone.position = grid.GetPositionOnGrid(new Vector2Int(x, y));
            stone.GetComponent<SpriteRenderer>().sortingOrder = grid.height-y;
        }
    }
}
