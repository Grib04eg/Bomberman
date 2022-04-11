using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Bomb : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.DOShakePosition(2.5f,new Vector2(0.1f,0.1f),15,90).onComplete += () => 
        {
            GetComponent<SpriteRenderer>().DOColor(Color.red, 0.2f).SetDelay(0.2f);
            transform.DOScale(1.5f, 0.5f).SetEase(Ease.InCubic).onComplete += () =>
            {
                Destroy(gameObject);
            };
        };
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
