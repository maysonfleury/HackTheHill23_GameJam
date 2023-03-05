using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private Camera _camera = null;
    [SerializeField] private float _moveSpeed = 15f; //50f;
    [SerializeField] private float _moveSmooth = 5f;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _zoomSmooth = 5f;
    private Controls _input = null;
    private bool _moving = false;
    private bool _zooming = false;

    private Vector3 _center = Vector3.zero;
    private float _right = 10f;
    private float _left = 10f;
    private float _up = 10f;
    private float _down = 10f;
    private float _angle = 45f;
    private float _zoom = 5f;
    private float _zoomMax = 10f;
    private float _zoomMin = 1f;
    private Vector2 _zoomPositionOnScreen = Vector2.zero;
    private Vector3 _zoomPositionInWorld = Vector3.zero;
    private float _zoomBaseValue = 0f;
    private float _zoomBaseDistance = 0f;


    private Transform _root = null;
    private Transform _pivot = null;
    private Transform _target = null;

    private void Awake()
    {
        _input = new Controls();
        _root =  new GameObject("CameraHelper").transform;
        _pivot =  new GameObject("CameraPivot").transform;
        _target =  new GameObject("CameraTarget").transform;
        _camera.orthographic = true;
        _camera.nearClipPlane = 0;
    }

    private void Start()
    {
        Initialize(Vector3.zero, 20f, 20f, 15f, 15f, 45f, 5f, 2f, 8f);
    }

    private void Initialize(Vector3 center, float right, float left, float up, float down, float angle, float zoom, float zoomMin, float zoomMax)
    {
        _center = center;
        _right = right;
        _left = left;
        _up = up;
        _down = down;
        _angle = angle;
        _zoom = zoom;
        _zoomMin = zoomMin;
        _zoomMax = zoomMax;
        _camera.orthographicSize = _zoom;
        _moving = false;
        _zooming = false;
        _pivot.SetParent(_root);
        _target.SetParent(_pivot);
        _root.position = _center;
        _root.localEulerAngles = Vector3.zero;
        _pivot.localPosition = Vector3.zero;
        _pivot.localEulerAngles = new Vector3(_angle, 0f, 0f);
        _target.localPosition = new Vector3(0f, 0f, -100f);
        _target.localEulerAngles = Vector3.zero;
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Main.Move.started += _ => MoveStarted();
        _input.Main.Move.canceled += _ => MoveCanceled();
        _input.Main.TouchZoom.started += _ => ZoomStarted();
        _input.Main.TouchZoom.canceled += _ => ZoomCanceled();
        _input.Main.PointerClick.performed += _ => ScreenClicked();
    }

    private void OnDisable()
    {
        _input.Main.Move.started -= _ => MoveStarted();
        _input.Main.Move.canceled -= _ => MoveCanceled();
        _input.Main.TouchZoom.started -= _ => ZoomStarted();
        _input.Main.TouchZoom.canceled -= _ => ZoomCanceled();
        _input.Main.PointerClick.performed -= _ => ScreenClicked();
        _input.Disable();
    }

    private void MoveStarted()
    {
        _moving = true;
    }

    private void MoveCanceled()
    {
        _moving = false;
    }

    private void ZoomStarted()
    {
        Vector2 touch0 = _input.Main.TouchPosition0.ReadValue<Vector2>();
        Vector2 touch1 = _input.Main.TouchPosition1.ReadValue<Vector2>();
        _zoomPositionOnScreen = Vector2.Lerp(touch0, touch1, 0.5f);
        _zoomPositionInWorld = CameraScreenPositionToPlanePosition(_zoomPositionOnScreen);
        _zoomBaseValue = _zoom;
        touch0.x /= Screen.width;
        touch1.x /= Screen.width;
        touch0.y /= Screen.height;
        touch1.y /= Screen.height;
        _zoomBaseDistance = Vector2.Distance(touch0, touch1);
        _zooming = true;
    }

    private void ZoomCanceled()
    {
        _zooming = false;
    }

    private void ScreenClicked()
    {
        Vector2 position = _input.Main.PointerPosition.ReadValue<Vector2>();
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject.GetComponent<Building>() != null)
            {
                Debug.Log(hit.transform.name + " was clicked on.");
                hit.transform.gameObject.GetComponent<Building>().ShowUI();
            }
        }
    }

    private void Update()
    {
        if (Input.touchSupported == false)
        {
            float mouseScroll = _input.Main.MouseScroll.ReadValue<float>();
            if (mouseScroll > 0)
            {
                _zoom -= 3f * Time.deltaTime;
            }
            else if (mouseScroll < 0)
            {
                _zoom += 3f * Time.deltaTime;
            }
        }

        if (_zooming)
        {
            Vector2 touch0 = _input.Main.TouchPosition0.ReadValue<Vector2>();
            Vector2 touch1 = _input.Main.TouchPosition1.ReadValue<Vector2>();

            touch0.x /= Screen.width;
            touch1.x /= Screen.width;
            touch0.y /= Screen.height;
            touch1.y /= Screen.height;

            float currentDistance = Vector2.Distance(touch0, touch1);
            float deltaDistance = currentDistance - _zoomBaseDistance;
            _zoom = _zoomBaseDistance - (deltaDistance * _zoomSpeed);

            Vector3 zoomCenter = CameraScreenPositionToPlanePosition(_zoomPositionOnScreen);
            _root.position += (_zoomPositionInWorld - zoomCenter);
        }
        else if (_moving)
        {
            Vector2 move = _input.Main.MoveDelta.ReadValue<Vector2>();
            if(move != Vector2.zero)
            {
                move.x /= Screen.width;
                move.y /= Screen.height;
                _root.position -= _root.right.normalized * move.x * _moveSpeed;
                _root.position -= _root.forward.normalized * move.y * _moveSpeed;
            }
        }

        AdjustBounds();

        if (_camera.orthographicSize != _zoom)
        {
            _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, _zoom, _zoomSmooth * Time.deltaTime);
        }
        if (_camera.transform.position != _target.position)
        {
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, _target.position, _moveSmooth * Time.deltaTime);
        }
        if (_camera.transform.rotation != _target.rotation)
        {
            _camera.transform.rotation = _target.rotation;
        }
    }

    private void AdjustBounds()
    {
        if (_zoom < _zoomMin)
        {
            _zoom = _zoomMin;
        }
        if (_zoom > _zoomMax)
        {
            _zoom = _zoomMax;
        }

        float h = PlaneOrthographicSize();
        float w = h * _camera.aspect;

        if (h > (_up + _down) / 2f)
        {
            float n = (_up + _down) / 2f;
            _zoom = n * Mathf.Sin(_angle * Mathf.Deg2Rad);
        }
        if (w > (_right + _left) / 2f)
        {
            float n = (_right + _left) / 2f;
            _zoom = n * Mathf.Sin(_angle * Mathf.Deg2Rad) / _camera.aspect;
        }

        h = PlaneOrthographicSize();
        w = h * _camera.aspect;

        Vector3 tr = _root.position + _root.right.normalized * w + _root.forward.normalized * h;
        Vector3 tl = _root.position - _root.right.normalized * w + _root.forward.normalized * h;
        Vector3 dr = _root.position + _root.right.normalized * w - _root.forward.normalized * h;
        Vector3 dl = _root.position - _root.right.normalized * w - _root.forward.normalized * h;

        if (tr.x > _center.x + _right)
        {
            _root.position += Vector3.left * Mathf.Abs(tr.x - (_center.x + _right));
        }
        if (tl.x < _center.x - _left)
        {
            _root.position += Vector3.right * Mathf.Abs((_center.x - _left) - tl.x);
        }

        if (tr.z > _center.z + _up)
        {
            _root.position += Vector3.back * Mathf.Abs(tr.z - (_center.z + _up));
        }
        if (dl.z < _center.z - _down)
        {
            _root.position += Vector3.forward * Mathf.Abs((_center.z - _down) - dl.z);
        }
    }

    private float PlaneOrthographicSize()
    {
        float h = _zoom * 2f;
        return h / Mathf.Sin(_angle * Mathf.Deg2Rad) / 2f;
    }

    private Vector3 CameraScreenPositionToWorldPosition(Vector2 position)
    {
        float h = _camera.orthographicSize * 2f;
        float w = _camera.aspect * h;
        Vector3 anchor = _camera.transform.position - (_camera.transform.right.normalized * w / 2f) - (_camera.transform.up.normalized * h / 2f);
        return anchor + (_camera.transform.right.normalized * position.x / Screen.width * w) + (_camera.transform.up.normalized * position.y / Screen.height * h);
    }

    private Vector3 CameraScreenPositionToPlanePosition(Vector2 position)
    {
        Vector3 point = CameraScreenPositionToWorldPosition(position);
        float h = point.y - _root.position.y;
        float x = h / Mathf.Sin(_angle * Mathf.Deg2Rad);
        return point + _camera.transform.forward.normalized * x;
    }
}
