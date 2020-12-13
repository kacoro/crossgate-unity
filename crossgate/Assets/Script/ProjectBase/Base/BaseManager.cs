using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. 泛型
//2. 单例模式
public class BaseManager<T> where T:new()
{
   private static T instance;
   public static T GetInstatnce(){
       if(instance == null)
            instance = new T();
       return instance;
   }
}
