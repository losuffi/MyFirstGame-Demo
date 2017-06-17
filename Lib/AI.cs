namespace AI
{
    using UnityEngine;
    using System.Collections;
    using System.Collections.Generic;
    using System;

    public abstract class AIState 
    {
        public GameObject Npc;
        private bool _IsInitState = false;
        public bool IsInitState
        {
            get { return _IsInitState; }
        }
        public delegate void ActOther();
        private AIAdvanced.FsmStateId _OwnFsmStateID;
        protected Dictionary<AIAdvanced.Transition, AIAdvanced.FsmStateId> map = new Dictionary<AIAdvanced.Transition, AIAdvanced.FsmStateId>();
        public void AddTransition(AIAdvanced.Transition newTransition, AIAdvanced.FsmStateId newFsmStateId)
        {
            if (map.ContainsKey(newTransition))
            {
                if (map[newTransition] == newFsmStateId)
                {
                    return;
                }
                else
                {
                    map[newTransition] = newFsmStateId;
                }
            }
            map.Add(newTransition, newFsmStateId);
        }
        public void DeleteTransition(AIAdvanced.Transition newTransition,AIAdvanced.FsmStateId newFsmStateId)
        {
            if (!map.ContainsKey(newTransition))
            {
                return;
            }
            map.Remove(newTransition);
        }
        public AIAdvanced.FsmStateId GetOutPutState(AIAdvanced.Transition newTransition)
        {
            if (map.ContainsKey(newTransition))
            {
                return map[newTransition];
            }
            return AIAdvanced.FsmStateId.None;
        }
        public AIAdvanced.FsmStateId OwnFsmStateID
        {
            get
            {
                return _OwnFsmStateID;
            }
        }
        public void SetOwnFsmStateID(AIAdvanced.FsmStateId initStateID)
        {
            _OwnFsmStateID = initStateID;
        }
        public abstract void Act(GameObject player,AIAdvanced aicompnet);
        public abstract void Reason(GameObject player, AIAdvanced aicompnet);
        public void SetAsInitState()
        {
            _IsInitState = true;
        }
    }
    public class AIAdvanced: MonoBehaviour
    {
        public enum Transition
        {
            None=0,
            SawPlayer,
            ReachPlayer,
            LostPlayer,
            BeAttacked,
            BadMood,
            NoHealth,
        }
        public enum FsmStateId
        {
            None=0,
            Patrolling,
            Chasing,
            Attacking,
            RunAway,
            Dead,
        }
        private List<AIState> fsmstates = new List<AIState>();
        private FsmStateId _CurrentStateID;
        public FsmStateId CurrentStateID
        {
            get
            {
                return _CurrentStateID;
            }
        }
        private AIState _CurrentState;
        public AIState CurrentState
        {
            get
            {
                return _CurrentState;
            }
        }
        public void AddState(AIState newState)
        {
            fsmstates.Add(newState);
        }
        public void DeleteState(AIState newState)
        {
            if (!fsmstates.Contains(newState))
                return;
            fsmstates.Remove(newState);
        }
        public void PerformTransition(Transition t)
        {
            FsmStateId outputState = CurrentState.GetOutPutState(t);
            if (outputState != FsmStateId.None)
            {
                AIState result = fsmstates.Find(alist =>
                  {
                      if (alist.OwnFsmStateID == outputState)
                          return true;
                      else
                          return false;
                  });
                if (result == null)
                    return;
                _CurrentState = result;
                _CurrentStateID = result.OwnFsmStateID;
            }
        }
        protected virtual void OnInit() { }
        protected virtual void OnFixedUpdate() { }
        public void StartAI()
        {
            _CurrentState = fsmstates.Find(alist =>
            {
                return alist.IsInitState;
            });
            _CurrentStateID = _CurrentState.OwnFsmStateID;
            OnInit();
            StartCoroutine(OtherUpdate());
        }
        IEnumerator OtherUpdate()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.02f);
                OnFixedUpdate();
            }
        }
    }
    public class AIPatrlState:AIState
    {
        public event ActOther ActO;
        public Vector3 PatrlPointer;
        private float PatrlR;
        private bool IsAttacked;
        private bool flag;
        private NavMeshAgent nma;
        public AIPatrlState(AIAdvanced.FsmStateId initState,Vector3 Pointer,float R,GameObject npc) 
        {
            IsAttacked = false;
            SetOwnFsmStateID(initState);
            PatrlPointer = Pointer;
            PatrlR = R;
            Npc = npc;
            Npc.GetComponent<Self_class>().Attacked += IsA;
            flag = true;
            nma = npc.GetComponent<NavMeshAgent>();
        }
        public override void Act(GameObject player, AIAdvanced aicompnet)
        {
            if (ActO != null)
                ActO();
            if (flag)
            {
                float e = UnityEngine.Random.Range(0, 2 * Mathf.PI);
                float x = 1.5f * PatrlR * Mathf.Cos(e);
                float z = 1.5f * PatrlR * Mathf.Sin(e);
                float y = Terrain.activeTerrain.SampleHeight(new Vector3(PatrlPointer.x + x, PatrlPointer.y, PatrlPointer.z + z));
                try
                {
                    if(nma.isOnNavMesh)
                        nma.destination = new Vector3(PatrlPointer.x + x, y, PatrlPointer.z + z);
                    else
                    {
                        GameObject.Find("Hero").transform.FindChild("Player").GetComponent<UnitSyncCmd>().CmdDes(Npc);
                    }
                    flag = false;
                }
                catch { flag = true; }
            }
            if (Vector3.Distance(PatrlPointer, Npc.transform.position) > PatrlR|| Npc.transform.position.x < 110 || Npc.transform.position.x > 420 || Npc.transform.position.z < 70 || Npc.transform.position.z > 420)
            {
                nma.destination = PatrlPointer;
                //Npc.transform.Rotate(Npc.transform.up * (90 + UnityEngine.Random.Range(0, 180)));
            }
            if (nma.hasPath==false)
            {
                flag = true;
            }
        }
        public override void Reason(GameObject player, AIAdvanced aicompnet)
        {
            Vector3 direction = player.transform.position - Npc.transform.position;
            if ((Vector3.Angle(direction, Npc.transform.forward) < 80))
            {
                RaycastHit hit;
                if (Physics.Raycast(Npc.transform.position, direction, out hit, 80))
                {
                    if (hit.collider.gameObject == player)
                    {
                        aicompnet.PerformTransition(AIAdvanced.Transition.SawPlayer);
                    }
                }
            }
            if (IsAttacked)
            {
                IsAttacked = false;
                aicompnet.PerformTransition(AIAdvanced.Transition.BeAttacked);
            }
        }
        void IsA(GameObject Mur)
        {
            IsAttacked = true;
            Npc.GetComponent<SqAI>().Murdeer = Mur;
        }
    }
    public class AIRunaway : AIState
    {
        private bool IsAttacked;
        private float Distance;
        private bool flag;
        private Vector3 Pointer;
        public event ActOther ActO;
        public AIRunaway(AIAdvanced.FsmStateId initState, GameObject npc,float RunawayDistance)
        {
            SetOwnFsmStateID(initState);
            IsAttacked = false;
            Npc = npc;
            Npc.GetComponent<Self_class>().Attacked += IsA;
            Distance=RunawayDistance;
            flag = true; Pointer = npc.transform.position;
        }
        public override void Act(GameObject player, AIAdvanced aicompnet)
        {
            if (ActO != null)
                ActO();
            if (flag)
            {
                float e = 0;
                if (Npc.GetComponent<SqAI>().Murdeer != null)
                {
                    e = Vector3.Angle(Npc.GetComponent<SqAI>().Murdeer.transform.position,Npc.transform.position);
                }
                else
                {
                    e = Vector3.Angle(player.transform.position, Npc.transform.position);
                }
                float x = 1.5f*Distance* Mathf.Cos(e);
                float z = 1.5f * Distance * Mathf.Sin(e);
                float y = Terrain.activeTerrain.SampleHeight(new Vector3(Pointer.x + x, Pointer.y, Pointer.z + z));
                Npc.GetComponent<NavMeshAgent>().destination = new Vector3(Pointer.x + x, y, Pointer.z + z);
                flag = false;
            }
            else
            {
                if (Npc.transform.position.x < 110 || Npc.transform.position.x > 420 || Npc.transform.position.z < 70 || Npc.transform.position.z > 420)
                {
                    Npc.GetComponent<NavMeshAgent>().destination = Pointer;
                    flag = true;
                    //Npc.transform.Rotate(Npc.transform.up * (90 + UnityEngine.Random.Range(0, 180)));
                }
                if (Vector3.Distance(Npc.transform.position, Pointer) > Distance)
                {
                    Pointer = Npc.transform.position;
                    flag = true;
                }
            }
        }
        public override void Reason(GameObject player, AIAdvanced aicompnet)
        {
            GameObject murdeer = Npc.GetComponent<SqAI>().Murdeer;
            if (IsAttacked)
            {
                IsAttacked = false;
                aicompnet.PerformTransition(AIAdvanced.Transition.BeAttacked);
            }
            if (Vector3.Distance(murdeer.transform.position, Npc.transform.position) > Distance)
            {
                aicompnet.PerformTransition(AIAdvanced.Transition.LostPlayer);
            }
            else
            {
                Vector3 direction = murdeer.transform.position - Npc.transform.position;
                RaycastHit hit;
                if (Physics.Raycast(Npc.transform.position, direction, out hit, Distance))
                {
                    if (hit.collider.gameObject != murdeer)
                    {
                        aicompnet.PerformTransition(AIAdvanced.Transition.LostPlayer);
                    }
                }
            }
        }
        void IsA(GameObject Mur)
        {
            IsAttacked = true;
            Npc.GetComponent<SqAI>().Murdeer = Mur;
        }
    }
}