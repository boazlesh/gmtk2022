using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts
{
    public class Instructions : MonoBehaviour
    {
        [SerializeField] private LevelLoader _levelLoader;

        private Input _input;

        private void Awake()
        {
            _input = new Input();

            _input.BattleActionMap.Continue.performed += ContinuePerformed;

            _input.Enable();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Disable();
        }

        private void ContinuePerformed(InputAction.CallbackContext obj)
        {
            _levelLoader.LoadNextLevel();
        }
    }
}
