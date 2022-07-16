using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ActionModel")]
    public class ActionModel : ScriptableObject
    {
        [SerializeField] public Sprite _sprite;

        [SerializeField] public ActionType _actionType;

        [SerializeField] public Projectile _projectilePrefab;

        [SerializeField] public bool _heal;
    }
}
