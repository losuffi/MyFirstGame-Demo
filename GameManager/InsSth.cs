using UnityEngine;
using System.Collections;
public class InsSth : InsBass {
    public string ParentPath;
    public string Pname;
    private bool IsSet = false;
    void Update()
    {
        if (isServer)
        {
            if (!IsSet && isInit && obj != null) 
            {
                GameObject Parent = obj.transform.Find(ParentPath).gameObject;
                GameObject GInit = Parent.transform.Find("GInit").gameObject;
                pos = GInit.transform.position;
                qua = GInit.transform.rotation;
                transform.position = pos;
                transform.rotation = qua;
                transform.parent = Parent.transform;
                transform.name = Pname;
                IsSet = true;
            }
        }
        else
        {
            if (!IsSet && isInit && obj != null)
            {
                GameObject Parent = obj.transform.Find(ParentPath).gameObject;
                transform.position = pos;
                transform.rotation = qua;
                transform.parent = Parent.transform;
                transform.name = Pname;
                IsSet = true;
            }
        }
    }
}
