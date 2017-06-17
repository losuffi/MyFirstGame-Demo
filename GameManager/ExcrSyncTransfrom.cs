using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
public class ExcrSyncTransfrom : NetworkBehaviour {
    [SyncVar]
    Vector3 pos;
    [SyncVar]
    Quaternion rota;
    public float LerpSpeed;
    void FixedUpdate()
    {
        if (isServer)
        {
            pos = transform.position;
            rota = transform.rotation;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, pos, LerpSpeed * Time.fixedDeltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, rota, LerpSpeed * Time.fixedDeltaTime);
        }
    }
}
