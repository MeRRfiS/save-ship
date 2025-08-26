using UnityEngine;

namespace Assets.Project.Scripts.EntityFeatures.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Scriptable Objects/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float InteractionRadius { get; private set; }
        [field: SerializeField] public LayerMask InteractableLayer { get; private set; }
    }
}