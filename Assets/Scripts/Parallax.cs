using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float parallaxEffect;
    private GameObject cam;
    private float xPosition;
    private float length;

    void Start()
    {
        cam=GameObject.Find("Main Camera");
        length=GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition=transform.position.x;
  
    }

    void Update()
    {
        float distenceMoved=cam.transform.position.x * parallaxEffect;
        float distanceToMove=cam.transform.position.x*(1-parallaxEffect);
        transform.position=new Vector3(xPosition+distanceToMove,transform.position.y);
        if(distenceMoved>xPosition+length) 
            xPosition+=length;
        else if(distenceMoved<xPosition-length) 
            xPosition-=length;
    }
}
