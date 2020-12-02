using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liaoji : MonoBehaviour
{
    public static liaoji instance;
    private Transform playe;
    public GameObject Liaoji;
    public Rigidbody you;
    public LayerMask isground;
    public GameObject Mainsheyingji;
    public GameObject sheyingji2;
    public Camera Sheyingji2;
    public GameObject sheyingji;
    public Camera Sheyingji;

    // Start is called before the first frame update
    void Awake()
    {
        Liaoji.SetActive(false);
    }
    private void OnEnable()
    {
        playe = GameObject.Find("qianmian").transform;
        transform.position = playe.position;
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
       you.velocity = new Vector2(Input.GetAxisRaw("Liaoji Horizontal") * -20, Input.GetAxisRaw("Liaoji Vertical") * 20);
        Collider[] collider = Physics.OverlapSphere(Liaoji.transform.position, 1, isground);
        if (collider.Length > 0)
        {
            Mainsheyingji.SetActive(true);
            Liaoji.SetActive(false);
            sheyingji2.SetActive(false);
            //僚机被动坠毁与你主动触发爆炸不同，伤害会减弱并且重启僚机需要启动两次
        }
    
        //摄影机控制，哭了
        if (Input.GetKeyDown(KeyCode.E))
        {
           if(Sheyingji.rect.height == 1)
           {
                if (sheyingji2.active == true)
                {
                    sheyingji2.active = false;
                }
                else
                {
                    sheyingji2.active = true;
                }
            }
            else 
            {
                if (sheyingji.active == true)
                {
                    sheyingji.active = false;
                }
                else
                {
                    sheyingji.active = true;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            if (sheyingji2.active == true && sheyingji.active == true) 
            {
                if(Sheyingji2.depth == 1)
                { 
                    Sheyingji2.rect = new Rect(0, 0, 1, 1);
                    Sheyingji2.depth = -1;
                    Sheyingji.rect = new Rect(0.7f, 0, 0.3f, 0.3f);
                 }
                else
                { 
                    Sheyingji2.rect = new Rect(0.7f, 0, 0.3f, 0.3f);
                    Sheyingji2.depth = 1;
                    Sheyingji.rect = new Rect(0, 0, 1, 1);
                }
            
            }
            else 
            {
                if (Sheyingji2.depth == 1)
                {
                    Sheyingji2.rect = new Rect(0, 0, 1, 1);
                    Sheyingji2.depth = -1;
                    Sheyingji.rect = new Rect(0.7f, 0, 0.3f, 0.3f);
                    sheyingji2.SetActive(true);
                    sheyingji.SetActive(false);
                }
                else 
                {
                    Sheyingji2.rect = new Rect(0.7f, 0, 0.3f, 0.3f);
                    Sheyingji2.depth = 1;
                    Sheyingji.rect = new Rect(0, 0, 1, 1);
                    sheyingji.SetActive(true);
                    sheyingji2.SetActive(false);
                }
            }
        }

      
    }

     // private void FixedUpdate()
   // {


   // }

}
