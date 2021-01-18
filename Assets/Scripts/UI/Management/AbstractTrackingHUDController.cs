using System;
using UnityEngine;
using UnityEngine.UIElements;

/**
 * Automatically tracks a game object with a UI element. The element's bottom-right or bottom-left corner will be
 * locked to the object's position, depending on where the player is. 
 */
public abstract class AbstractTrackingHUDController : AbstractHUDController
{
    [Tooltip("The object the #track_element should track to")]
    public GameObject TrackTarget;

    private GameObject _player;
    private Camera _camera;
    private VisualElement _trackElement;

    protected override void Start()
    {
        base.Start();
        _player = GameObject.FindWithTag("Player");
        _camera = Camera.main;
        _trackElement = Root.Q("track_element");
        _trackElement.style.position = new StyleEnum<Position>(Position.Absolute);
    }

    private void Update()
    {
        if (Visible && _camera && TrackTarget && _trackElement != null)
        {
            var targetPosition = WorldToUIPoint(TrackTarget.transform.position);
            var rightSide = false;
            if (_player)
            {
                var playerPosition = WorldToUIPoint(_player.transform.position);
                rightSide = playerPosition.x <= targetPosition.x;
            }

            if (rightSide)
            {
                _trackElement.style.right = new StyleLength(StyleKeyword.Auto);
                _trackElement.style.left = new StyleLength(targetPosition.x);
            }
            else
            {
                _trackElement.style.right = new StyleLength(1920 - targetPosition.x);
                _trackElement.style.left = new StyleLength(StyleKeyword.Auto);
            }

            _trackElement.style.bottom = new StyleLength(1080 - targetPosition.y);
        }
    }

    protected Vector2 WorldToUIPoint(Vector3 worldPoint)
    {
        var viewportPoint = _camera.WorldToViewportPoint(worldPoint);
        return new Vector2(viewportPoint.x * 1920, (1 - viewportPoint.y) * 1080);
    }
}