using UnityEngine;

namespace ZestGames.AI
{
    public static class Movement
    {
        /// <summary>
        /// Moves object towards given Vector3 Target with given speed using Translate.
        /// </summary>
        /// <param name="thisTransform">Transform of this object.</param>
        /// <param name="target">Where to move.</param>
        /// <param name="speed"></param>
        public static void MoveTransform(Transform thisTransform, Vector3 target, float speed = 2f)
        {
            Vector3 direction = target - thisTransform.position;

            thisTransform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        }

        /// <summary>
        /// Moves object towards given Vector3 Target with given speed using Add Force.
        /// </summary>
        /// <param name="thisRigidbody">Rigidbody of this object.</param>
        /// <param name="thisTransform">Transform of this object.</param>
        /// <param name="target">Where to move.</param>
        /// <param name="speed"></param>
        public static void MoveRigidbody(Rigidbody thisRigidbody, Transform thisTransform, Vector3 target, float speed = 100f)
        {
            Vector3 direction = target - thisTransform.position;

            thisRigidbody.AddForce(direction * speed * Time.deltaTime);
        }

        /// <summary>
        /// Cheks if object has reached Target point according to given distance limit.
        /// </summary>
        /// <param name="thisTransform"></param>
        /// <param name="target">This point will be checked if reached or not.</param>
        /// <param name="limit">This is distance limit. If we're closer than this limit we've reached. Default value: 2f</param>
        /// <returns>True if reached, False if not.</returns>
        public static bool IsTargetReached(Transform thisTransform, Vector3 target, float limit = 2f)
        {
            return (target - thisTransform.position).sqrMagnitude <= limit ? true : false;
        }

        /// <summary>
        /// Makes object look towards the given Vector3 Target with given turn speed.
        /// </summary>
        /// <param name="thisTransform">Transform of this object.</param>
        /// <param name="target">Where to look.</param>
        /// <param name="turnSpeed"></param>
        public static void LookAtTarget(Transform thisTransform, Vector3 target, float turnSpeed = 5f)
        {
            Vector3 direction = target - thisTransform.position;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            Vector3 rotation = Quaternion.Lerp(thisTransform.rotation, lookRotation, turnSpeed * Time.deltaTime).eulerAngles;
            thisTransform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
    }
}