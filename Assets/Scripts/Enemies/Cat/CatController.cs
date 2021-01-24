using UnityEngine;

public class CatController : MonoBehaviour
{
    public float Speed;
    public bool FaceRight;
    public AnimationState Animation;
    [SerializeField]
    private Animator _animator;

    public GameObject RunTarget;
    public GameObject LookTarget;

    private static readonly int State = Animator.StringToHash("state");

    private AnimationState? _animationOverride = null;

    // Update is called once per frame
    void Update()
    {
        UpdateRunning();

        var s = transform.localScale;
        s.x = FaceRight ? -1 : 1;
        transform.localScale = s;

        _animator.SetInteger(State, (int) (_animationOverride ?? Animation));
    }

    private void UpdateRunning()
    {
        var pos = transform.position;
        var targetX = RunTarget ? RunTarget.transform.position.x : pos.x;

        if (Mathf.Approximately(pos.x, targetX)) // no target or already at the target
        {
            _animationOverride = null;
            if (LookTarget)
            {
                FaceRight = LookTarget.transform.position.x > pos.x;
            }
            return;
        }
        
        FaceRight = targetX > pos.x;

        var frameSpeed = Speed * Time.unscaledDeltaTime;
        pos.x = targetX > pos.x
            ? Mathf.Min(targetX, pos.x + frameSpeed)
            : Mathf.Max(targetX, pos.x - frameSpeed);
        transform.position = pos;
        _animationOverride = AnimationState.Running;
    }

    public enum AnimationState
    {
        Sitting = 0,
        Sleeping = 1,
        Playing = 2,
        Running = 3,
        Licking = 4,
    }
}