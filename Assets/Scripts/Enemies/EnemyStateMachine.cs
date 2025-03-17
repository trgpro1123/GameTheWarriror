using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine 
{
    public EnemyState enemyState {get;private set;}

    public void Initialize(EnemyState _enemyState){
        enemyState=_enemyState;
        enemyState.Enter();
    }
    public void ChangeState(EnemyState _newEnemyState){
        enemyState.Exit();
        enemyState=_newEnemyState;
        enemyState.Enter();

    }
    
}
