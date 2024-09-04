using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectBuilder : MonoBehaviour
{
    [SerializeField] float _maxTargetRange;
    [SerializeField] float _buildHologramRange;
    [SerializeField] string _interactbleObjectTag;
    [SerializeField] LayerMask ignoreMask;
    [SerializeField] LayerMask objectMask;

    [SerializeField] Camera _cam;

    BuildMode _buildMode = BuildMode.off;

    BuildObject _buildObject;
    Vector3 _buildObjectRoot;
    RaycastHit _hit;
    public Vector3 GetBuildObjectRoot()
    {
        return _buildObjectRoot;
    }

    private void FixedUpdate()
    {

        if (_buildObject)
        {
            _buildMode = BuildMode.on;

            if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, _maxTargetRange, ignoreMask))
            {                
                _buildObjectRoot = hit.point + (hit.normal * (_buildObject.transform.localScale.y / 2f));
 
                _buildObject.Connected(hit.collider.gameObject);
            }
            else
            {
                _buildObjectRoot = _cam.transform.position + (transform.forward * _buildHologramRange);
                _buildObject.Connected(null);
            }
        }
        else
        {
            _buildMode = BuildMode.off;
        }

       
        
        
    }


    //По хорошему бы отделить инпут, на скорую руку, думаю, не критично :)
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_buildObject)
            {
                if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out _hit, _maxTargetRange, objectMask))
                {
                    BuildObject targetObject = _hit.collider.GetComponent<BuildObject>();
                    _buildObject = Instantiate(targetObject, _hit.point, _hit.transform.rotation);
                    _buildObject.Init(this);
                }
            }
            else
            {
                if (_buildObject.Create())
                {
                    _buildObject = null;
                }
            }
        }

        if (_buildObject)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                _buildObject.ChangeRotation(+45);
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                _buildObject.ChangeRotation(-45);
            }
        }
        
    }
}

public enum BuildMode
{
    off,
    on
}
