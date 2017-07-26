using System;
using UnityEngine;
using System.Collections.Generic;

public enum EventListenerType
{
    TriggertEnter,
    TriggertExit,
    TriggertStay,
    CollisionEnter,
    CollisionStay,
    CollisionExit,
    InputKey,
    InputAxis,

    ChangeCraneParameter,
}

public static class EventListenerManager
{
    #region 事件监听列表
    /// <summary>
    /// 无参数监听字典
    /// </summary>
    private static Dictionary<object, List<Delegate>> m_eventListenerDic = new Dictionary<object, List<Delegate>>();

    internal static void RemoveEventListener<T>(EventListenerType changeCraneParameter, T m_CraneParameterVO)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 一个参数监听字典
    /// </summary>
    private static Dictionary<object, List<Delegate>> m_eventListenerDic_1 = new Dictionary<object, List<Delegate>>();
    /// <summary>
    /// 两个参数监听字典
    /// </summary>
    private static Dictionary<object, List<Delegate>> m_eventListenerDic_2 = new Dictionary<object, List<Delegate>>();
    /// <summary>
    /// 三个参数监听字典
    /// </summary>
    private static Dictionary<object, List<Delegate>> m_eventListenerDic_3 = new Dictionary<object, List<Delegate>>();
    #endregion 事件监听列表

    #region Atheos
    public static void AddEventListener(EventListenerType enEventType, Action callback)
    {
        lock (m_eventListenerDic)
        {
            if (!m_eventListenerDic.ContainsKey(enEventType))
            {
                m_eventListenerDic.Add(enEventType, new List<Delegate>());
            }
            if (m_eventListenerDic[enEventType].Contains(callback))
            {
                Debug.Log("该事件已经注册:  " + "事件type" + enEventType.ToString() + "callback:" + callback.ToString());
            }
            else
            {
                m_eventListenerDic[enEventType].Add(callback);
            }
        }
    }
    public static void AddEventListener<T>(EventListenerType enEventType, Action<T> callback)
    {
        lock (m_eventListenerDic_1)
        {
            if (!m_eventListenerDic_1.ContainsKey(enEventType))
            {
                m_eventListenerDic_1.Add(enEventType, new List<Delegate>());
            }
            if (m_eventListenerDic_1[enEventType].Contains(callback))
            {
                Debug.Log("该事件已经注册:  " + "事件type" + enEventType.ToString() + "callback:" + callback.ToString());
            }
            else
            {
                m_eventListenerDic_1[enEventType].Add(callback);
            }
        }
    }

    public static void AddEventListener<T, V>(EventListenerType enEventType, Action<T, V> callback)
    {
        lock (m_eventListenerDic_2)
        {
            if (!m_eventListenerDic_2.ContainsKey(enEventType))
            {
                m_eventListenerDic_2.Add(enEventType, new List<Delegate>());
            }
            if (m_eventListenerDic_2[enEventType].Contains(callback))
            {
                Debug.Log("该事件已经注册:  " + "事件type" + enEventType.ToString() + "callback:" + callback.ToString());
            }
            else
            {
                m_eventListenerDic_2[enEventType].Add(callback);
            }
        }
    }

    public static void AddEventListener<T, V, K>(EventListenerType enEventType, Action<T, V, K> callback)
    {
        lock (m_eventListenerDic_3)
        {
            if (!m_eventListenerDic_3.ContainsKey(enEventType))
            {
                m_eventListenerDic_3.Add(enEventType, new List<Delegate>());
            }
            if (m_eventListenerDic_3[enEventType].Contains(callback))
            {
                Debug.Log("该事件已经注册:  " + "事件type" + enEventType.ToString() + "callback:" + callback.ToString());
            }
            else
            {
                m_eventListenerDic_3[enEventType].Add(callback);
            }
        }
    }
    public static void RemoveEventListener(EventListenerType enEventType, Action callback)
    {
        lock (m_eventListenerDic)
        {
            if (m_eventListenerDic.ContainsKey(enEventType))
            {
                if (m_eventListenerDic[enEventType].Contains(callback))
                {
                    m_eventListenerDic[enEventType].Remove(callback);
                }
                else
                {
                    Debug.Log("该事件尚未注册：  " + "事件ype:" + enEventType.ToString() + "callback:" + callback.ToString());
                }
            }
        }
    }
    public static void RemoveEventListener<T>(EventListenerType enEventType, Action<T> callback)
    {
        lock (m_eventListenerDic_1)
        {
            if (m_eventListenerDic_1.ContainsKey(enEventType))
            {
                if (m_eventListenerDic_1[enEventType].Contains(callback))
                {
                    m_eventListenerDic_1[enEventType].Remove(callback);
                }
                else
                {
                    Debug.Log("该事件尚未注册：  " + "事件ype:" + enEventType.ToString() + "callback:" + callback.ToString());
                }
            }
        }
    }
    public static void RemoveEventListener<T, V>(EventListenerType enEventType, Action<T, V> callback)
    {
        lock (m_eventListenerDic_2)
        {
            if (m_eventListenerDic_2.ContainsKey(enEventType))
            {
                if (m_eventListenerDic_2[enEventType].Contains(callback))
                {
                    m_eventListenerDic_2[enEventType].Remove(callback);
                }
                else
                {
                    Debug.Log("该事件尚未注册：  " + "事件ype:" + enEventType.ToString() + "callback:" + callback.ToString());
                }
            }
        }
    }

    public static void RemoveEventListener<T, V, K>(EventListenerType enEventType, Action<T, V, K> callback)
    {
        lock (m_eventListenerDic_3)
        {
            if (m_eventListenerDic_3.ContainsKey(enEventType))
            {
                if (m_eventListenerDic_3[enEventType].Contains(callback))
                {
                    m_eventListenerDic_3[enEventType].Remove(callback);
                }
                else
                {
                    Debug.Log("该事件尚未注册：  " + "事件ype:" + enEventType.ToString() + "callback:" + callback.ToString());
                }
            }
        }
    }

    public static void Trigger(EventListenerType enEventType)
    {
        lock (m_eventListenerDic)
        {
            if (m_eventListenerDic.ContainsKey(enEventType))
            {
                //把回调事件缓存起来，防止在执行的时候对链表进行了操作，导致某些回调未执行
                List<Delegate> tempList = new List<Delegate>();
                for (int i = 0; i < m_eventListenerDic[enEventType].Count; i++)
                {
                    tempList.Add(m_eventListenerDic[enEventType][i]);
                }
                for (int i = 0; i < tempList.Count; i++)
                {
                    Action callback = (Action)tempList[i];
                    if (callback != null)
                    {
                        callback();
                    }
                }
            }
        }
    }
    public static void Trigger<T>(EventListenerType enEventType, T arg)
    {
        lock (m_eventListenerDic_1)
        {
            if (m_eventListenerDic_1.ContainsKey(enEventType))
            {
                //把回调事件缓存起来，防止在执行的时候对链表进行了操作，导致某些回调未执行
                List<Delegate> tempList = new List<Delegate>();
                for (int i = 0; i < m_eventListenerDic_1[enEventType].Count; i++)
                {
                    tempList.Add(m_eventListenerDic_1[enEventType][i]);
                }
                for (int i = 0; i < tempList.Count; i++)
                {
                    Action<T> callback = (Action<T>)tempList[i];
                    if (callback != null)
                    {
                        callback(arg);
                    }
                }
            }
        }
    }
    public static void Trigger<T, V>(EventListenerType enEventType, T arg1, V arg2)
    {
        lock (m_eventListenerDic_2)
        {
            if (m_eventListenerDic_2.ContainsKey(enEventType))
            {
                //把回调事件缓存起来，防止在执行的时候对链表进行了操作，导致某些回调未执行
                List<Delegate> tempList = new List<Delegate>();
                for (int i = 0; i < m_eventListenerDic_2[enEventType].Count; i++)
                {
                    tempList.Add(m_eventListenerDic_2[enEventType][i]);
                }
                for (int i = 0; i < tempList.Count; i++)
                {
                    Action<T, V> callback = (Action<T, V>)tempList[i];
                    if (callback != null)
                    {
                        callback(arg1, arg2);
                    }
                }
            }
        }
    }
    public static void Trigger<T, V, K>(EventListenerType enEventType, T arg1, V arg2, K arg3)
    {
        lock (m_eventListenerDic_3)
        {
            if (m_eventListenerDic_3.ContainsKey(enEventType))
            {
                //把回调事件缓存起来，防止在执行的时候对链表进行了操作，导致某些回调未执行
                List<Delegate> tempList = new List<Delegate>();
                for (int i = 0; i < m_eventListenerDic_3[enEventType].Count; i++)
                {
                    tempList.Add(m_eventListenerDic_3[enEventType][i]);
                }
                for (int i = 0; i < tempList.Count; i++)
                {
                    Action<T, V, K> callback = (Action<T, V, K>)tempList[i];
                    if (callback != null)
                    {
                        callback(arg1, arg2, arg3);
                    }
                }
            }
        }
    }
    #endregion Atheos
}