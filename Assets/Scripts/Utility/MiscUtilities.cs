using System;
using System.Collections;
using UnityEngine;

static class MiscUtilities
{
    /// <summary>
    /// Invokes the specified action after the specified period of time in seconds
    /// </summary>
    public static void DelayedInvoke(Action theDelegate, float time)
    {
        State.GameManager.StartCoroutine(ExecuteAfterTime(theDelegate, time));
    }

    internal static void DelayedInvoke(Action<bool> regenerate, float v)
    {
        State.GameManager.StartCoroutine(ExecuteAfterTime(regenerate, v));
    }

    private static IEnumerator ExecuteAfterTime(Action<bool> action, float v)
    {
        yield return new WaitForSeconds(v);
        try
        {
            action(false);
        }
        catch
        {

        }
    }

    private static IEnumerator ExecuteAfterTime(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        try
        {
            action();
        }
        catch
        {

        }

    }
}

