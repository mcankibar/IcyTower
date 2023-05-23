using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform target;
    public Camera Camera;
    public GameObject bg1;
    public GameObject bg2;
    public GameObject backGround;
    public GameObject wall;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public GameObject objects;
    public GameObject obje;
    public GameObject gameovertext;
    public GameObject scoretext;
    TextMeshProUGUI TextMeshProObject;
    public PlayerController PlayerController;
    public Objects ObjectsScript;
    private GameObject adjustedObject;
    public Component[] ChangedWallSprites;
   
    // Start is called before the first frame update
    private void Awake()
    {
       
        gameovertext.SetActive(false);
        TextMeshProObject = scoretext.GetComponent<TextMeshProUGUI>();
        TextMeshProObject.text = "Score: 0";
        CreateObject();
        
    }

    void Start()
    {
        
        
        for (int i = 0; i < 9; i++)
        {
            CreateObject();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y > 0 && transform.position.y - target.position.y > 4)
        {
            gameovertext.SetActive(true);
            Time.timeScale = 0;
            
        }
        if(target.position.y>transform.position.y){
            Vector3 targetPos = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, targetPos.y, transform.position.z);
        }
        else if (transform.position.y > 0)
        {
            transform.position +=new Vector3(0, 1 * Time.deltaTime,0);
        }
        
        if (Camera.transform.position.y > bg2.transform.position.y+2f)
        {
            createBackGround();
            
        }
        if (Camera.transform.position.y > wall3.transform.position.y-10f)
        {
            createFromWall3();
            createFromWall4();
            
        }
        
        if (Camera.transform.position.y > objects.GetComponent<Objects>().objectList[objects.GetComponent<Objects>().
                objectList.Count-1].transform.position.y - 3)
        {
            
            AdjustObject();
            AdjustObject();
            AdjustObject();
            
            
        }

        
        

        
            
        
    }

    private void FixedUpdate()
    {
        
        //camera
        



    }

    void createBackGround()
    {
        GameObject go = GameObject.Instantiate(bg2);
        go.transform.SetParent(backGround.transform);
        go.GetComponent<Transform>().localScale = bg2.GetComponent<Transform>().localScale;
        go.GetComponent<Transform>().position = bg2.GetComponent<Transform>().position;
        go.transform.position = new Vector3(go.transform.position.x, go.transform.position.y + 15.75f,
            go.transform.position.z);
        
        Destroy(bg1);
        bg1 = bg2;
        bg2 = go;

    }

    void createFromWall3()
    {
        GameObject newWall = GameObject.Instantiate(wall3);
        newWall.transform.SetParent(wall.transform);
        newWall.GetComponent<Transform>().localScale = wall3.GetComponent<Transform>().localScale;
        newWall.GetComponent<Transform>().position = wall3.GetComponent<Transform>().position;
        newWall.transform.position = new Vector3(newWall.transform.position.x, wall3.transform.position.y + 14.48f,
            newWall.transform.position.z);
        Destroy(wall1);
        wall1 = wall3;
        wall3 = newWall;
        
        if (PlayerController.score > 100)
        {
            int children = wall3.transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                wall3.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    wall.GetComponent<WallScript>().wallSprites[1];
            }
        }
        if (PlayerController.score > 200)
        {
            int children = wall3.transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                wall3.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    wall.GetComponent<WallScript>().wallSprites[2];
            }
        }
        
        
        
        

    }

    void createFromWall4()
    {
        GameObject newWall2 = GameObject.Instantiate(wall4);
        newWall2.transform.SetParent(wall.transform);
        newWall2.GetComponent<Transform>().localScale = wall4.GetComponent<Transform>().localScale;
        newWall2.GetComponent<Transform>().position = wall4.GetComponent<Transform>().position;
        newWall2.transform.position = new Vector3(newWall2.transform.position.x, wall4.transform.position.y + 14.48f,
            newWall2.transform.position.z);
        Destroy(wall2);
        wall2 = wall4;
        wall4 = newWall2;
        
        if (PlayerController.score > 100)
        {
            int children = wall4.transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                wall4.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    wall.GetComponent<WallScript>().wallSprites[1];
            }
        }
        if (PlayerController.score > 200)
        {
            int children = wall4.transform.childCount;
            for (int i = 0; i < children; ++i)
            {
                wall4.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite =
                    wall.GetComponent<WallScript>().wallSprites[2];
            }
        }

    }
    
    void CreateObject()
    {
        GameObject gameObject = GameObject.Instantiate(obje);
        gameObject.transform.parent = objects.transform;
        Random s = new Random();
        int randomscale = s.Next(7, 10);
        gameObject.transform.localScale =new Vector3(randomscale,9,this.transform.localScale.z);
        gameObject.GetComponent<SpriteRenderer>().sprite = objects.GetComponent<Objects>().objectSprites[0];

        Random r = new Random();
        int rInt = r.Next(-6, 7);

        gameObject.transform.position = new Vector3(rInt,
                objects.GetComponent<Objects>().objectList[objects.GetComponent<Objects>().objectList.Count-1].transform.position.y + 2,gameObject.transform.position.z);

        objects.GetComponent<Objects>().objectList.Add(gameObject);
        int index = objects.GetComponent<Objects>().objectList.Count ;
        gameObject.name = "Object"+index.ToString();
    }
    
    void AdjustObject()
    {
        adjustedObject = objects.GetComponent<Objects>().objectList[0];
        Random s = new Random();
        int randomscale = s.Next(7, 10);
        adjustedObject.GetComponent<SpriteRenderer>().sprite = ObjectsScript.objectSprites[0];

        Random r = new Random();
        int rInt = r.Next(-6, 7);

        objects.GetComponent<Objects>().objectList[0].transform.position = new Vector3(rInt,
            objects.GetComponent<Objects>().objectList[objects.GetComponent<Objects>().objectList.Count-1].transform.position.y + 2,adjustedObject.transform.position.z);
        
        if (PlayerController.score > 100)
        {
            adjustedObject.GetComponent<SpriteRenderer>().sprite = ObjectsScript.objectSprites[1];
        }
        if (PlayerController.score > 200)
        {
            adjustedObject.GetComponent<SpriteRenderer>().sprite = ObjectsScript.objectSprites[2];
        }
        
        
        objects.GetComponent<Objects>().objectList.RemoveAt(0);
        objects.GetComponent<Objects>().objectList.Add(adjustedObject);
        
    }

    
}
