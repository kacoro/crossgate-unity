using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//1. 泛型
//2. 单例模式
//3. 保护类型 虚函数

//继承 MonoBehaviour 的单例模式对象 需要保证场景只出现一次，建议使用SingleAutotonMono
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    // 继承Mono的脚本，不能够直接new
    // 只能通过拖动到对象上 或者通过加脚本的api AddComponent去加脚本
    // U3D内部帮助实例化它
    private static T instance;

    public static T GetInstance(){
        return instance;
    }
    protected virtual void Awake() {
            instance = this as T;
    }

}
