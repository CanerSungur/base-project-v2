using UnityEngine;

public class InitManager : MonoBehaviour
{
    private void Start()
    {
        GameEvents.OnChangeScene?.Invoke();
    }
}
