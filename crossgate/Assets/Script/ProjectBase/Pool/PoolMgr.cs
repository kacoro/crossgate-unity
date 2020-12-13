using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 缓存池模块
// 1.Dictionary List
// 2. GameObje 和 Resources
// 相对占用内存 减少GC Cpu
public class PoolMgr : BaseManager<PoolMgr>
{
    
    //缓存池容器
    public Dictionary<string,List<GameObject>> poolDic = new Dictionary<string,List<GameObject>>();
    private GameObject poolObj;

    public GameObject GetObj(string name){
        GameObject obj = null;

        if(poolDic.ContainsKey(name) && poolDic[name].Count > 0){
            Debug.Log(name);
            obj = poolDic[name][0];
            poolDic[name].RemoveAt(0);
        }else{
             Debug.Log("null");
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        //激活
        obj.SetActive(true);
        //断开了缓存池物体与poolObj的父子关系
        obj.transform.parent = null;
        return obj;
    }

    public void PushObj(string name,GameObject obj){
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }
        //实现所有申请的对象都放在Pool这个空物体下面，当某个对象物体被激活才会回到主目录下。
        obj.transform.parent = poolObj.transform;
        //失活
        obj.SetActive(false);
        if(poolDic.ContainsKey(name)){ //有抽屉 直接替换
            poolDic[name].Add(obj);
        }else{ //没有抽屉 添加一个抽屉
            poolDic.Add(name,new List<GameObject>(){obj});
        }
    }

    //清空缓存池的方法
    //主要用在场景切换时
    public void Clear() {
        poolDic.Clear();
        poolObj = null;
    }

}
