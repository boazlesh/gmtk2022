using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CustomScreen : MonoBehaviour
    {
        private const float rollTimeSeconds = 0.5f;

        [SerializeField] private ActionModel[] _actionLibrary;

        [SerializeField] private ActionBlock _redActionBlock;
        [SerializeField] private ActionBlock _greenActionBlock;
        [SerializeField] private ActionBlock _blueActionBlock;

        [SerializeField] private DieRollWindow _redDieRollWindow;
        [SerializeField] private DieRollWindow _greenDieRollWindow;
        [SerializeField] private DieRollWindow _blueDieRollWindow;

        [SerializeField] private TextMeshProUGUI _diceLeftText;
        [SerializeField] private Button _submitButton;

        private Input _input;
        private DieRollWindow _activeDieRollWindow;
        private int _diceLeft;
        private bool _isSubmitted = false;

        private Input Input
        {
            get
            {
                if (_input == null)
                {
                    _input = new Input();
                }

                return _input;
            }
        }

        private void Start()
        {
            _redDieRollWindow.OnRoll += OnDieRoll;
            _greenDieRollWindow.OnRoll += OnDieRoll;
            _blueDieRollWindow.OnRoll += OnDieRoll;

            _redDieRollWindow.OnRolled += OnDieRolled;
            _greenDieRollWindow.OnRolled += OnDieRolled;
            _blueDieRollWindow.OnRolled += OnDieRolled;

            Input.BattleActionMap.CustomScreen.performed += _ => Submit();
            Input.BattleActionMap.MoveLeft.performed += _ => MoveLeft();
            Input.BattleActionMap.MoveRight.performed += _ => MoveRight();
            Input.BattleActionMap.UseTopAbility.performed += _ => RollMore();
        }

        public IEnumerator SelectActionsRoutine()
        {
            gameObject.SetActive(true);

            Input.Enable();

            PauseUnpauseAllProjectilesThxbby(pauseOrUnpause: true);

            _isSubmitted = false;

            AudioManager.Instance.ChillOut();

            SetDiceLeft(5);
            SetActions();
            SetSubmitInteractable(false);
            SetDiceRollsInteractable(false);

            yield return RollDiceRoutine();

            _activeDieRollWindow = _redDieRollWindow;
            _activeDieRollWindow.SetHighlighted(true);

            yield return new WaitUntil(() => _isSubmitted);

            var actionInstances = new Dictionary<FaceColor, ActionInstance>();

            if (!_redDieRollWindow.IsBust())
            {
                actionInstances.Add(FaceColor.Red, new ActionInstance { Action = _redActionBlock.GetAction(), Potency = _redDieRollWindow.GetSum() });
            }

            if (!_greenDieRollWindow.IsBust())
            {
                actionInstances.Add(FaceColor.Green, new ActionInstance { Action = _greenActionBlock.GetAction(), Potency = _greenDieRollWindow.GetSum() });
            }

            if (!_blueDieRollWindow.IsBust())
            {
                actionInstances.Add(FaceColor.Blue, new ActionInstance { Action = _blueActionBlock.GetAction(), Potency = _blueDieRollWindow.GetSum() });
            }

            yield return actionInstances;

            gameObject.SetActive(false);

            Input.Disable();

            _activeDieRollWindow?.SetHighlighted(false);
            _activeDieRollWindow = null;

            PauseUnpauseAllProjectilesThxbby(pauseOrUnpause: false);

            AudioManager.Instance.BlastIt();
        }

        private void SetActions()
        {
            _redActionBlock.SetFaceColor(FaceColor.Red);
            _redActionBlock.SetAction(_actionLibrary[0]);
            _redActionBlock.SetText(null);

            _greenActionBlock.SetFaceColor(FaceColor.Green);
            _greenActionBlock.SetAction(_actionLibrary[1]);
            _greenActionBlock.SetText(null);

            _blueActionBlock.SetFaceColor(FaceColor.Blue);
            _blueActionBlock.SetAction(_actionLibrary[2]);
            _blueActionBlock.SetText(null);
        }

        public void SetDiceLeft(int diceLeft)
        {
            _diceLeft = diceLeft;

            _diceLeftText.text = $"Extra dice left: {_diceLeft}";

            bool hasDiceLeft = _diceLeft > 0;

            SetDiceRollsInteractable(hasDiceLeft);

            if (!hasDiceLeft)
            {
                _activeDieRollWindow = null;
            }
            else
            {
                _activeDieRollWindow?.SetHighlighted(true);
            }
        }

        private IEnumerator RollDiceRoutine()
        {
            yield return _redDieRollWindow.RollRoutine(rollTimeSeconds);
            yield return _greenDieRollWindow.RollRoutine(rollTimeSeconds);
            yield return _blueDieRollWindow.RollRoutine(rollTimeSeconds);
        }

        private void OnDieRoll()
        {
            SetSubmitInteractable(false);
            SetDiceRollsInteractable(false);

            _activeDieRollWindow?.SetHighlighted(true);
        }

        private void SetSubmitInteractable(bool interactable)
        {
            _submitButton.interactable = interactable;
        }

        private void SetDiceRollsInteractable(bool interactable)
        {
            if (!_redDieRollWindow.IsBust())
            {
                _redDieRollWindow.SetInteractable(interactable);
            }

            if (!_greenDieRollWindow.IsBust())
            {
                _greenDieRollWindow.SetInteractable(interactable);
            }

            if (!_blueDieRollWindow.IsBust())
            {
                _blueDieRollWindow.SetInteractable(interactable);
            }
        }

        private void OnDieRolled()
        {
            SetSubmitInteractable(true);

            SetDiceLeft(_diceLeft - 1);
        }

        public void Submit()
        {
            if (!_submitButton.interactable)
            {
                return;
            }

            _isSubmitted = true;
        }

        private void PauseUnpauseAllProjectilesThxbby(bool pauseOrUnpause)
        {
            foreach (var x in FindObjectsOfType<Projectile>())
            {
                x.PausePleaseThanks = pauseOrUnpause;
            }
        }

        private void MoveLeft()
        {
            if (_activeDieRollWindow == null)
            {
                return;
            }

            _activeDieRollWindow.SetHighlighted(false);

            if (_activeDieRollWindow == _greenDieRollWindow)
            {
                _activeDieRollWindow = _redDieRollWindow;
            }
            else if (_activeDieRollWindow == _blueDieRollWindow)
            {
                _activeDieRollWindow = _greenDieRollWindow;
            }
            else
            {
                _activeDieRollWindow = _blueDieRollWindow;
            }

            _activeDieRollWindow.SetHighlighted(true);
        }

        private void MoveRight()
        {
            if (_activeDieRollWindow == null)
            {
                return;
            }

            _activeDieRollWindow.SetHighlighted(false);

            if (_activeDieRollWindow == _redDieRollWindow)
            {
                _activeDieRollWindow = _greenDieRollWindow;
            }
            else if (_activeDieRollWindow == _greenDieRollWindow)
            {
                _activeDieRollWindow = _blueDieRollWindow;
            }
            else
            {
                _activeDieRollWindow = _redDieRollWindow;
            }

            _activeDieRollWindow.SetHighlighted(true);
        }

        private void RollMore()
        {
            if (_activeDieRollWindow == null)
            {
                return;
            }

            _activeDieRollWindow.Roll(1);
        }
    }
}
