using System.Collections.Generic;
using UnityEngine;

public class AnimationSelecter : MonoBehaviour
{
    private List<string> _animations;
    private Animation _animation;

    public List<string> Animations => _animations;

    public void TakeAnimation(List<string> animations, Animation animation)
    {
        _animations = animations;
        _animation = animation;
    }

    public void ChangeAnimation(string name)
    {
        _animation.Play(name);
    }
}
