using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//事件中心模块（基于观察者设计模式）
public class EventCenter : BaseManager<EventCenter>
{
    //字典中，key对应着事件的名字，
    //value对应的是监听这个事件对应的委托方法们
    //添加事件监听
    //第一个参数：事件的名字
    //第二个参数：处理事件的方法
     private Dictionary<string, UnityAction<object>> eventDic
        = new Dictionary<string, UnityAction<object>>();
    public void AddEventListener(string name, UnityAction<object> action) {
        //有没有对应的事件监听
        //有的情况
        if (eventDic.ContainsKey(name))
        {
            eventDic[name] += action;
        }
        //没有的情况
        else {
            eventDic.Add(name, action);
        }
    }

    //通过事件名字进行事件触发
    public void EventTrigger(string name,object info) {
        //有没有对应的事件监听
        //有的情况（有人关心这个事件）
        if (eventDic.ContainsKey(name))
        {
            //调用委托（依次执行委托中的方法）
            eventDic[name](info);
        }
    }

     //移除对应的事件监听
    public void RemoveEventListener(string name, UnityAction<object> action) {
        if (eventDic.ContainsKey(name))
        {
            //移除这个委托
            eventDic[name] -= action;
        }
    }

    //清空所有事件监听(主要用在切换场景时)
    public void Clear() {
        eventDic.Clear();
    }
}
