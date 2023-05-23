using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    
    public Rigidbody2D body;
    private Animator anim;
    private bool isGrounded;
    Vector2 currentVelocity;
    public GameObject objects;
    public CapsuleCollider2D CapsuleCollider2D;
    public GameObject scoretext;
     TextMeshProUGUI TextMeshProObject;
     public float score;
    
    private void Awake()
    {
        isGrounded = true;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        TextMeshProObject = scoretext.GetComponent<TextMeshProUGUI>();

    }

    void Start()
    {
        speed = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        
        anim.SetBool("isGrounded",isGrounded);
        float horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput*speed*1.2f, body.velocity.y);
        
        if (currentVelocity.x > 0.01f||horizontalInput > 0.01f)
        {
            
            transform.localScale=new Vector3(5,5,1);
        }else if (horizontalInput < -0.01f||currentVelocity.x < -0.01f)
        {
           
            transform.localScale = new Vector3(-5, 5, 1);
            
        }
        
        
        
        if(isMoving())
        {
            anim.SetFloat("HoriSpeed",Mathf.Abs(body.velocity.x));
           
        }
        else
        {
            anim.SetFloat("HoriSpeed",0);
        }
        
        
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        
        currentVelocity = body.velocity;


        
    }

    private void FixedUpdate()
    {
        
    }

    bool isMoving()
    {
        if (body.velocity.x != 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Jump()
    {
        if(isGrounded==true){
        body.velocity = new Vector2(body.velocity.x, speed*2f);
        isGrounded = false;
        anim.SetBool("isGrounded",false);
        CapsuleCollider2D.size = new Vector2(0.21f, 0.21f);

        }
    }
    

    private void OnCollisionEnter2D(Collision2D col)
    {
        
        if (col.gameObject.tag == "Platform" || col.gameObject.tag == "ÇUBUK YÜZEY")
        {
            if(body.velocity.y<=7){
            isGrounded = true;
            CapsuleCollider2D.size = new Vector2(CapsuleCollider2D.size.x, 0.2904f);
            anim.SetBool("isGrounded",true);
            }

            if (isGrounded == true)
            {
                 score = body.position.y;
                int numInt = (int)Math.Ceiling(score);
                if(numInt>0){
                    
                TextMeshProObject.text ="Score: "+numInt ;
                }
            }
            
        }
        if (col.gameObject.tag == "Wall")
        {
            
            Debug.Log("DUVAAR");
            var speed = currentVelocity.magnitude;
            var direction = Vector3.Reflect(currentVelocity.normalized, col.contacts[0].normal);

            body.velocity = new Vector2(20f, body.velocity.y);



        }

        
    }

    
}
