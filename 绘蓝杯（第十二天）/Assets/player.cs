//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private float speed = 10;
    public Rigidbody you;
    //public byte move;
    public float jump = 6.5f;
    public bool isGround;
    public bool isWall;
    public Transform feet;
    public float check = 1.3f;
    public LayerMask isground;
    public LayerMask isspeciawall;
    public bool firstjump = true;
    public bool doublejump = true;
    public bool triblejump = true;
    public float jumpmax = 1;
    public bool abletodash = true;
    public bool chaoxiang = true;
    public bool dengqiangtiao=false;
    public bool Downing = false;
    public bool canying = false;
    public GameObject Liaoji;
    public bool liaojiqidong=false;
    public bool gongjuren;
    public GameObject Mainsheyingji;
    public GameObject sheyingji2;
    public Camera Sheyingji;
    public Camera Sheyingji2;

    [Header("时间控制参数")]
    public float activejumper = 1;
    public byte activedasher = 0;
    public float dashlimit=0;
    public float Liaojishixian = 0;
    // Start is called before the first frame update
    void Awake()
    {
        you = GetComponent<Rigidbody>();
        feet = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //x轴简单移动与朝向代码
        //move = Input.GetAxisRaw("Horizontal");
        you.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, you.velocity.y);
        if (you.velocity.x>0)chaoxiang=true ;
        if (you.velocity.x<0)chaoxiang=false;
        //喜闻乐见的三段大小跳与地面检测
        if (isWall == false && Downing == false)
        {
            if (Input.GetButtonUp("Jump") && doublejump == false && dengqiangtiao == false)
            {
                triblejump = false;
                activejumper = jumpmax;
            }
            if (Input.GetButtonUp("Jump") && firstjump == false && dengqiangtiao == false)
            {
                doublejump = false;
                activejumper = jumpmax;
            }
            if (Input.GetButtonUp("Jump")&&dengqiangtiao==false)
            {
                firstjump = false;
                activejumper = jumpmax;
            }
            if (Input.GetButtonDown("Jump") && triblejump == true && activejumper > 0)
            {
                you.velocity = new Vector2(you.velocity.x, jump);
            }
            if (Input.GetButton("Jump") && triblejump == true && activejumper > 0 && dengqiangtiao == false)
            {
                if (activejumper < 0.1) you.velocity = new Vector2(you.velocity.x, jump * 2);
                activejumper -= Time.deltaTime;
                //Debug.Log(activejumper);
            }
        }
       
        Collider[] colliders = Physics.OverlapSphere(feet.position, check, isground);
        if (colliders.Length > 0 && you.velocity.y < 1.5E-08 && you.velocity.y >-1.5E-08)
        {
              isGround = true;
             firstjump = true;
            doublejump = true;
            triblejump = true;
               Downing = false;
        }
        else isGround = false;

        //冲刺代码，主要在fixupdate那边
        if (Input.GetButtonDown("dash") && abletodash == true)
        {
            activedasher = 5;
            abletodash = false;   
        }     

        //爬墙
        Collider[] colliders2= Physics.OverlapSphere(feet.position, check, isspeciawall);
        if (colliders2.Length > 0)
        {
            isWall = true;
           // Debug.Log("1");
        }else
        {
            isWall = false;
           // Debug.Log("0");
        }
        if (isWall == true && Input.GetButton("Jump") == true) { dengqiangtiao = true; activejumper = jumpmax; }
        if (Input.GetButtonUp("Jump") == true) dengqiangtiao = false;

        //召唤僚机
        gongjuren = false;
        if (Input.GetButtonDown("liaoji") && liaojiqidong == true)
        {
            Mainsheyingji.SetActive(true);
            Liaoji.SetActive(false);
            sheyingji2.SetActive(false);
            liaojiqidong = false;
            gongjuren = true;
            //僚机在一定范围内可爆炸，代码回头整伤害的时候做
        }
        if (Input.GetButtonDown("liaoji") && liaojiqidong == false && gongjuren == false && Downing == false && Liaojishixian < 0.1)
        {
            Sheyingji.rect = new Rect(0, 0, 1, 1);
            Liaoji.SetActive(true);
            Mainsheyingji.SetActive(false);
            Liaojishixian = 5;
            liaojiqidong = true;
            Sheyingji2.rect = new Rect(0.7f, 0, 0.3f, 0.3f);
            Sheyingji2.depth = 1;
        }
        if(Liaojishixian >0) Liaojishixian-= Time.deltaTime;

        //下砸 限制下落速度
        if (Input.GetButton("Jump") == true && Input.GetAxisRaw("Vertical") == -1 && isGround == false) { Downing = true; activejumper = jumpmax; }
        if (Input.GetAxisRaw("Vertical") == 1) Downing = false;
       // if (Input.GetButtonDown("Jump") == true) Downing = false;
        if (Downing==true) you.velocity = new Vector2(you.velocity.x, -30) ;
        else if (you.velocity.y<-15) you.velocity = new Vector2(you.velocity.x, -15);

        //残影大改//加速
        if (Input.GetButtonDown("canying"))
        {
            if (canying == false)
            {
                speed = 15;
                canying = true;
            }
            else
            {
                canying = false;
                speed = 10;
            }
        }
    }



    private void FixedUpdate()
    {
        //残影
        if(canying==true|| activedasher>0||Downing) shadowpool.instance.GetFrompool();
        
        //冲刺
        if (activedasher > 0)
        {
           if(chaoxiang==true)you.velocity = new Vector2(50, 0);
            else you.velocity = new Vector2(-50, 0);
            activedasher--;
        }
        if (abletodash == false)
        {
            dashlimit++;
        }
        if (dashlimit > 29){
            abletodash = true;
            dashlimit = 0;
        }

        //爬墙
        if (isWall == true && Input.GetButton("Jump") == true && Downing == false)
        {
            you.velocity = new Vector2(you.velocity.x, 15);
        }
    }
}
