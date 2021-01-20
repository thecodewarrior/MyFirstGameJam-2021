using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HeartHUDController : AbstractHUDController
{
    protected override string TemplateName => "heart_hud";

    public Sprite FullHeart;
    public Sprite EmptyHeart;

    private List<VisualElement> _hearts = new List<VisualElement>();
    private VisualElement _heartContainer;

    protected override void Start()
    {
        base.Start();
        Manager.ShowController(this);
    }

    public override void OnShow()
    {
        _heartContainer = Root.Q("heart_container");
        for (var i = 0; i < GlobalPlayerData.MaxHealth; i++)
        {
            var heart = new VisualElement();
            heart.AddToClassList("heart");
            _hearts.Add(heart);
            _heartContainer.Add(heart);
        }
    }

    private void Update()
    {
        for (var i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].style.backgroundImage = new StyleBackground(
                i < GlobalPlayerData.Health ? FullHeart : EmptyHeart
            );
        }
    }
}