using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildBox : BuildObject
{
    private void FixedUpdate()
    {
        if(_buildObjectState == BuildObjectState.hologram)
        {
            bool colliderFlag = true;

            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 1.9f, transform.rotation);

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
                        if (collider.tag != "Floor" & collider.tag != "Cube")
                        {
                            colliderFlag = false;

                        }

                        if (collider.tag == "Cube" && !CheckCubeDown(collider.gameObject))
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

    bool CheckCubeDown(GameObject checkTarget)
    {
        Collider[] checkCubeCollider = Physics.OverlapBox(transform.position + Vector3.down, transform.localScale / 2.2f, transform.rotation);

        foreach(Collider collider in checkCubeCollider)
        {
            if(collider.gameObject == checkTarget)
            {
                return true;
            }
        }

        return false;
    }
}
