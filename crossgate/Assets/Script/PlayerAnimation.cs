using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{   
    
    private Animator anim;

    public string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
    public string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE" };

    int lastDireaction;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        float result1 = Vector2.SignedAngle(Vector2.up, Vector2.right);
        Debug.Log("R1" + result1);
        float result2 = Vector2.SignedAngle(Vector2.up, Vector2.left);
        Debug.Log("R2" + result2);
    }

    public void SetDirection(Vector2 _direction)
    {
        string[] directionArray = null;
        if(_direction.magnitude <0.01) //MARKER character is static. 
        {
            directionArray = staticDirections;
        }
        else
        {
            directionArray = runDirections;
            lastDireaction = DirectionToIndex(_direction);
        }
        anim.Play(directionArray[lastDireaction]);
    }

    private int DirectionToIndex(Vector2 _direction)
    {
        Vector2 norDir = _direction.normalized;
        float step = 360 / 8;

        float offset = step / 2;

        float angle = Vector2.SignedAngle(Vector2.up, norDir);

        angle += offset;
        if (angle < 0)
        {
            angle += 360;
        }
        float stepCount = angle / step;
        return Mathf.FloorToInt(stepCount);
    }


}
