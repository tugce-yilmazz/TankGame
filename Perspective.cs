using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : Sense
{
    public Transform player;
    public float fieldOfView = 60;

    public override void Initialize()
    {
        
    }

    public override void UpdateSense()
    {
        Vector3 dir = player.position - transform.position;
        float cosAngle = Vector3.Dot(transform.forward, dir) / (transform.forward.magnitude * dir.magnitude);
        float angle = Mathf.Acos(cosAngle) * Mathf.Rad2Deg;

        if (angle < fieldOfView / 2)
        {
            if(Physics.Raycast(transform.position,dir,out RaycastHit info))
            {
                aspect = info.transform.GetComponent<Aspect>();
                if (aspect)
                {
                    Debug.Log(aspect.aspectType + "has been spotted");
                }
            }
        }
    }

   
}
