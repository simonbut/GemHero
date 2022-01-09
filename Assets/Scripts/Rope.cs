using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject player;

    public LineRenderer rope;
    public LayerMask collMask;

    public List<Vector3> ropePositions = new List<Vector3>();

    public TextMesh lengthText;

    public static Rope Instance;
    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        ropePositions = new List<Vector3>();
        AddPosToRope(player.transform.position);
    }

    private void Update()
    {
        UpdateRopePositions();
        LastSegmentGoToPlayerPos();

        DetectCollisionEnter();
        if (ropePositions.Count > 1)
        {
            DetectCollisionExits();
        }

        lengthText.transform.position = player.transform.position;
        lengthText.text = GetRopeLength(Vector3.zero).ToString("0.0");
    }

    public float GetRopeLength(Vector3 _offset)
    {
        float result = 0f;
        for (int i = 0; i < ropePositions.Count - 1; i++)
        {
            result += (ropePositions[i + 1] - ropePositions[i]).magnitude;
        }
        result += (player.transform.position + _offset - ropePositions[ropePositions.Count - 1]).magnitude;

        return result;
    }

    private void DetectCollisionEnter()
    {
        Vector2 _v = rope.GetPosition(ropePositions.Count - 1);
        RaycastHit2D hit = Physics2D.Linecast(player.transform.position, _v, collMask);
        if (hit && hit.point != _v)
            //if (hit && hit.point != _v)
        {
            //print("colliding");
            AddPosToRope(hit.point);
        }
    }

    private void DetectCollisionExits()
    {
        Vector2 _v = rope.GetPosition(ropePositions.Count - 1);
        Vector2 _v2 = rope.GetPosition(ropePositions.Count - 2);
        Vector3 _projectionPoint = GetProjectionPoint(_v, player.transform.position, _v2);
        RaycastHit2D hit = Physics2D.Linecast(_projectionPoint, _v, collMask);
        if (_projectionPoint != player.transform.position && (!hit || hit.point == _v))
        {
            ropePositions.RemoveAt(ropePositions.Count - 1);
        }
    }

    private void AddPosToRope(Vector3 _pos)
    {
        ropePositions.Add(_pos);
    }

    private void UpdateRopePositions()
    {
        rope.positionCount = ropePositions.Count + 1;
        rope.SetPositions(ropePositions.ToArray());
    }

    private void LastSegmentGoToPlayerPos()
    {
        rope.SetPosition(rope.positionCount - 1, player.transform.position);
    }

    //https://forum.unity.com/threads/perpendicular-2d-vector.347451/
    Vector3 GetProjectionPoint(Vector3 A, Vector3 B, Vector3 C)
    {
        Vector3 force = Vector3.Project(A - B, C - B);
        Debug.DrawLine(B + force, A, Color.white);
        return B + force;
    }

}