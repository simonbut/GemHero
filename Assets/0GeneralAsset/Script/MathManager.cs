using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathManager : MonoBehaviour
{
    public static float ColorMagnitude(Color _color)
    {
        return Mathf.Sqrt(_color.a * _color.a + _color.b * _color.b + _color.r * _color.r + _color.g * _color.g);
    }


    public static float AbsSinWithInitialZeroPointFive(float t,float period = 1f)//initial value is 0.5f,than go to 1f , 0.5f , 0f , 0.5f
    {
        float result = 0f;
        result = (Mathf.Cos(t * 2 * Mathf.PI / period) + 1f) / 2f;

        return result;
    }

    public static float AbsSin(float t, float period = 1f)//initial value is 0f,than go to 1f , 0f
    {
        float result = 0f;
        result = (Mathf.Sin(t * 2 * Mathf.PI / period - Mathf.PI / 2f) + 1f) / 2f;

        return result;
    }

    public static float Sin(float t, float period = 1f)//initial value is 0f,than go to 1f , 0f
    {
        float result = 0f;
        result = Mathf.Sin(t * 2 * Mathf.PI / period);

        return result;
    }

    public static float Cos(float t, float period = 1f)//initial value is 0f,than go to 1f , 0f
    {
        float result = 0f;
        result = Mathf.Cos(t * 2 * Mathf.PI / period);

        return result;
    }

    public static float BoundingFloat(float t, float startVal, float endVal, float period,float boundCoe = 0f)
    {
        if (t > period)
        {
            return endVal;
        }
        return startVal + ((startVal / 16f + boundCoe) * AbsSin(t, period)) + ((endVal - startVal) * AbsSin(t, period * 2f));
    }

    public static Vector3 CirclePos(float t, float period)
    {
        return new Vector3(-Mathf.Sin(t * 2 * Mathf.PI / period), 0, Mathf.Cos(t * 2 * Mathf.PI / period));
    }

    public static Vector3 WorldPosToCanvasPos(Vector3 _pos)
    {
        Vector3 temp = RectTransformUtility.WorldToScreenPoint(Camera.main, _pos);

        return new Vector3(temp.x, temp.y, 0f);
    }

    public static Vector2 ScreenPosToCanvasPos(Vector2 _pos)
    {
        return new Vector2(_pos.x * 1600f / Screen.width, _pos.y * 900f / Screen.height);
    }
}
