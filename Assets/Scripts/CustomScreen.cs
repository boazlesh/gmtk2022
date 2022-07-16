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

        private bool _isSubmitted = false;

        private void Start()
        {
            StartCoroutine(SelectActionsRoutine());
        }

        public IEnumerator SelectActionsRoutine()
        {
            _isSubmitted = false;

            SetActions();

            RollDice();

            yield return new WaitUntil(() => _isSubmitted);

            List<ActionInstance> actionInstances = new List<ActionInstance>();

            if (!_redDieRollWindow.IsBust())
            {
                actionInstances.Add(new ActionInstance { Action = _redActionBlock.GetAction(), Potency = _redDieRollWindow.GetSum() });
            }

            if (!_blueDieRollWindow.IsBust())
            {
                actionInstances.Add(new ActionInstance { Action = _blueActionBlock.GetAction(), Potency = _blueDieRollWindow.GetSum() });
            }

            if (!_greenDieRollWindow.IsBust())
            {
                actionInstances.Add(new ActionInstance { Action = _greenActionBlock.GetAction(), Potency = _greenDieRollWindow.GetSum() });
            }

            yield return actionInstances;
        }

        private void SetActions()
        {
            _redActionBlock.SetFaceColor(FaceColor.Red);
            _redActionBlock.SetAction(_actionLibrary[0]);

            _blueActionBlock.SetFaceColor(FaceColor.Blue);
            _blueActionBlock.SetAction(_actionLibrary[1]);

            _greenActionBlock.SetFaceColor(FaceColor.Green);
            _greenActionBlock.SetAction(_actionLibrary[2]);
        }

        private void RollDice()
        {
            _redDieRollWindow.Roll();
            _blueDieRollWindow.Roll();
            _greenDieRollWindow.Roll();
        }

        public void Submit()
        {
            _isSubmitted = true;
        }
    }
}
