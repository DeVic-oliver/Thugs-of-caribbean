using TOC.Core.Items;
using UnityEngine;

namespace TOC.Core.EntitySystem
{
    [RequireComponent(typeof(Entity), typeof(Collider2D))]
    public class EntityInteractableItemWatcher : MonoBehaviour
    {
        [SerializeField] private Entity m_Entity;

        private void Start()
        {
            if (!m_Entity)
                Debug.LogError("Missing entity reference at" + gameObject.name);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<InteractableItem>(out var item))
                item.Use(m_Entity);
        }
    }
}