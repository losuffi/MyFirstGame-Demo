using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class player :  NetworkBehaviour {
    public GameObject Weapon;
    public GameObject Weapon_Init;
    //---------------------------------------!
    public delegate void IsGround();
    public delegate void IsWater();
    public event IsGround ig;
    public event IsWater iw;
    public Transform cplayer;
    public Animator Ani;
    public float speed,Ispeed;
    public Vector3 dir;
    public float J_time;
    public float JumpSpeed;
    public int bt;
    private int Dead_status;
    private bool isLife=true;
    public int Spell_status;
    public bool IsAttack = true;
    private bool IsFallWater = false;
    private bool IsHandleModemouse = false;
    private Vector3 _derectmove;
    private float _verticalview, _horizontalview;
    public float MouseSpeed;
    public GameObject testprefab;
    public GameObject playcamera;
    //--------------------------------------!
    //功能按鍵控制開關
    //------------------------------------~
	void Awake()
    {
        speed = 0;
        Ispeed = this.transform.GetComponent<Self_class>().s_speed;
        dir = Vector3.zero;
        JumpSpeed = 0;
        Dead_status = 2;
        Spell_status = 2;
    }
    void Start()
    {
        if (isLocalPlayer)
        {
            this.transform.name = "Player";
            if (isServer)
            {   
                GameManager._ins.reg();
            }
            else
            {
                GameManager._ins.clientReg();
            }
            LockMouseAndScreen();
            MouseSpeed = 320f;
            StartCoroutine(MouseScan());
        }
        else
        {
            this.transform.name = "RomotePlayer";
            GameManager._ins.RomoteReg();
            this.enabled = false;
        }
    }
    public void player_act_cast(int spell_id)
    {
        if (Spell_status != 1)
        {
            Spell_status = 1;
            this.transform.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(spell_id, this.transform);
        }

    }
    void Spell_end(int status)
    {
        Ani.SetBool("isSpell_1", false);
        Spell_status = status;
    }
    public void InsWeapon(int weaponindex)
    {
        transform.GetComponent<UnitSyncCmd>().CmdInsWeapon(weaponindex, transform.position + 3 * transform.forward, Quaternion.identity, this.gameObject);
    }
	void Update () {
        CharacterController controller = GetComponent<CharacterController>();
        if (!controller.isGrounded)
        {
            if (IsFallWater)
                JumpSpeed = -1;
            else
                JumpSpeed -= 90 * Time.deltaTime;
            controller.Move(cplayer.up * JumpSpeed * Time.deltaTime);
        }
        else
        {
            JumpSpeed = 0;
            if (ig != null&&isLocalPlayer)
            {
                ig();
            }
        }
        ControlHandle();
    }
    public void Move()
    {
        if (IsFallWater)
            _derectmove *= 0.5f;
        CharacterController controller = GetComponent<CharacterController>();
        dir = (_derectmove * speed) + 0.1f*cplayer.up * JumpSpeed;
        controller.Move(dir * Time.deltaTime);
        Ani.SetFloat("Speed", speed);
    }
    public void Jump()
    {
        if (ig != null)
            return;
        CharacterController controller = GetComponent<CharacterController>();
        speed = 2;
        JumpSpeed = 25;
        if (IsFallWater)
            JumpSpeed = 6;
        if (Ispeed == 0)
        {
            controller.Move(cplayer.up * JumpSpeed * Time.deltaTime);
            Ani.SetFloat("Speed", speed);
        }
        else
        {
            Move();
        }
    }
    public void Idel()
    {
        speed = 0;
        Ispeed = 0;
        Ani.SetFloat("Speed", speed);
    }
    public void Dead()
    {
        isLife = false;
        Ispeed = 0;
        speed = 0;
        if (Dead_status == 2)
        {
            Ani.SetBool("isAttack", false);
            Ani.SetFloat("Speed", speed);
            Ani.SetBool("isLife", isLife);
            Dead_status = 1;
        }
        Invoke("StopAni",.5f); 
    }
    void StopAni()
    {
        Ani.StopPlayback();
    }
    public void Attack()
    {
        if (!IsAttack)
            return;
        IsAttack = false;
        this.transform.parent.FindChild("Spell").GetComponent<Spell_cast>().casting(101, this.transform);
    }
    public void Swim()
    {
        if (!IsFallWater)
            IsFallWater = true;
        StartCoroutine(FallWater());
    }
    void SystemKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            transform.GetComponent<Self_class>().s_Canvas.FindChild("Center").GetComponent<CanvasCenter>().OpenBag();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            transform.GetComponent<Self_class>().s_Canvas.FindChild("Center").GetComponent<CanvasCenter>().OpenPerson();
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            transform.GetComponent<Self_class>().s_Canvas.FindChild("Center").GetComponent<CanvasCenter>().OpenMaker();
        }
        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            LockMouseAndScreen();
        }
    }
    void OnGUI()
    {
        if (!isLocalPlayer)
            return;
        if (Input.anyKeyDown)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                Pan_shortcut._sc.KeyTrig(e.keyCode, this.gameObject);
            }
        }
    }
    IEnumerator FallWater()
    {
        yield return new WaitForSeconds(0.01f);
        if (iw != null&&isLocalPlayer)
        {
            iw();
        }
        IsFallWater = false;
    }
    IEnumerator MouseScan()
    {
        while (true)
        {
            if (IsHandleModemouse)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            _verticalview = Input.GetAxis("Mouse Y") * -MouseSpeed;
            _horizontalview = Input.GetAxis("Mouse X") * MouseSpeed;
            cplayer.Rotate(0, _horizontalview * Time.fixedDeltaTime, 0);
            if(!(playcamera.transform.eulerAngles.x>40&& playcamera.transform.eulerAngles.x < 300))
            {
                playcamera.transform.Rotate(_verticalview * Time.fixedDeltaTime, 0, 0);
            }
            else
            {
                if(playcamera.transform.eulerAngles.x > 40 && playcamera.transform.eulerAngles.x < 90 && _verticalview < 0)
                {
                    playcamera.transform.Rotate(_verticalview * Time.fixedDeltaTime, 0, 0);
                }
                else if(playcamera.transform.eulerAngles.x<300&& playcamera.transform.eulerAngles.x > 270 && _verticalview > 0)
                {
                    playcamera.transform.Rotate(_verticalview * Time.fixedDeltaTime, 0, 0);
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }
    void ControlHandle()
    {
        if (!isLife)
            return;
        if (Input.GetKey(KeyCode.W))
        {
            speed = 1;
            Ispeed = this.transform.GetComponent<Self_class>().s_speed;
            _derectmove = cplayer.forward * Ispeed;
            Move();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            speed = 1;
            Ispeed = -this.transform.GetComponent<Self_class>().s_speed;
            _derectmove = cplayer.forward * Ispeed;
            Move();
        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Idel();
        }
        if ((Input.GetKey(KeyCode.Space)))         //Junp
        {
            if (GetComponent<CharacterController>().isGrounded || IsFallWater)
                Jump();
        }
        if (Input.GetKey(KeyCode.D))
        {
            speed = 1;
            Ispeed = this.transform.GetComponent<Self_class>().s_speed;
            _derectmove = cplayer.right * Ispeed;
            Move();
        }
        else if (Input.GetKey((KeyCode)97))
        {
            speed = 1;
            Ispeed = -this.transform.GetComponent<Self_class>().s_speed;
            _derectmove = cplayer.right * Ispeed;
            Move();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            TestItem(1);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            TestItem(2);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            TestItem(3);
        }
        if (!IsHandleModemouse && Input.GetKeyUp(KeyCode.Mouse0))
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (transform.FindChild("buildpos") != null && transform.FindChild("buildpos").GetComponent<Building>().IsTrueBuild())
            {
                transform.FindChild("buildpos").GetComponent<Building>().SetBuild(this.gameObject);
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            GetComponent<player_pocket>().dropall();
        }
        SystemKeyboard();
    }
    void TestItem(int a)
    {
        if (a == 1)
        {
            for(int i = 0; i < 9; i++)
            {
                GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(transform.position, 20001+i);
            }
        }
        if (a == 2)
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(transform.position, 20010+i);
            }
        }
        if (a == 3)
        {
            for (int i = 0; i < 8; i++)
            {
                GameObject.Find("Environment").transform.FindChild("Item_class").GetComponent<Item_create>().ItemAloneCreate(transform.position, 20019 + i);
            }
        }
    }
    void LockMouseAndScreen()
    {
        Cursor.visible = Cursor.visible ? false : true;
        if (!Cursor.visible)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        IsHandleModemouse = Cursor.visible;
    }
}
