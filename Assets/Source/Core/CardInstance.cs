using UnityEngine;


namespace Core
{
  public class CardInstance
  {
    private GameData _gameData;
    private Layout _layout;
    private int _lastPosition;
    public CardInstance(CardAsset cardAsset, GameData gameData)
    {
      CardAsset = cardAsset;
      _gameData = gameData;
    }

    public Layout Layout
    {
      get
      {
        return _layout;
      }
      set
      {
        _layout = value;
      }
    }

    public void MoveToLayout(Layout layout)
    {
      _gameData[_layout].RemoveCard(this);
      _gameData[layout].AddCard(this);
    }

    public void RemoveFromLayout()
    {
      _lastPosition = _gameData[_layout].GetPosition(this);
      _gameData[_layout].RemoveCard(this);
    }

    public void ReturnCard()
    {
      _gameData[_layout].AddCard(this, _lastPosition);
    }
    
    public CardAsset CardAsset { get; }
  }
}