using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class MiscUtilities
{
    /// <summary>
    /// Invokes the specified action after the specified period of time
    /// </summary>
    public static void DelayedInvoke(Action theDelegate, float time)
    {
        State.GameManager.StartCoroutine(ExecuteAfterTime(theDelegate, time));
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

