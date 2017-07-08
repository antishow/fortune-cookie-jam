using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerGrill : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Meat"))
        {
            Ingredient i = collision.collider.GetComponentInParent<Ingredient>();

            if(!i.data.isTenderized)
            {
                return;
            }

            Renderer[] rend = collision.collider.transform.parent.GetComponentsInChildren<Renderer>();

            int count = 0;

            foreach(Renderer r in rend)
            {
                if(r.gameObject == collision.collider.gameObject)
                {
                    r.material.color = Color.gray;

                    count++;
                }
                else
                {
                    if(r.material.color == Color.gray)
                    {
                        count++;
                    }
                }
            }

            if(count == 0)
            {
                i.data.isCooked = true;
            }
        }
    }
}