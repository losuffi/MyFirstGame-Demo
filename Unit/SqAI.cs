using UnityEngine;
using System.Collections;
using AI;
public class SqAI : AIAdvanced {
    private GameObject _murdeer;
    public GameObject Murdeer
    {
        get
        {
            return _murdeer;
        }
        set
        {
            _murdeer = value;
        }
    }
    public GameObject TargetPlayer;
    private int a,num;
    private NavMeshAgent agent;
    private Animator ani;
    public void ConstructFsm()
    {
        agent = GetComponent<NavMeshAgent>();
        AIPatrlState patrol = new AIPatrlState(FsmStateId.Patrolling,this.transform.position,30,this.gameObject);
        patrol.AddTransition(Transition.BeAttacked, FsmStateId.RunAway);
        patrol.AddTransition(Transition.NoHealth, FsmStateId.Dead);
        patrol.ActO += Patrolling;
        patrol.SetAsInitState();
        AddState(patrol);
        AIRunaway runaway = new AIRunaway(FsmStateId.RunAway, this.gameObject,20);
        runaway.AddTransition(Transition.BeAttacked, FsmStateId.RunAway);
        runaway.AddTransition(Transition.LostPlayer, FsmStateId.Patrolling);
        runaway.AddTransition(Transition.NoHealth, FsmStateId.Dead);
        runaway.ActO += RunAway;
        AddState(runaway);
        StartAI();
        this.GetComponent<Self_class>().EvisDead += End;
    }
    protected override void OnInit()
    {
        a = 0;
        num = 0;
        ani = GetComponent<Animator>();
        base.OnInit();
    }
    protected override void OnFixedUpdate()
    {
        if (a < 10)
        {
            a++;
            return;
        }
        if (AICenter.Ins.MaxIndex < 0)
            return;
        if (num > AICenter.Ins.MaxIndex)
            num = 0;
        GameObject TargetPlayer = AICenter.Ins.Getplayer(num);
        if(TargetPlayer==null)
            return;
        CurrentState.Reason(TargetPlayer, this);
        CurrentState.Act(TargetPlayer, this);
        num++;
    }
    void Move()
    {
        if (this.transform.position.x < 110 || this.transform.position.x > 420 || this.transform.position.z < 70 || this.transform.position.z > 420)
        {
            transform.Rotate(transform.up * (90.1f + Random.Range(0.9f,179.9f)));
        }
        this.gameObject.GetComponent<Animator>().SetInteger("MoStatus", 1);
        agent.destination = transform.position + (transform.forward * GetComponent<Self_class>().s_speed * 0.02f);
    }
    void RunAway()
    {
        ani.SetInteger("MoStatus", 1);
    }
    void Patrolling()
    {
        ani.SetInteger("MoStatus", 1);
    }
    void End()
    {
        this.enabled = false;
    }
}
