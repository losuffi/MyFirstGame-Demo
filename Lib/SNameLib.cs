using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
namespace GetSName
{
    abstract class Item
    {
        private static List<ItemTemp> It;
        public static string Name(int sid)
        {
            ItemTemp temp = It.Find(ar=> {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Name;
            return "Empty!";
        }
        public static string Type(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Type;
            return "Empty!";
        }
        public static string Content(int sid)
        {

            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Content;
            return "Empty!";

        }
        public static int SpellID(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.SpellID;
            return 0;
        }
        public static bool IsCanTwiceSpell(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.IsCanTwiceSpell;
            return false;
        }
        public static float Attack(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Attack;
            return 0;
        }
        public static float Defense(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Defense;
            return 0;
        }
        public static float Slife(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Slife;
            return 0;
        }
        public static float Speed(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.Speed;
            return 0;
        }
        public static float GatherValue(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.GatherValue;
            return 0;
        }
        public static Sprite ISprite(int sid)
        {
            return GameObject.Find("System/Sprite").GetComponent<ImageSprite>().GetSprite(sid);
        }
        public static Sprite Geizi
        {
            get
            {
                return GameObject.Find("System/Sprite").GetComponent<ImageSprite>().Geizi;
            }
        }
        public static int WeponModeIndex(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.WeaponModeIndex;
            return 0;
        }
        public static string EquiPos(int sid)
        {
            ItemTemp temp = It.Find(ar => {
                if (ar.Id == sid)
                    return true;
                return false;
            });
            if (temp != null)
                return temp.EquiPos;
            return "Empty!";
        }
        public static void StartItemData()
        {
            using (StreamReader s= File.OpenText(Application.dataPath + @"/StreamingAssets/Item.txt")){
                It = JsonMapper.ToObject<List<ItemTemp>>(s.ReadToEnd());
            }
            // It = JsonMapper.ToObject<List<ItemTemp>>( File.ReadAllText(Application.dataPath + @"/StreamingAssets/Item.txt"));
        }
    }
    public class ItemTemp
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public int SpellID { get; set; }
        public bool IsCanTwiceSpell { get; set; }
        public float Attack { get; set; }
        public float Defense { get; set; }
        public float Slife { get; set; }
        public float Speed { get; set; }
        public float GatherValue { get; set; }
        public string EquiPos { get; set; }
        public int WeaponModeIndex { get; set; }
    }
}