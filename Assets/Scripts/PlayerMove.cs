using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMove : MonoBehaviour
{
    #region Field Declarations

    public float speed;
    PlayerInput _playerInput;
    Quaternion _currentRotation; //оставлять поворот при остановке движения

    #endregion
    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _currentRotation = transform.rotation;
    }
    void FixedUpdate()
    {
        Move();
    }

    #region Movement
    void Move()
    {
        if (Mathf.Abs(_playerInput.Horizontal) > Mathf.Epsilon)
        {
            if (_playerInput.Horizontal > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, -1 * 90f - 90f);
                _currentRotation = transform.rotation;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 1 * 90f - 90f);
                _currentRotation = transform.rotation;
            }
            transform.position = new Vector2(transform.position.x + speed * Time.fixedDeltaTime * _playerInput.Horizontal, transform.position.y);
        }
        else if (Mathf.Abs(_playerInput.Vertical) > Mathf.Epsilon)
        {
            if (_playerInput.Vertical > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 1 * -90f);
                _currentRotation = transform.rotation;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 1 * 90f);
                _currentRotation = transform.rotation;
            }
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.fixedDeltaTime * _playerInput.Vertical);
        }
        else
        {
            transform.rotation = _currentRotation;
        }
    }

    #endregion
}
