using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class AIWorkFigure : NetworkBehaviour {
    private float VertSpeed = 0;
    void Start()
    {
        if (isServer)
            this.gameObject.GetComponent<SqAI>().ConstructFsm();
    }
    void Update()
    {
        if (!this.gameObject.GetComponent<CharacterController>().isGrounded)
        {
            VertSpeed -= 90 * Time.deltaTime;
            this.gameObject.GetComponent<CharacterController>().Move(transform.up * VertSpeed * Time.deltaTime);
        }
        else
        {
            VertSpeed = 0;
        }
    }
}
