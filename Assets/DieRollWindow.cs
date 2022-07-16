using Assets.Scripts;
using UnityEngine;

namespace Assets
{
    public class DieRollWindow : MonoBehaviour
    {
        [SerializeField] private FaceColor _faceColor;
        [SerializeField] private Die _diePrefab;
        [SerializeField] private Transform _content;

        public void AddDie(DieFace dieFace)
        {
            Die die = Instantiate(_diePrefab, _content);

            die.SetColor(_faceColor);
            die.SetFace(dieFace);
        }

        [ContextMenu("Add Die")]
        public void AddDieDebug()
        {
            AddDie(DieFace.Five);
        }
    }
}
