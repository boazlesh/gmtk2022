using Assets.Scripts;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHud : MonoBehaviour
{
    [SerializeField] private ActionBlock _actionBlockRed;
    [SerializeField] private ActionBlock _actionBlockGreen;
    [SerializeField] private ActionBlock _actionBlockBlue;

    public void UpdateActions(Dictionary<FaceColor, ActionInstance> actionInstances)
    {
        UpdateActionBlock(actionInstances, FaceColor.Red, _actionBlockRed);
        UpdateActionBlock(actionInstances, FaceColor.Green, _actionBlockGreen);
        UpdateActionBlock(actionInstances, FaceColor.Blue, _actionBlockBlue);
    }

    private void UpdateActionBlock(Dictionary<FaceColor, ActionInstance> actionInstances, FaceColor faceColor, ActionBlock actionBlock)
    {
        if (actionInstances.ContainsKey(faceColor))
        {
            actionBlock.SetAction(actionInstances[faceColor].Action);
            actionBlock.SetFaceColor(faceColor, mute: false);
            actionBlock.SetText(actionInstances[faceColor].Potency.ToString());
        }
        else
        {
            actionBlock.SetFaceColor(faceColor, mute: true);
            actionBlock.SetText(null);
        }
    }
}
