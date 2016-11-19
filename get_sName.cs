using UnityEngine;
using System.Collections;

public class get_sName : MonoBehaviour {
    public string Item_name(int sid)
    {
        string sname;
        switch (sid)
        {
            case 20001:
                sname = "木材";
                break;
            case 20002:
                sname = "石材";
                break;
            case 20003:
                sname = "草藤";
                break;
            case 20004:
                sname = "动物皮毛";
                break;
            case 20005:
                sname = "治疗药剂";
                break;
            case 20006:
                sname = "弓箭";
                break;
            case 20007:
                sname = "火弩";
                break;
            case 20008:
                sname = "火箭喷射机";
                break;
            case 20009:
                sname = "火焰喷雾";
                break;
            case 20010:
                sname = "地雷";
                break;
            case 20011:
                sname = "炸弹";
                break;
            case 20012:
                sname = "小型肉";
                break;
            case 20013:
                sname = "油脂";
                break;
            case 20014:
                sname = "原木精华";
                break;
            case 20015:
                sname = "硝石";
                break;
            case 20016:
                sname = "轻皮甲";
                break;
            case 20017:
                sname = "石矛";
                break;
            default:
                sname = "无定义";
                break;
        }
        return sname;
    }
    public string Item_type(int sid)
    {
        switch (sid)
        {
            case 20001:
                return "Material";
            case 20002:
                return "Material";
            case 20003:
                return "Material";
            case 20004:
                return "Material";
            case 20005:
                return "prop";
            case 20006:
                return "equi";
            case 20007:
                return "equi";
            case 20008:
                return "equi";
            case 20009:
                return "equi";
            case 20010:
                return "prop";
            case 20011:
                return "prop";
            case 20012:
                return "prop";
            case 20013:
                return "Material";
            case 20014:
                return "Material";
            case 20015:
                return "Material";
            case 20016:
                return "staticequi";
            case 20017:
                return "staticequi";
            default:
                return "null";
        }
    }
    public string Item_content(int sid)
    {
        string scontent;
        switch (sid)
        {
            case 20001:
                scontent = "  基本制作材料，用于合成，无法直接使用";
                break;
            case 20002:
                scontent = "  基本制作材料，用于合成，无法直接使用";
                break;
            case 20003:
                scontent = "  基本制作材料，用于合成，无法直接使用";
                break;
            case 20004:
                scontent = "  基本制作材料，用于合成，无法直接使用";
                break;
            case 20005:
                scontent = "  基本战斗道具，使用后可直接恢复100点生命";
                break;
            case 20006:
                scontent = "  初级战斗器械，冷却时间：9，射速：中，伤害：150";
                break;
            case 20007:
                scontent = "  中级战斗器械，冷却时间：3，射速：快，伤害：180";
                break;
            case 20008:
                scontent = "  中级战斗器械，冷却时间：8，提供跳跃初始速度：90";
                break;
            case 20009:
                scontent = "  中级战斗器械，冷却时间：2，每秒伤害：80，移动速度降低：4,伤害范围：4";
                break;
            case 20010:
                scontent = "  中级战斗道具，冷却时间：1，爆炸伤害：300，生存周期：5min，爆炸半径：5";
                break;
            case 20011:
                scontent = "  中级战斗道具，冷却时间：1，爆炸伤害：200，生存周期：20S，最大水平投掷距离：150，爆炸半径：20";
                break;
            case 20012:
                scontent = " 初级食物，恢复15点饱食度";
                break;
            case 20013:
                scontent = " 中级制作材料，用于合成，无法直接使用";
                break;
            case 20014:
                scontent = " 中级制作材料，用于合成，无法直接使用";
                break;
            case 20015:
                scontent = " 中级制作材料，用于合成，无法直接使用";
                break;
            case 20016:
                scontent = " 初级战斗装备，提升20点护甲";
                break;
            case 20017:
                scontent = " 初级战斗装备，提升10点攻击";
                break;
            default:
                scontent = "Null";
                break;
        }
        return scontent;
    }
    public int Item_Spell(int sid)
    {
        int sp_id;
        switch (sid)
        {
            case 20005:
                sp_id = 102;
                break;
            case 20006:
                sp_id = 103;
                break;
            case 20007:
                sp_id = 104;
                break;
            case 20008:
                sp_id = 105;
                break;
            case 20009:
                sp_id = 106;
                break;
            case 20010:
                sp_id = 107;
                break;
            case 20011:
                sp_id = 108;
                break;
            case 20012:
                sp_id = 301;
                break;
            default:
                sp_id = 0;
                break;
        }
        return sp_id;
    }
    public bool Item_IsTwiceCast(int sid)
    {
        if (sid == 20011)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public string Talent_name(int tid)
    {
        string sname;
        switch (tid)
        {
            case 50001:
                sname = "力量强化";
                break;
            case 50002:
                sname = "敏捷强化";
                break;
            case 50003:
                sname = "防御熟练";
                break;
            case 50004:
                sname = "快速奔跑";
                break;
            default:
                sname = "无定义";
                break;
        }
        return sname;
    }
    public string Talent_Type(int tid)
    {
        string stype;
        switch (tid)
        {
            case 50001:
                stype = "passive";
                break;
            case 50002:
                stype = "passive";
                break;
            case 50003:
                stype = "passive";
                break;
            case 50004:
                stype = "active";
                break;
            default:
                stype = "无定义";
                break;
        }
        return stype;
    }
    public string Talent_Content(int tid)
    {
        string scontent;
        switch (tid)
        {
            case 50001:
                scontent = "基础天赋，增强自身力量，解锁力量系天赋，被动增加30点攻击力";
                break;
            case 50002:
                scontent = "基础天赋，增强自身敏捷，解锁敏捷系天赋，被动增加1.5点移动速度";
                break;
            case 50003:
                scontent = "初级天赋，父级天赋为力量强化，被动添加20点伤害抗性";
                break;
            case 50004:
                scontent = "初级天赋，父级天赋为敏捷强化，主动施放：快速奔跑，5秒内移动速度提高40%，冷却时间：20";
                break;
            default:
                scontent = "无定义";
                break;
        }
        return scontent;
    }
    public int Talent_parent(int tid)
    {
        int sparent;
        switch (tid)
        {
            case 50001:
                sparent = 50000;
                break;
            case 50002:
                sparent = 50000;
                break;
            case 50003:
                sparent = 50001;
                break;
            case 50004:
                sparent = 50002;
                break;
            default:
                sparent = 50000;
                break;
        }
        return sparent;
    }
    public int Talent_SpellId(int tid)
    {
        int sid;
        switch (tid)
        {
            case 50004:
                sid = 201;
                break;
            default:
                sid = 200;
                break;
        }
        return sid;
    }
    public int Talent_Count()
    {
        return 4;
    }
}
/*s_class:
 *        Unit:单位类
 *        Item:物品类
 *        Hero:英雄类
 *        Talent:天赋类
 * 
 * s_id:
 *         10->:怪物类
 *         20->:物品类
 *         30->:英雄类
 *         40->:环境类
 *         50->:天赋类
 *       10:
 *          10001:巨型松鼠
 *       20：
 *          20001：木材
 *          20002：石材
 *          20003：动物皮毛
 *          20004：草藤
 *          20005:治疗药剂
 *          20006：弓箭
 *          20007:火弩
 *          20008：火箭喷射机
 *          20009:火焰喷雾
 *          20010：地雷
 *          20011:炸弹
 *          20012:小型肉
 *          20013:油脂
 *       30：
 *          30001：达尔文
 *          30002：铁犬
 *       40：
 *          40001：树
 *          40002：兰草
 *          40003：岩石
 *          40004：动物皮毛
 *       50:
 *          50001:力量强化
 *          50002:敏捷强化
 * s_iType:
 *         equi:装备类 使用无消耗
 *         prop:道具类 使用有消耗
 *         staticequi:靜態數據提升裝備
 *         passive:被动类   
 *         active:主动类
 * */

