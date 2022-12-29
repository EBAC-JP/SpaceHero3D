using System.Security.AccessControl;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBase : MonoBehaviour {

    [SerializeField] Animator animator;
    [SerializeField] List<AnimationSetup> animationSetups;

    public void PlayAnimationByTrigger(AnimationType type) {
        AnimationSetup setup = animationSetups.Find(i => i.animationType == type);
        if (setup != null) animator.SetTrigger(setup.animationTrigger);
    }
}

public enum AnimationType {
    IDLE,
    RUN,
    DEATH,
    ATTACK
}

[System.Serializable]
public class AnimationSetup {

    public AnimationType animationType;
    public string animationTrigger;

}
