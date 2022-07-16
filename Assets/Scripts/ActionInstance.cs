using UnityEngine;

namespace Assets.Scripts
{
    [CreateAssetMenu(menuName = "ScriptableObjects/ActionInstance")]
    public class ActionInstance : ScriptableObject
    {
        [SerializeField] public ActionModel Action;

        [SerializeField] public int Potency;
    }
}
