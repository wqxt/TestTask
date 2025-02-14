using UnityEngine;

public class GroundCheckSphere : MonoBehaviour
{
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private float _groundSphereRadius;
    [SerializeField] private Character _character;
    [SerializeField] private CharacterData _characterData;


    private void OnTriggerStay(Collider other)
    {
        if ((_groundLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            _characterData._isGrounded = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ((_groundLayerMask.value & (1 << other.transform.gameObject.layer)) > 0)
        {
            _characterData._isGrounded = false;

        }
    }
}
