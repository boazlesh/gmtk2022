using Assets.Scripts;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class DieRollWindow : MonoBehaviour
    {
        public event Action OnRoll;

        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Die _diePrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private TextMeshProUGUI _sumLabel;
        [SerializeField] private int _sum = 0;
        [SerializeField] private Button _button;
        [SerializeField] private int _bustThreshold;

        private void Awake()
        {
            foreach (Transform child in _content)
            {
                Destroy(child.gameObject);
            }

            _sumLabel.text = "0";
            _sum = 0;
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

        public void Roll(float rollTimeSeconds) => StartCoroutine(RollRoutine(rollTimeSeconds));

        public IEnumerator RollRoutine(float rollTimeSeconds)
        {
            yield return AddDieRoutine(DieFaceHelper.Roll(), rollTimeSeconds);

            OnRoll?.Invoke();
        }

        public IEnumerator AddDieRoutine(DieFace dieFace, float rollTimeSeconds)
        {
            Die die = Instantiate(_diePrefab, _content);

            die.SetColor(_faceColor);

            yield return die.RollRandomFacesRoutine(rollTimeSeconds);

            die.SetFace(dieFace);

            _sum += DieFaceHelper.GetNumericValue(dieFace);

            if (IsBust())
            {
                SetInteractable(false);
                _sumLabel.text = "BUST!";

                yield break;
            }

            _sumLabel.text = _sum.ToString();
        }

        public bool IsBust() => _sum > _bustThreshold;

        public int GetSum() => _sum;

        public bool SetInteractable(bool interactable) => _button.interactable = interactable;

        [ContextMenu("Add Die")]
        public IEnumerator AddDieDebug()
        {
            yield return AddDieRoutine(DieFace.Five, rollTimeSeconds: 1f);
        }
    }
}
