using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class UnitUnetTransform : NetworkBehaviour {
    public float LerpSpeed;
    [SyncVar]Vector3 SeverPos = Vector3.zero;
    [SyncVar]Quaternion SeverRotate = Quaternion.identity;
    void FixedUpdate()
    {
        if (isLocalPlayer)
        {
            CmdSend2Serve(this.transform.position,this.transform.rotation);
        }
        else
        {
            LerpPos();
        }
    }
    [Command]void CmdSend2Serve(Vector3 pos,Quaternion rotate)
    {
        SeverPos = pos;
        SeverRotate = rotate;
    }
    void LerpPos()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, SeverPos, LerpSpeed * Time.fixedDeltaTime);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, SeverRotate, LerpSpeed * Time.fixedDeltaTime);
    }
}
