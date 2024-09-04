using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildObject : MonoBehaviour
{
    [SerializeField] protected Collider _collider;
    [SerializeField] protected MeshRenderer _mesh;
    [SerializeField] protected Material _acceptHologramMaterial;
    [SerializeField] protected Material _cancelHologramMaterial;
    protected Material _defaultMaterial;

    protected ObjectBuilder _objectBuilder;
    protected BuildObjectState _buildObjectState = BuildObjectState.created;

    protected GameObject _connectObject;
    protected bool _personalObjectRequirement;
    int layerBuff;
    public void Connected(GameObject connectObject)
    {
        _connectObject = connectObject;
    }

    public void Init(ObjectBuilder objectBuilder)
    {
        _defaultMaterial = _mesh.material;
        _objectBuilder = objectBuilder;
        _buildObjectState = BuildObjectState.hologram;
        _mesh.material = _cancelHologramMaterial;
        layerBuff = gameObject.layer;
        gameObject.layer = 6;
        _collider.isTrigger = true;
    }

    public bool Create()
    {
        if(_connectObject & _personalObjectRequirement)
        {
            _mesh.material = _defaultMaterial;
            gameObject.layer = layerBuff;
            _collider.isTrigger = false;
            _buildObjectState = BuildObjectState.created;
            return true;
        }
        else
        {
            return false;
        }
        

    }

    public void ChangeRotation(float yRotation)
    {
        Vector3 newVectorRotate = transform.rotation.eulerAngles;
        newVectorRotate.y += yRotation;
        transform.rotation = Quaternion.Euler(newVectorRotate);
    }

    protected void FixedUpdate()
    {
        if(_buildObjectState == BuildObjectState.hologram)
        {
            transform.position = _objectBuilder.GetBuildObjectRoot();

            if (_connectObject)
            {
                if (_personalObjectRequirement)
                {
                    if (_mesh.material != _acceptHologramMaterial)
                    {
                        _mesh.material = _acceptHologramMaterial;
                    }
                }
                else
                {
                    if (_mesh.material != _cancelHologramMaterial)
                    {
                        _mesh.material = _cancelHologramMaterial;
                    }
                }

            }

            if (!_connectObject)
            {
                if (_mesh.material != _cancelHologramMaterial)
                {
                    _mesh.material = _cancelHologramMaterial;
                }

            }
        }
       
    }

}

public enum BuildObjectState
{
    hologram,
    created,
}
