using System.Collections;
using UnityEngine;
using System;

namespace ZestGames.Utility
{
    public static class Delayer
    {
        /// <summary>
        /// Call this function if you want to delay a function.
        /// Any function can be but into this with lambda expression like '() =>'
        /// USAGE: this.DoActionAfterDelay(...);
        /// </summary>
        /// <param name="mono">This is required because Coroutine requires MonoBehaviour.</param>
        /// <param name="delayTime">Function will be executed after this time.</param>
        /// <param name="action">Function you want to delay.</param>
        public static void DoActionAfterDelay(this MonoBehaviour mono, float delayTime, Action action)
        {
            mono.StartCoroutine(ExecuteAction(delayTime, action));
        }

        private static IEnumerator ExecuteAction(float delayTime, Action action)
        {
            yield return new WaitForSeconds(delayTime);
            action.Invoke();
            yield break;
        }
    }
}