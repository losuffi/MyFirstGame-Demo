using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//public class PlayDirectCurrent : MonoBehaviour,IDragHandler,IPointerUpHandler
//{
//    private float u_x,u_y,angle,shift;
//    private Vector3 u_pos;
//    private Transform Center;
//    private enum result
//    {
//        go,
//        back,
//        turnR,
//        turnL,
//        goTr,
//        goTl,
//        backTr,
//        backTl,
//        none,
//    }
//    private  result act;
//    void Update()
//    {
//        Center.GetComponent<PlayDirect>().Work((byte)act);
//    }
//    void Awake()
//    {
//        Center = this.transform.parent;
//    }
//    void Start()
//    {
//        u_pos = this.transform.position;
//        act = result.none;
//    }
//    public void OnDrag(PointerEventData eventdata)
//    {
//        u_x = eventdata.position.x - u_pos.x;
//        u_y = eventdata.position.y - u_pos.y;
//        angle = Mathf.Atan2(u_y, u_x);
//        shift = Mathf.Sqrt(u_x * u_x + u_y * u_y);
//        shift = (shift > 100 ? 100 : shift);
//        this.transform.position = new Vector2(u_pos.x + shift * Mathf.Cos(angle), u_pos.y + shift * Mathf.Sin(angle));
//        if (shift > 70)
//        {
//            if (angle > Mathf.PI *5/ 12 && angle <= Mathf.PI * 7 / 12)
//            {
//                act = result.go;
//            }
//            else if(angle < Mathf.PI * -5 / 12 && angle >= Mathf.PI * -7 / 12)
//            {
//                act = result.back;
//            }
//            else if(angle > Mathf.PI * 1 / 12 && angle <= Mathf.PI * 5 / 12)
//            {
//                act = result.goTr;
//            }
//            else if (angle > Mathf.PI * 7 / 12 && angle <= Mathf.PI * 11 / 12)
//            {
//                act = result.goTl;
//            }
//            else if (angle < Mathf.PI * -1 / 12 && angle >= Mathf.PI * -5 / 12)
//            {
//                act = result.backTr;
//            }
//            else if (angle < Mathf.PI * -7 / 12 && angle >= Mathf.PI * -11 / 12)
//            {
//                act = result.backTl;
//            }
//            else if(angle > Mathf.PI * -1 / 12 && angle <= Mathf.PI * 1 / 12)
//            {
//                act = result.turnR;
//            }
//            else
//            {
//                act = result.turnL;
//            }
//        }
//        else
//        {
//            act = result.none;
//        }
//    }
//    public void OnPointerUp(PointerEventData eventdata)
//    {
//        this.transform.GetComponent<RectTransform>().position = u_pos;
//        act = result.none;
//    }
//}
