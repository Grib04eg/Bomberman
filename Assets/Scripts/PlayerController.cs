using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerController : Character
{
    [SerializeField] Image bombCoverImage;
    [SerializeField] TextMeshProUGUI bombTimerText; 
    bool everythingUnderControl = true;
    bool bombUnderControl = true;
    float bombTimer = 0f;
    void Start()
    {
        Move(new Vector2Int(0,0));
    }
    Vector2Int nextMove = Vector2Int.zero;
    void Update()
    {
        if (bombTimer > 0)
        {
            bombTimer -= Time.deltaTime;
            bombTimerText.text = bombTimer.ToString("0");
            bombCoverImage.fillAmount = bombTimer/4f;
            if (bombTimer <= 0)
            {
                bombCoverImage.fillAmount = 0;
                bombTimerText.text = "";
                bombUnderControl = true;
            }
        }
        if (!everythingUnderControl)
        {
            return;
        }
        if (nextMove != Vector2Int.zero)
        {
            if (Move(gridPosition + nextMove, OnMoveEnd))
                everythingUnderControl = false;
            nextMove = Vector2Int.zero;
            return;
        }
    }
    Vector2Int[] directions = new Vector2Int[] { Vector2Int.up, Vector2Int.left, Vector2Int.down, Vector2Int.right };
    public void ControlMove(int dir)
    {
        if (everythingUnderControl)
        {
            if (Move(gridPosition + directions[dir], OnMoveEnd))
                everythingUnderControl = false;
        } else
        {
            nextMove = directions[dir];
        }
    }

    public void ControlBomb()
    {
        if (bombUnderControl)
        {
            bombUnderControl = false;
            grid.SpawnBomb(gridPosition);
            bombTimer = 4;
        }
    }

    void OnMoveEnd()
    {
        everythingUnderControl = true;
    }

    public override void Die()
    {
        SceneManager.LoadScene(0);
    }
}