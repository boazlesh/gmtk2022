using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomScreen : MonoBehaviour
    {
        [SerializeField] private ActionModel[] _actionLibrary;

        [SerializeField] private ActionBlock _redActionBlock;
        [SerializeField] private ActionBlock _blueActionBlock;
        [SerializeField] private ActionBlock _greenActionBlock;

        [SerializeField] private DieRollWindow _redDieRollWindow;
        [SerializeField] private DieRollWindow _blueDieRollWindow;
        [SerializeField] private DieRollWindow _greenDieRollWindow;

        private void Awake()
        {
            SetActions();
        }

        public IEnumerator SelectActionsRoutine()
        {
            yield return new List<ActionInstance> { new ActionInstance { Action = _actionLibrary[0], Potency = 1 } };
        }

        private void SetActions()
        {
            _redActionBlock.SetAction(_actionLibrary[0]);
            _blueActionBlock.SetAction(_actionLibrary[1]);
            _greenActionBlock.SetAction(_actionLibrary[2]);
        }
    }
}
