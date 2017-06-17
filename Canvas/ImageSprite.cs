using UnityEngine;
using System.Collections;

public class ImageSprite : MonoBehaviour {
    public Sprite[] Item_image;
    public Sprite Geizi;
    public Sprite GetSprite(int Item_id)
    {
        return Item_image[Item_id - 20001];
    }
    public Sprite GetTalentSprite(int Tid)
    {
        return Item_image[Tid - 50001];
    }
}
