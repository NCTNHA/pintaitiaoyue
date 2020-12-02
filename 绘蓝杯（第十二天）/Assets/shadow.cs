using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    private Transform playe;
    private SpriteRenderer Sprite;
    private SpriteRenderer playerSprite;
    private Color color;

    [Header("时间控制参数")]
    public float activeTime = 0.2f;
    private float activeStart;

    [Header("不透明度控制")]
    private float alpha;
    public float alphaset = 1;
    public float alphaMultiplier = 0.8f;


    private void OnEnable()
    {
        playe = GameObject.Find("back").transform;
        //GameObject.FindGameObjectWithTag("player").transform;
        Sprite = GetComponent<SpriteRenderer>();
        playerSprite = playe.GetComponent<SpriteRenderer>();
        alpha = alphaset;
        Sprite.sprite = playerSprite.sprite;
        transform.position = playe.position;
        transform.localScale = playe.localScale;
        transform.rotation = playe.rotation;

        activeStart = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("1");

        if (Time.time >= activeStart + activeTime)
        {
            shadowpool.instance.Returnpow(this.gameObject);
        }
    }
    void FixedUpdate() { 
    alpha *= alphaMultiplier;
        Sprite.color = new Color(1,0.5f,0.5f, alpha);
    }

}
