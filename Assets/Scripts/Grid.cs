using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    [SerializeField] GameObject stonePrefab;
    [SerializeField] GameObject bombPrefab;
    [SerializeField] GameObject explosionPrefab;
    public Character[] characters;
    public Vector2 horizontalOffset;
    public Vector2 verticalOffset;
    public int width;
    public int height;

    private void Start()
    {
        for (int x = 1; x < width; x+=2)
        {
            for (int y = 1; y < height; y+=2)
            {
                var pos = GetPositionOnGrid(new Vector2Int(x, y));
                Instantiate(stonePrefab, pos, Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = height - y;
            }
        }
    }

    public Vector2 GetPositionOnGrid(Vector2Int gridPos)
    {
        return transform.position + new Vector3(gridPos.x * horizontalOffset.x + gridPos.y * verticalOffset.x, gridPos.y * verticalOffset.y + gridPos.x * horizontalOffset.y);
    }

    public bool EmptyCellOnGrid(Vector2Int gridPos)
    {
        if ((gridPos.x % 2 == 1 && gridPos.y % 2 == 1) || gridPos.x < 0 || gridPos.y < 0 || gridPos.x > width - 1 || gridPos.y > height - 1)
            return false;
        return true;
    }

    public void SpawnBomb(Vector2Int gridPos)
    {
        Instantiate(bombPrefab, GetPositionOnGrid(gridPos), Quaternion.identity).GetComponent<SpriteRenderer>().sortingOrder = height - gridPos.y;
        StartCoroutine(StartExplode(gridPos, 3));
    }

    Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right };
    IEnumerator StartExplode(Vector2Int pos, int length)
    {
        yield return new WaitForSeconds(3f);
        Explode(pos);
        for (int i = 0; i < length-1; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                StartCoroutine(ExplodeDirection(pos + directions[j], directions[j], length - 1));
            }
        }
    }

    IEnumerator ExplodeDirection(Vector2Int pos, Vector2Int dir, int length)
    {
        int iteration = 0;
        while (length > 0)
        {
            yield return new WaitForSeconds(0.5f);
            pos += dir * iteration;
            if (EmptyCellOnGrid(pos))
            {
                iteration++;
                Explode(pos);
            }
            length--;
        }
    }

    void Explode(Vector2Int pos)
    {
        Instantiate(explosionPrefab, GetPositionOnGrid(pos), Quaternion.identity);
        foreach (var character in characters)
        {
            if (character.gridPosition == pos)
                character.Die();
        }
    }
}
