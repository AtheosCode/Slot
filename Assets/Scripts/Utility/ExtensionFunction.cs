using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// a set of extension methods meant help with common coroutine cases. Example :
/// <code>
/// void OnTriggerEnter(Collider col) {
///     if(col.gameObject.tag != "Ice")
///             return;
///     Freeze();
///     this.ExecuteLater(()=> Unfreeze(), 2f); // unfreezes the current gameObject 2 seconds from now.
/// }
///
/// </code>
/// </summary>

public static class ExtensionFunction
{
    #region Timing

    public delegate bool When();

    /// <summary>
    /// Execute the given Action when <code>condition</code> returns <code>true</code>.
    /// condition will be evaluated every frame.
    /// </summary>
    /// <param name="action">the action to execute</param>
    /// <param name="condition">Condition.</param>
    public static void ExecuteWhen(this MonoBehaviour m, Action action, When condition)
    {
        m.StartCoroutine(ExecuteWhenCoroutine(action, condition));
    }

    /// <summary>
    /// Execute the action after a delay of <code>seconds</code>
    /// </summary>
    /// <param name="action">Action.</param>
    /// <param name="seconds">Seconds.</param>
    public static void ExecuteLater(this MonoBehaviour m, Action action, float seconds)
    {
        m.StartCoroutine(ExecuteLaterCoroutine(action, seconds));
    }

    /// <summary>
    /// Execute an action next frame
    /// </summary>
    /// <param name="m">M.</param>
    /// <param name="action">Action.</param>
    public static void ExecuteNextFrame(this MonoBehaviour m, Action action)
    {
        m.StartCoroutine(ExecuteNextFrameCoroutine(action));
    }

    private static IEnumerator ExecuteWhenCoroutine(Action action, When condition)
    {
        while (!condition())
            yield return null;
        action();
    }

    private static IEnumerator ExecuteLaterCoroutine(Action action, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        action();
    }

    private static IEnumerator ExecuteNextFrameCoroutine(Action action)
    {
        yield return null;
        action();
    }

    public static void Co(this MonoBehaviour m, Func<IEnumerator> coroutine)
    {
        m.StartCoroutine(CoCoroutine(coroutine));
    }

    private static IEnumerator CoCoroutine(Func<IEnumerator> coroutine)
    {
        yield return coroutine;
    }

    #endregion Timing

    #region Transform

    // 扩展了只修改一个值的
    public static void SetPositionX(this Transform t, float newX)
    {
        t.position = new Vector3(newX, t.position.y, t.position.z);
    }

    public static void SetPositionY(this Transform t, float newY)
    {
        t.position = new Vector3(t.position.x, newY, t.position.z);
    }

    public static void SetPositionZ(this Transform t, float newZ)
    {
        t.position = new Vector3(t.position.x, t.position.y, newZ);
    }

    // 扩展了更快的获取一个值
    public static float GetPositionX(this Transform t)
    {
        return t.position.x;
    }

    public static float GetPositionY(this Transform t)
    {
        return t.position.y;
    }

    public static float GetPositionZ(this Transform t)
    {
        return t.position.z;
    }

    #endregion Transform

    #region Component

    /// <summary>
    /// Gets or add a component. Usage example:
    /// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
    /// </summary>
    public static T GetOrAddComponent<T>(this GameObject child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }

    public static T GetOrAddComponent<T>(this Transform child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }
        return result;
    }

    public static T GetSafeComponent<T>(this GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError(obj.name + " Expected to find component of type "
               + typeof(T) + " but found none", obj);
        }

        return component;
    }
    public static T GetSafeComponent<T>(this Transform obj) where T : Component
    {
        T component = obj.GetComponent<T>();

        if (component == null)
        {
            Debug.LogError(obj.name + " Expected to find component of type "
               + typeof(T) + " but found none", obj);
        }

        return component;
    }
    public static Transform FindSafe(this Transform obj,string name)
    {
        Transform target = obj.Find(name);

        if (target == null)
        {
            Debug.LogError(obj.name + " Expected to find  " + obj.name + "'Child node :" + name);
        }
        return target;
    }
    #endregion Component
}