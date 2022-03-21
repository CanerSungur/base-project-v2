using System.Collections.Generic;
using UnityEngine;

namespace ZestGames.AI
{
    public static class Operation
    {
        /// <summary>
        /// Finds and returns the closest Transform from given list of transforms.
        /// </summary>
        /// <param name="thisTransform">Transform who is doing this search.</param>
        /// <param name="list">Transform list that we want to find the closest. Like a target list.</param>
        /// <returns></returns>
        public static Transform FindClosestTransform(Transform thisTransform, List<Transform> list)
        {
            if (list == null || list.Count == 0) return null;

            float shortestDistance = Mathf.Infinity;
            Transform closestTransform = null;

            for (int i = 0; i < list.Count; i++)
            {
                float distanceToTransform = (thisTransform.position - list[i].position).sqrMagnitude;
                if (distanceToTransform < shortestDistance && thisTransform != list[i])
                {
                    shortestDistance = distanceToTransform;
                    closestTransform = list[i];
                }
            }

            return closestTransform;
        }
    }
}
