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
                sname = "燧石";
                break;
            case 20016:
                sname = "草衣";
                break;
            case 20017:
                sname = "石矛";
                break;
            case 20018:
                sname = "石制斧头";
                break;
            case 20019:
                sname = "夏木叶簇";
                break;
            case 20020:
                sname = "樟木叶簇";
                break;
            case 20021:
                sname = "粗树枝";
                break;
            case 20022:
                sname = "草绳";
                break;
            case 20023:
                sname = "石制锄头";
                break;
            case 20024:
                sname = "工作台";
                break;
            case 20025:
                sname = "篝火";
                break;
            case 20026:
                sname = "燃料";
                break;
            default:
                sname = "无";
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
            case 20018:
                return "staticequi";
            case 20019:
                return "Material";
            case 20020:
                return "Material";
            case 20021:
                return "Material";
            case 20022:
                return "Material";
            case 20023:
                return "staticequi";
            case 20024:
                return "Building";
            case 20025:
                return "Building";
            case 20026:
                return "prop";
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
                scontent = "  最基本的制作材料，也可直接生成较多的燃料，可以加工成木板、木棍等中级材料";
                break;
            case 20002:
                scontent = "  最基本的制作材料，主要用于建造";
                break;
            case 20003:
                scontent = "  初级制作材料，产生自动物尸体，能提取得到油脂，可用于制作皮制物、和绳子等物品";
                break;
            case 20004:
                scontent = "  初级制作材料，主要用于制作简单的绳子、部分药物和作为燃料和引燃物使用，能得到较少的燃料";
                break;
            case 20005:
                scontent = "  初级药品，快速止血，能直接恢复少量生命值--50";
                break;
            case 20006:
                scontent = "  中级战斗器械，发射系数：20，伤害因子：6，冷却时间：10 ,制作需要：工作台";
                break;
            case 20007:
                scontent = "  高级战斗器械，发射系数：40，伤害因子：9，冷却时间：3 ,制作需要：工作台";
                break;
            case 20008:
                scontent = "  特级战斗器械，提供一个向上的初速度，大小为90，以及20的移动速度加成，消耗：燃油 X1 ，冷却：2 ,制作需要：工作台";
                break;
            case 20009:
                scontent = "  特级战斗器械，向外喷射火焰，半径为：4，喷射后移动速度-3，每秒伤害：100，每3秒消耗：燃油 X1 ,制作需要：工作台 ";
                break;
            case 20010:
                scontent = "  高级陷阱，触发范围：3，触发后爆炸，伤害：300，陷阱生存周期：5Min ,制作需要：工作台";
                break;
            case 20011:
                scontent = "  高级战斗消耗品，主动投掷物品，投掷距离：5-150，产生爆炸，爆炸半径：20，伤害200，生存周期：20s，冷却时间：1 ,制作需要：工作台 ";
                break;
            case 20012:
                scontent = " 初级食物，直接恢复15点饱食度";
                break;
            case 20013:
                scontent = " 中级制作材料，燃烧类道具的基本原材料，常用于火药、燃油、不稳定化合物等物品的制作";
                break;
            case 20014:
                scontent = " 高级制作材料，通玄类道具的基本原材料，常用于符文笔、符墨、各类护身符等物品的制作";
                break;
            case 20015:
                scontent = " 初级制作材料，可经过简单的加工，成为一些简单道具的制作材料，可直接用于：石制斧子、打火石、石锥等基本道具的制作";
                break;
            case 20016:
                scontent = " 初级装备，提升5点护甲";
                break;
            case 20017:
                scontent = " 初级装备，提升15点攻击";
                break;
            case 20018:
                scontent = " 初级装备，提升5点攻击，5点收集能力，特性：树木收集精通";
                break;
            case 20019:
                scontent = " 初级制作材料，可制作叶布胚、引燃物、生成较少燃料、某些道具制作需求";
                break;
            case 20020:
                scontent = " 初级制作材料，可制作叶布胚、引燃物、生成较少燃料、某些道具制作需求";
                break;
            case 20021:
                scontent = " 初级制作材料，可经过简单的加工，成为一些简单道具的制作材料，可直接用于：石制斧子、打火石、石锥等基本道具的制作，生成较少燃料";
                break;
            case 20022:
                scontent = " 中级制作材料，用于制作：石制斧子、打火石、石锥等基本道具，生成较少燃料";
                break;
            case 20023:
                scontent = " 初级装备，提升5点攻击，5点收集能力，特性：矿物收集精通";
                break;
            case 20024:
                scontent = " 初级建筑，用于放置各类加工器械，制作高阶道具的必要建筑";
                break;
            case 20025:
                scontent = " 初级建筑，燃烧场所，主要用于加工食物或者作为光源、取暖";
                break;
            case 20026:
                scontent = " 燃烧消耗品，用于燃烧照明，使用方法为：靠近火源，并面向火源，并点击使用，或按下快捷键，进行投放燃料操作";
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
            case 20026:
                sp_id = 302;
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
    public int WeaponIndex(int sid)
    {
        int Index;
        switch (sid)
        {
            case 20017:
                Index = 1;
                break;
            case 20018:
                Index = 2;
                break;
            default:
                Index = 0;
                break;
        }
        return Index;
    }
    public string EquiPos(int sid)
    {
        switch (sid)
        {
            default:
                return "z";
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
    public int BuildId(int sid)
    {
        switch (sid)
        {
            case 20024:
                return 60001;
            case 20025:
                return 60002;
            default:
                return 0;
        }
    }
}
/*s_class:
 *        Unit:单位类
 *        Item:物品类
 *        Hero:英雄类
 *        Talent:天赋类
 *        Building:建筑类
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
 *          20003：草藤
 *          20004：动物皮毛
 *          20005:治疗药剂
 *          20006：初级弓
 *          20007:火弩
 *          20008：火箭喷射机
 *          20009:火焰喷雾
 *          20010：地雷
 *          20011:炸弹
 *          20012:小型肉
 *          20013:油脂
 *          20014：原木精华 
 *          20015：燧石
 *          20016：轻皮甲
 *          20017：石矛
 *          20018:石制斧头
 *          20019:夏树叶簇
 *          20020：红樟叶簇
 *          20021：粗树枝
 *          20022:草绳
 *          20023:石制锄头
 *          20024:工作台
 *          20025：篝火
 *          20026:燃料
 *       30：
 *          30001：达尔文
 *          30002：铁犬
 *       40：
 *          40001：树
 *          40002：兰草
 *          40003：岩石
 *          40004：动物皮毛
 *          40005: 红樟树
 *          40006: 碎石
 *          40007：黄泥木
 *       50:
 *          50001:力量强化
 *          50002:敏捷强化
 *       60:
 *          60001:工作台
 *          60002:篝火
 * s_iType:
 *         equi:装备类 使用无消耗
 *         prop:道具类 使用有消耗
 *         staticequi:靜態數據提升裝備
 *         passive:被动类   
 *         active:主动类
 * */

