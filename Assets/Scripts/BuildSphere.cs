using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSphere : BuildObject
{
    private void FixedUpdate()
    {
        bool colliderFlag = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, transform.localScale.x / 1.9f);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                if (collider.gameObject != _connectObject)
                {
                    colliderFlag = false;
                }
                else
                {
                    if (collider.tag != "Wall")
                    {
                        colliderFlag = false;
                    }
                }
            }

        }
        _personalObjectRequirement = colliderFlag;
        base.FixedUpdate();

    }
}
