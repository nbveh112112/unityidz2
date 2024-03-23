using System;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

namespace Core
{
  [CreateAssetMenu(fileName = "CardInfo", menuName = "GameObjects/CardInfo")]
  public class CardAsset : ScriptableObject
  {
    [SerializeField]
    private string _cardName;
    [SerializeField]
    private Sprite _cardImage;

    public string Name => _cardName;

    public Sprite Sprite
    {
      get
      {
        return _cardImage;
      }
    }
  }
}
