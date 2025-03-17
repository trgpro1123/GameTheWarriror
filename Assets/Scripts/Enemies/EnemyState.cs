using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState 
{
    

    protected EnemyStateMachine stateMachine;
    protected Enemy enemyBase;
    protected string animateBoolName;
    protected Rigidbody2D rb;
    protected bool triggerCalled;
    protected float stateTimer;

    public EnemyState(Enemy _enemyBase,EnemyStateMachine _enemyStateMachine,string _animateBoolName)
    {
        stateMachine=_enemyStateMachine;
        this.enemyBase=_enemyBase;
        animateBoolName=_animateBoolName;
    }

    public virtual void Uddate(){
        stateTimer-=Time.deltaTime;
    }
    public virtual void Enter(){
        enemyBase.animator.SetBool(animateBoolName,true);
        rb=enemyBase.rb;
        triggerCalled=false;
    }
    public virtual void Exit(){
        enemyBase.animator.SetBool(animateBoolName,false);
        enemyBase.AssignLastAnimBoolName(animateBoolName);
    }
    public void AnimationFininshTrigger(){
        triggerCalled=true;
    }

}
