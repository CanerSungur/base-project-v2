using UnityEngine;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    [SerializeField, Tooltip("Particle effect for collecting this object.")]protected GameObject collectEffect;

    /// <summary>
    /// If you override this function, write everything you want before base.Collect();
    /// </summary>
    public virtual void Collect()
    {
        // You can add sound here.

        if (collectEffect)
        {
            ParticleSystem ps = Instantiate(collectEffect, transform.position, Quaternion.identity).GetComponent<ParticleSystem>();
            ps.Play();
            Destroy(ps.gameObject, ps.main.duration);
        }

        Apply();
        Dispose();
    }

    public abstract void Apply();

    public void Dispose() => Destroy(gameObject);
}
