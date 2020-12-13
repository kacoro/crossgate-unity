using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//1. 泛型
//2. 单例模式
//3. 保护类型 虚函数

//继承遮罩自动创建的单例模式基类 不需要手动去挂载脚本
//想用的时候直接GetInstatnce就好了
public class SingletonAutoMonoo<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T GetInstance(){
        if( instance == null){
            GameObject obj = new GameObject();
            //设置对象的名字为脚本名
            obj.name = typeof(T).ToString();
            //让这个单例模式对象过场景不移除
            //因为单例模式对象 往往存在于整个程序的生命周期中的
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
    protected virtual void Awake() {
            instance = this as T;
    }

}

