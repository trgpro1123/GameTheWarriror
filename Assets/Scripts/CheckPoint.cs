using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public string Id;
    public  bool actived;

    private Animator animator;

    [ContextMenu("Genarate check point Id")]
    private void GenarateID(){
        Id=System.Guid.NewGuid().ToString();
    }

    private void Awake() {
        animator=GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.GetComponent<Player>()!=null){
            ActiveCheckPoint();
        }
    }
    public void ActiveCheckPoint(){
        if(actived==false)
            AudioManager.instance.PlaySFX(5);
        actived=true;
        animator.SetTrigger("active");
    }
}
