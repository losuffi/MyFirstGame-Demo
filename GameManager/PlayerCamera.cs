using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {
    void Awake()
    {
        transform.Rotate(transform.right * 12);
        transform.position = new Vector3(50, 15, 50);
    }
    public void BindPlayer(Transform Player)
    {
        this.transform.parent = Player;
        this.transform.position = Player.position - 5 * Player.forward + 3 * Player.up;
        this.transform.Rotate(7 * this.transform.right);
        Player.GetComponent<player>().playcamera = this.gameObject;
    }
}
