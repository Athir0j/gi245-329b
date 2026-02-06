using UnityEngine;
using UnityEngine.AI;

public enum CharState
{
    Idle, Walk, Attack, Hit, Die, WalkToEnemy
}

public abstract class Character : MonoBehaviour
{
    protected NavMeshAgent navAgent;

    [SerializeField]
    protected Animator anim;
    public Animator Anim{get {return  anim;}}

    [SerializeField] 
    protected CharState state;
    public CharState State {get {return state;}}

    [SerializeField]
    protected int curHP = 10;
    public int CurHp {get {return curHP;}}

    [SerializeField]
    protected Character curCharTarget;

    [SerializeField]
    protected float attackRange = 2f;

    [SerializeField]
    protected float attackCoolDown = 2f;
    
    protected float attackTimer = 0f;


    void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

    }

    public void SetState(CharState s)
    {
        state = s;

        if (state == CharState.Idle)
        {
            navAgent.isStopped = true;
            navAgent.ResetPath();
        }
    }

    public void WalkToPosition(Vector3 dest)
    {
        if (navAgent != null)
        {
            navAgent.SetDestination(dest);
            navAgent.isStopped = false;
        }

        SetState(CharState.Walk);
    }

    protected void WalkUpdate()
    {
        float distance = Vector3.Distance(transform.position, navAgent.destination);
        if (distance <= navAgent.stoppingDistance)
            SetState(CharState.Idle);
    }

    //  move to attack an enermy 
    public void ToAttackCharacter(Character target)
    {
        if(curHP <= 0 || state == CharState.Die)
            return;
        //lock target
        curCharTarget = target;

        // start walking to enemy 
        navAgent.SetDestination(target.transform.position);
        navAgent.isStopped = false;

        SetState(CharState.WalkToEnemy);
    }

    protected void WalkToEnemyUpdate()
    {
        if(curCharTarget == null)
        {
            SetState(CharState.Idle);
            return;
        }

        navAgent.SetDestination(curCharTarget.transform.position);
        float distance = Vector3.Distance(transform.position,curCharTarget.transform.position);

        if(distance <= attackRange) 
        {
            SetState(CharState.Attack);
        }
    }

    void Update()
    {
        switch (state)
        {
            case CharState.Walk:
                WalkUpdate();
                break;
            

        }
    }
}

