using UnityEngine;

public class ScreenBounds : MonoBehaviour //Границы экрана 
{
    #region Field Declarations

    private static Vector3 _bounds;
    private static float _spriteBorder = .5f;
    public static float left { get { return -_bounds.x + _spriteBorder; } }
    public static float right { get { return _bounds.x - _spriteBorder; } }
    public static float top { get { return _bounds.y - _spriteBorder; } }
    public static float bottom { get { return -_bounds.y + _spriteBorder; } }

    #endregion
    private void Start()
    {
        _bounds = new Vector3(41, 41);
    }
    public static bool OutOfBounds(Vector2 position) //вышел за пределы экрана
    {
        float x = Mathf.Abs(position.x);
        float y = Mathf.Abs(position.y);
        return (x > _bounds.x || y > _bounds.y);
    }
}
