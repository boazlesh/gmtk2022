using Assets.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class DieRollWindow : MonoBehaviour
    {
        public event Action OnRoll;
        public event Action OnRolled;

        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Die _diePrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _sumLabel;
        [SerializeField] private int _sum = 0;
        [SerializeField] private Button _button;
        [SerializeField] private int _bustThreshold;
        [SerializeField] private GameObject _hightlight;

        private List<Die> _dice = new List<Die>();

        private void OnEnable()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }

            _sumLabel.text = "0";
            _sum = 0;
            _dice.Clear();
            _button.interactable = true;
        }

        private void OnValidate()
        {
            foreach (Transform child in _content)
            {
                Die die = child.GetComponent<Die>();

                if (die != null)
                {
                    die.SetColor(_faceColor);
                }
            }
        }

        public void Roll(float rollTimeSeconds)
        {
            if (!GetInteractable())
            {
                return;
            }

            StartCoroutine(RollRoutine(rollTimeSeconds));
        }

        public IEnumerator RollRoutine(float rollTimeSeconds)
        {
            OnRoll?.Invoke();

            yield return AddDieRoutine(DieFaceHelper.Roll(), rollTimeSeconds);

            OnRolled?.Invoke();
        }

        public IEnumerator AddDieRoutine(DieFace dieFace, float rollTimeSeconds)
        {
            Die die = Instantiate(_diePrefab, _content);

            die.SetColor(_faceColor, mute: false);

            yield return die.RollRandomFacesRoutine(rollTimeSeconds);

            die.SetFace(dieFace);

            _sum += DieFaceHelper.GetNumericValue(dieFace);
            _dice.Add(die);

            if (IsBust())
            {
                SetInteractable(false);
                _sumLabel.text = "BUST!";
                foreach (Die currentDie in _dice)
                {
                    currentDie.SetColor(_faceColor, mute: true);
                }

                yield break;
            }

            _sumLabel.text = _sum.ToString();
        }

        public bool IsBust() => _sum > _bustThreshold;

        public int GetSum() => _sum;

        public bool SetInteractable(bool interactable)
        {
            _button.interactable = interactable;

            if (!interactable)
            {
                SetHighlighted(false);
            }

            return _button.interactable;
        }

        public bool GetInteractable() => _button.interactable;

        [ContextMenu("Add Die")]
        public IEnumerator AddDieDebug()
        {
            yield return AddDieRoutine(DieFace.Five, rollTimeSeconds: 0.5f);
        }

        public void SetHighlighted(bool highlight)
        {
            _hightlight.gameObject.SetActive(highlight);
        }
    }
}
