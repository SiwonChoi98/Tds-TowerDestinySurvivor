using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class State<T>
{
    protected int mecanimStateHash;
    protected StateMachine<T> _stateMachine;
    protected T _context;

    public State()
    {
    }
    public State(string mecanimStateName) : this(Animator.StringToHash(mecanimStateName))
    {
    }
    public State(int mecanimStateHash)
    {
        this.mecanimStateHash = mecanimStateHash;
    }

    internal void SetMachineAndContext(StateMachine<T> stateMachine, T context)
    {
        this._stateMachine = stateMachine;
        this._context = context;

        OnInitialized();
    }
    public virtual void OnInitialized() //셋팅
    { }

    public abstract void OnEnter(); //한번 입력


    public virtual void FixedUpdate(float deltaTime) //계속업데이트
    {
        
    }

    public abstract void OnExit(); //빠져나온다.

}
public class StateMachine<T>
{
    private T context;
    public event Action OnChangedState;

    private State<T> currentState; //현재상태
    public State<T> CurrentState => currentState;

    //private State<T> previousState; //전상태
    //public State<T> PreviousState => previousState;
    private Dictionary<System.Type, State<T>> states = new Dictionary<Type, State<T>>();
    public StateMachine(T context, State<T> initialState)
    {
        this.context = context;

        // Setup our initial state
        AddState(initialState);
        currentState = initialState;
        currentState.OnEnter();
    }
    public void AddState(State<T> state)
    {
        state.SetMachineAndContext(this, context);
        states[state.GetType()] = state;
    }
    public void FixedUpdate(float deltaTime)
    {
        currentState.FixedUpdate(deltaTime); //현재 상태 계속 업데이트
    }
    
    public R ChangeState<R>() where R : State<T>
    {
        var newType = typeof(R);
        if (currentState.GetType() == newType)
        {
            return currentState as R;
        }


        if (currentState != null)
        {
            currentState.OnExit();
        }
        
        //previousState = currentState;
        currentState = states[newType];

        currentState.OnEnter();
        
        if (OnChangedState != null)
        {
            OnChangedState();
        }
        return currentState as R;
    }
}