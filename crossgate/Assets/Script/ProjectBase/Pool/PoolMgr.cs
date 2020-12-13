using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 缓存池模块
// 1.Dictionary List
// 2. GameObje 和 Resources
// 相对占用内存 减少GC Cpu



public class PoolData{
    public GameObject fatherObj;
    public List<GameObject> poolList;

    public PoolData(GameObject obj,GameObject poolObj){
        fatherObj = new GameObject(obj.name);
        fatherObj.transform.parent = poolObj.transform;

        poolList = new List<GameObject>(){};
        PushObj(obj);
    }
     public void PushObj(GameObject obj)
    {
        //存起来
        poolList.Add(obj);
        //设置父对象
        obj.transform.parent = fatherObj.transform;
        //失活，让其隐藏
        obj.SetActive(false);
    }
     //向抽屉中取东西
    public GameObject GetObj() {
        GameObject obj = null;
        //取出第一个
        obj = poolList[0];
        poolList.RemoveAt(0);
        //激活，让其展示
        obj.SetActive(true);
        //断开父子关系
        obj.transform.parent = null;
 
        return obj;
    }

}

public class PoolMgr : BaseManager<PoolMgr>
{
    
    //缓存池容器
    public Dictionary<string,PoolData> poolDic = new Dictionary<string,PoolData>();
    private GameObject poolObj;

    public GameObject GetObj(string name){
        GameObject obj = null;

        if(poolDic.ContainsKey(name) && poolDic[name].poolList.Count > 0){
           
            obj = poolDic[name].GetObj();
        }else{
           
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
        }
        // //激活
        // obj.SetActive(true);
        // //断开了缓存池物体与poolObj的父子关系
        // obj.transform.parent = null;
        return obj;
    }

    public void PushObj(string name,GameObject obj){
        if (poolObj == null)
        {
            poolObj = new GameObject("Pool");
        }
     
        if(poolDic.ContainsKey(name)){ //有抽屉 直接替换
            poolDic[name].PushObj(obj);
        }else{ //没有抽屉 添加一个抽屉
            poolDic.Add(name,new PoolData(obj,poolObj));
        }
    }

    //清空缓存池的方法
    //主要用在场景切换时
    public void Clear() {
        poolDic.Clear();
        poolObj = null;
    }

}
