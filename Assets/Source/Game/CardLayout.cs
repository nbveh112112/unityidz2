using Core;
using UnityEngine;

namespace Game
{
  public class CardLayout : MonoBehaviour
  {
    [SerializeField]
    protected Layout _layout;
    [SerializeField]
    protected bool _faceUp;
    [SerializeField]
    protected Transform _transform;
    [SerializeField]
    protected Vector2 _field;
    [SerializeField] 
    protected Collider2D _collider;

    protected LayoutData _layoutData;
    protected CardGame _cardGame;

    public Layout Layout => _layout;

    public void Initialize(LayoutData layoutData, CardGame cardGame)
    {
      _cardGame = cardGame;
      _layoutData = layoutData;
      layoutData.OnChange += ReDraw;
    }

    public void ColliderEnable(bool enable)
    {
      _collider.enabled = enable;
    }

    protected virtual void ReDraw(LayoutData layoutData)
    {
      int cards = _layoutData.Data.Count;
      var position = _transform.position;
      Vector3 now = new Vector3(position.x - _field.x/2, position.y - _field.y/2, position.x);
      Vector3 step = new Vector3(_field.x / (cards + 1), _field.y / (cards + 1), 0);
      int layer = cards;
      for (int i = 0; i < cards; ++i)
      {
        now += step;
        CardView card = _cardGame.GetCard(_layoutData.Data[i]);
        card.Transform(now);
        card.Rotate(_faceUp);
        card.SetSortingOrder(layer--);
      }
    }
  }
}