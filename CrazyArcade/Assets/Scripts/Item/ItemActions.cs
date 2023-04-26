using System;
using UnityEngine;

public static class ItemActions
{
    public static Action<Character>[] itemActions = new Action<Character>[4];
    
    public static void SaveActions()
    {
        itemActions[0] += NiddleAction;
    }

    private static void NiddleAction(Character useCharacter)
    {
        AudioManager.Instance.PlaySFX("bubble_pop");
        useCharacter.GetComponent<Animator>().SetTrigger(PlayableCharacter.PlayerAnimID.REVIVAL);
    }
}
