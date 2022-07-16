using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ActionBlock : MonoBehaviour
{
    [SerializeField] private Sprite _actionSprite;
    [SerializeField] private FaceColor _faceColor;
    [SerializeField] private Image _actionImage;
    [SerializeField] private Image _outlineImage;
    [SerializeField] private FaceColorMapping _faceColorMapping;

    private ActionModel _actionModel; 

    private void OnValidate()
    {
        SetFaceColor(_faceColor);

        SetAction(new ActionModel { _actionType = ActionType.Cannon});
    }

    public void SetFaceColor(FaceColor faceColor)
    {
        Color color = _faceColorMapping.GetColorFromFaceColor(faceColor);

        _actionImage.color = color;
        _outlineImage.color = color;
    }

    public void SetAction(ActionModel actionModel)
    {
        _actionModel = actionModel;

        _actionImage.sprite = actionModel._sprite;
    }

    public ActionModel GetAction() => _actionModel;
}
