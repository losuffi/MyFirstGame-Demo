using UnityEngine;
using System.Collections;

public class Monster : MonoBehaviour
{
    public Transform Mo;
    public float HoriSpeed;
    public float VertSpeed=0;
    public float TurnSpeed = 180;
    public bool isMove=false;
    public bool isAttackDone=true;
    public int AttackKind=0;
    public Transform Brain;
    public int ItemId=0;
    void Awake()
    {
        Mo = this.transform;
        Brain = this.transform.FindChild("AI").FindChild("Brain");
    }
    void Start()
    {
        HoriSpeed = Mo.GetComponent<Self_class>().s_speed;
    }
    void Animator()
    {
        if (HoriSpeed > 0)
        {
            Mo.GetComponent<Animator>().SetInteger("MoStatus", 1);
        }
        else
        {
            Mo.GetComponent<Animator>().SetInteger("MoStatus", 0);
        }
    }
    void Update()
    {
        if (!Mo.GetComponent<CharacterController>().isGrounded)
        {
            VertSpeed -= 98 * Time.deltaTime;
            Mo.GetComponent<CharacterController>().Move(Mo.up * VertSpeed * Time.deltaTime);
        }
        else
        {
            VertSpeed = 0;
            if (this.transform.FindChild("ejector"))
            {
                GameObject.Destroy(this.transform.FindChild("ejector").gameObject);
                this.transform.GetComponent<Self_class>().s_speed = this.transform.GetComponent<Self_class>().s_speedInit;
            }
        }
    }
    public void Move()
    {
        Mo.GetComponent<Animator>().SetInteger("MoStatus", 1);
        Mo.GetComponent<CharacterController>().Move(Mo.forward * HoriSpeed * Time.deltaTime);
    }
    public void Idel()
    {
        Mo.GetComponent<Animator>().SetInteger("MoStatus", 0);
    }
    public void TurnDirection()
    {
        Mo.Rotate(TurnSpeed * Mo.up * Time.deltaTime);
    }
    public void Attack()
    { 
        if (isAttackDone)
        {
            Mo.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(101, Mo);
            isAttackDone = false;
        }
    }
    public void AttackStatus(int status)
    {
       Mo.GetComponent<Animator>().SetBool("isAttack", false);
       isAttackDone = (status >= 1 ? true : false);

    }
    public void Spell(int sid)
    {
        Mo.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(sid, Mo);
    }
    void Spell_end(int status)
    {
        Mo.GetComponent<Animator>().SetBool("isSpell_1", false);
    }
    public void isAttacked(Transform Murderer)
    {
        Brain.GetComponent<AiBrain>().isAttacked(Murderer);
    }
    public void Dead()
    {
        Destroy(Brain.gameObject);
        ItemBe();
        StartCoroutine(Deadwork());
    }
    void ItemBe()
    {
        if (ItemId > 20000)
        {
            GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(Mo.position, ItemId);
        }
    }
    IEnumerator Deadwork()
    {
        yield return new WaitForSeconds(1.3f);
        Destroy(this.transform.gameObject);
    }
}
