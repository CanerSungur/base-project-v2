using UnityEngine;

public abstract class CollectableBase : MonoBehaviour, ICollectable
{
    [Tooltip("Is this object collectable in scene or rewarded when something happens.")] public CollectableStyle CollectableStyle;
    [Tooltip("Select movement type of this object when it's collected.")] public CollectStyle CollectStyle;
    [SerializeField, Tooltip("Particle effect for collecting this object.")] protected GameObject collectEffect;

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

    public void Dispose()
    {
        if (CollectStyle == CollectStyle.OnSite)
            Destroy(gameObject);
    }
}
public enum CollectStyle { OnSite, MoveToUI }
public enum CollectableStyle { Collect, Reward }
