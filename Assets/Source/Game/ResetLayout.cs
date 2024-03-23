
using Core;
using UnityEngine;
using Random = System.Random;

namespace Game
{
  public class ResetLayout : CardLayout
  {
    protected override void ReDraw(LayoutData layoutData)
    {
      Random rng = new Random(1);
      int cards = _layoutData.Data.Count;
      var position = _transform.position;
      Vector3 now = new Vector3(position.x - _field.x/2, position.y - _field.y/2, position.x);
      Vector3 step = new Vector3(_field.x / (cards + 1), _field.y / (cards + 1), 0);
      for (int i = 0; i < cards; ++i)
      {
        now += step;
        CardView card = _cardGame.GetCard(_layoutData.Data[i]);
        card.Transform(now, rng.Next(360));
        card.Rotate(_faceUp);
        card.SetSortingOrder(i);
      }
    }
  }
}