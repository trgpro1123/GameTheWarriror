using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState playerState {get;private set;}

    public void Initialize(PlayerState _playerState){
        playerState=_playerState;
        playerState.Enter();
    }
    public void ChangeState(PlayerState _newPlayerState){
        playerState.Exit();
        playerState=_newPlayerState;
        playerState.Enter();
    }
}
