using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class PlayerGrabAndThrow : MonoBehaviour
{
    private PlayerInputHandler _playerInputHandler;

    [Tooltip("Объект, относительно которого хватается предмет")]
    [SerializeField] private GameObject _pointOfGripObject;

    [Header("Настройки хвата предметов")]
    [SerializeField] private float _handLenght;
    [SerializeField] private float _holdThingDistance;
    [SerializeField] private float _dropStrength;

    [Tooltip("Ссылка на схваченный предмет")]
    private GameObject _takedObject;
    private bool _isDragObject = false;

    private Vector2 _direction;
    private Vector2 _rotate;
    private Vector3 _rotation;


    private void Start()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();

        if (_pointOfGripObject == null)
        {
            GameObject findedPointOfGrip;
            _pointOfGripObject = (findedPointOfGrip = this.GetComponentInChildren<TagLookRoot>().gameObject) != null ? findedPointOfGrip : null;
        }

        //_playerInputHandler.PlayerInput.Player.Use.started += context => Debug.Log(context);
        //_playerInputHandler.PlayerInput.Player.Use.performed += context => Debug.Log(context);
        //_playerInputHandler.PlayerInput.Player.Use.canceled += context => Debug.Log(context);


        _playerInputHandler.PlayerInput.Player.Use.performed += context =>
        {
            if (!_isDragObject)
                TryGrab(_pointOfGripObject, ref _takedObject, ref _isDragObject, _handLenght, _holdThingDistance);
            else
            { 
                if (context.interaction is HoldInteraction)
                    Throw(ref _takedObject, ref _isDragObject);
                else
                    Drop(ref _takedObject, ref _isDragObject);
            }
        };

    }

    private void TryGrab(GameObject pointOfGripObject, ref GameObject takedObject, ref bool isDragObject, float handLenght, float holdThingDistance)
    {
        if (Physics.Raycast(pointOfGripObject.transform.position, pointOfGripObject.transform.forward, out var hitinfo, handLenght) )
        {
            if (!hitinfo.collider.gameObject.isStatic && hitinfo.collider.gameObject.TryGetComponent<Rigidbody>(out var rigidbody))
            {
                takedObject = hitinfo.collider.gameObject;
                takedObject.transform.position = default;
                takedObject.transform.SetParent(pointOfGripObject.transform, worldPositionStays: true);

                takedObject.transform.localPosition = new Vector3(0, 0, holdThingDistance);

                rigidbody.isKinematic = true;

                isDragObject = true;
            }
        }
    }


    private void Drop(ref GameObject takedObject, ref bool isDragObject)
    {
        takedObject.transform.parent = null;
        takedObject.GetComponent<Rigidbody>().isKinematic=false;
        isDragObject = false;
    }

    private void Throw(ref GameObject takedObject, ref bool isDragObject)
    {
        takedObject.transform.parent = null;
        var rigidbody = takedObject.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(_pointOfGripObject.transform.forward * _dropStrength, ForceMode.Impulse);
        isDragObject = false;
    }

}
