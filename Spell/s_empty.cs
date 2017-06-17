using UnityEngine;
using System.Collections;

public class s_empty : MonoBehaviour {
    public int spell_id;
	void FixedUpdate() {
        if (spell_id == 106)
        {
            this.transform.rotation = this.transform.parent.rotation;
            Vector3 Trig_point = this.transform.position-this.transform.forward;
            RaycastHit hit;
            bool isCover = false;
            Transform[] Target = new Transform[10];
            int m = 0;
            for (int j = 0; j < 3; j++)
            {
                for (int k = 0; k < 3; k++)
                {
                    Trig_point = this.transform.position + k * this.transform.right + j * this.transform.up - this.transform.right - 0.2f * this.transform.up;
                    if (Physics.Linecast(Trig_point, Trig_point + this.transform.forward * 5, out hit))
                    {
                        isCover = false;
                        if (hit.collider.transform == transform || hit.collider.transform == transform.parent || hit.collider.GetComponent <Self_class>()==null|| !hit.collider.GetComponent<Self_class>().isLife||hit.collider.transform.tag!="Living")
                        {
                            isCover = true;
                        }
                        else
                        {
                            foreach (Transform ta in Target)
                            {
                                if (ta == hit.collider.transform)
                                {
                                    isCover = true;
                                    break;
                                }
                            }
                        }
                        if (!isCover)
                        {
                            Target[m] = hit.collider.transform;
                            m++;
                        }

                    }
                }
            }
            for (int n = 0; n < m; n++)
            {
                Target[n].GetComponent<Self_class>().injured(8 / 5, this.transform.parent);
            }
        }
	}
}
