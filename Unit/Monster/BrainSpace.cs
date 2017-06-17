using UnityEngine;
using System.Collections;
public class BrainSpace : MonoBehaviour {
    //****************************************************公共类
    public Transform[] Acts;
    public enum Mission
    {
        Idel,
        Patrol,
        Chase,
        Escape,
        Patrol_Guard,
    }
    public enum Status
    {
        isDone,
        isFail,
        isStart,
        isGoing,
    }
    public enum Nature
    {
        Aggressive,
        Meek,
        Guard,
    }
    public Nature Character;
    public Mission NowMission;
    public Status NowMissionStatus;
    public float StartTimel;
    //----------------------------------------------------------------------!

    //**********************************************动作类
    //******目光广度********
    public float InductorAngles=150;
    public float PatrolInductorDistance = 20;
    public float GuardRadius = 20;
    public Vector3 GuardInitPos;
    public Vector3 GuardPos;
    public Transform PatrolTarget;   //----------存储为巡逻发现的目标
    public Vector3 targetPos;         //目标坐标 y=0;
    //******转向********
    public int ActTurnLevel;
    public int ActTurnBuffer;
    //-----------------
    //****Idel**********
    public bool isIdel=false;           //------------用于巡逻
    //-----------------
    //*****Spell*********
    public Hashtable mSpell = new Hashtable();
    public byte mHaveItemAddr = 0;
    public byte mItemAddrMin = 1;
    public float DecideTime;                 //---------施放何种技能
    public int DecideResult;
    //----------------------------------------------------------------------!
    public void ActReg()
    {
        Acts[0] = this.transform.FindChild("TurnDirection");
        Acts[1] = this.transform.FindChild("Move");
        Acts[2] = this.transform.FindChild("Idel");
        Acts[3] = this.transform.FindChild("TurnDirectionFree");
        Acts[4] = this.transform.FindChild("Attack");
        Acts[5] = this.transform.FindChild("Spell");
    }
    public void StudySpell(int sid)
    {
        mHaveItemAddr++;
        mSpell.Add(mHaveItemAddr, sid);
    }
}
