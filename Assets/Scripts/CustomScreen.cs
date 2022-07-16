﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts
{
    public class CustomScreen : MonoBehaviour
    {
        private const float rollTimeSeconds = 0.6f;

        [SerializeField] private ActionModel[] _actionLibrary;

        [SerializeField] private ActionBlock _redActionBlock;
        [SerializeField] private ActionBlock _blueActionBlock;
        [SerializeField] private ActionBlock _greenActionBlock;

        [SerializeField] private DieRollWindow _redDieRollWindow;
        [SerializeField] private DieRollWindow _blueDieRollWindow;
        [SerializeField] private DieRollWindow _greenDieRollWindow;

        [SerializeField] private TextMeshProUGUI _diceLeftText;

        private int _diceLeft;
        private bool _isSubmitted = false;

        private void Start()
        {
            _redDieRollWindow.OnRoll += OnDieRoll;
            _blueDieRollWindow.OnRoll += OnDieRoll;
            _greenDieRollWindow.OnRoll += OnDieRoll;

            _redDieRollWindow.OnRolled += OnDieRolled;
            _blueDieRollWindow.OnRolled += OnDieRolled;
            _greenDieRollWindow.OnRolled += OnDieRolled;
        }

        public IEnumerator SelectActionsRoutine()
        {
            gameObject.SetActive(true);

            _isSubmitted = false;

            SetDiceLeft(5);
            SetActions();
            yield return RollDiceRoutine();

            yield return new WaitUntil(() => _isSubmitted);

            var actionInstances = new Dictionary<FaceColor, ActionInstance>();

            if (!_redDieRollWindow.IsBust())
            {
                actionInstances.Add(FaceColor.Red, new ActionInstance { Action = _redActionBlock.GetAction(), Potency = _redDieRollWindow.GetSum() });
            }

            if (!_blueDieRollWindow.IsBust())
            {
                actionInstances.Add(FaceColor.Blue, new ActionInstance { Action = _blueActionBlock.GetAction(), Potency = _blueDieRollWindow.GetSum() });
            }

            if (!_greenDieRollWindow.IsBust())
            {
                actionInstances.Add(FaceColor.Green, new ActionInstance { Action = _greenActionBlock.GetAction(), Potency = _greenDieRollWindow.GetSum() });
            }

            yield return actionInstances;

            gameObject.SetActive(false);
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

        public void SetDiceLeft(int diceLeft)
        {
            _diceLeft = diceLeft;

            _diceLeftText.text = $"Extra dice left: {_diceLeft}";

            bool hasDiceLeft = _diceLeft > 0;

            SetRollWindowsInteractable(hasDiceLeft);
        }

        private IEnumerator RollDiceRoutine()
        {
            yield return _redDieRollWindow.RollRoutine(rollTimeSeconds);
            yield return _blueDieRollWindow.RollRoutine(rollTimeSeconds);
            yield return _greenDieRollWindow.RollRoutine(rollTimeSeconds);
        }

        private void OnDieRoll()
        {
            SetRollWindowsInteractable(false);
        }

        private void SetRollWindowsInteractable(bool interactable)
        {
            if (!_redDieRollWindow.IsBust())
            {
                _redDieRollWindow.SetInteractable(interactable);
            }

            if (!_blueDieRollWindow.IsBust())
            {
                _blueDieRollWindow.SetInteractable(interactable);
            }

            if (!_greenDieRollWindow.IsBust())
            {
                _greenDieRollWindow.SetInteractable(interactable);
            }
        }

        private void OnDieRolled()
        {
            // Set dice left already sets interaction
            SetDiceLeft(_diceLeft - 1);
        }

        public void Submit()
        {
            _isSubmitted = true;
        }
    }
}
