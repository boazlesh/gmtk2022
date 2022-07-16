using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ActionBlock : MonoBehaviour
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] private Color _color;
    [SerializeField] private Image _actionImage;
    [SerializeField] private Image _outlineImage;
    [SerializeField] private ActionType _actionType;

    private void OnValidate()
    {
        _outlineImage.color = _color;

        SetAction(new ActionModel { Sprite = _actionSprite });
    }

    public void SetAction(ActionModel actionModel)
    {
        _actionImage.sprite = actionModel.Sprite;
        _actionType = actionModel.ActionType;
    }
}
