using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseObj : MonoBehaviour //基类
{
   protected abstract void MoveTo(Vector3 go,float dis);
}
