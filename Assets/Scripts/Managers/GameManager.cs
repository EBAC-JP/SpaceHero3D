using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {

    public enum GameStates {
        INTRO,
        GAMEPLAY,
        PAUSE,
        WIN,
        LOSE
    }
    public StateMachine<GameStates> stateMachine;

    void Start() {
        InitStateMachine();
    }

    void InitStateMachine() {
        stateMachine = new StateMachine<GameStates>(GameStates.INTRO, new StateBase());
        stateMachine.RegisterState(GameStates.GAMEPLAY, new StateBase());
        stateMachine.RegisterState(GameStates.PAUSE, new StateBase());
        stateMachine.RegisterState(GameStates.WIN, new StateBase());
        stateMachine.RegisterState(GameStates.LOSE, new StateBase());
    }

}
