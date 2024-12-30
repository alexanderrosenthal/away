using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;

    string currentAnimation = "";
    private float animationLength = 0f;



    public void ChangeAnimation(string animation, float crossfade = 0.2f)
    {
        if (currentAnimation != animation)
        {
            currentAnimation = animation;
            animator.CrossFade(animation, crossfade);
        }
    }

    public float GetAnimationDuration(string animationStateName)
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationStateName)
            {
                animationLength = clip.length; // Verwende Float direkt
                //Debug.Log($"Die Länge der Animation '{animationStateName}' beträgt {animationLength} Sekunden.");
                return animationLength; // Rückgabe bei Treffer
            }
        }

        // Keine passende Animation gefunden
        Debug.Log("Es konnte keine passende Animation gefunden werden.");
        return animationLength; // Standardwert zurückgeben
    }
}
