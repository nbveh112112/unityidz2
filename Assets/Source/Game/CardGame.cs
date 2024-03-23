using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
namespace Game
{
  public class CardGame : MonoBehaviour
  {
    private Dictionary<CardInstance, CardView> _cardViews = new();
    private Dictionary<Layout, CardLayout> _cardLayouts = new();
    private GameData _gameData;
    [SerializeField]
    private GameObject _cardPrefab;

    private void Start()
    {
      _gameData = new GameData();
      Initialize();
    }

    private void Initialize()
    {
      LoadLayouts();
      LayoutCollidersEnable(false);
      LoadCards();
      _gameData[Layout.Deck].Shuffle();
      DealCard(1);
      DealCard(2);
    }

    private void LoadLayouts()
    {
      foreach (var layout in GetComponentsInChildren<CardLayout>())
      {
        layout.Initialize(_gameData[layout.Layout], this);
        _cardLayouts.Add(layout.Layout, layout);
      }
    }
    private void LoadCards()
    {
      CardAsset[] assets = Resources.LoadAll<CardAsset>(path: "");
      foreach (var asset in assets)
      {
        CreateCard(asset);
      }
    }
    
    private void CreateCard(CardAsset cardAsset)
    {
      CardInstance cardInstance = new CardInstance(cardAsset, _gameData);
      CreateCardViev(cardInstance);
      _gameData[Layout.Deck].AddCard(cardInstance);
    }
    
    private void CreateCardViev(CardInstance cardInstance)
    {
      GameObject go = Instantiate(_cardPrefab);
      CardView cardView = go.GetComponent<CardView>();
      cardView.Initialize(cardInstance, this);
      _cardViews.Add(cardInstance, cardView);
      
    }
    
    public CardView GetCard(CardInstance cardInstance)
    {
      return _cardViews[cardInstance];
    }

    public void DealCard(int player)
    {
      switch (player)
      {
        case 1:
          _gameData[Layout.Deck].Data[^1].MoveToLayout(Layout.Player1);
          break;
        case 2:
          _gameData[Layout.Deck].Data[^1].MoveToLayout(Layout.Player2);
          break;
      }
    }
    public void LayoutCollidersEnable(bool enable)
    {
      foreach (var layout in _cardLayouts)
      {
        layout.Value.ColliderEnable(enable);
      }
    }

    public void CardCollidersEnable(bool enable)
    {
      foreach (var cardView in _cardViews)
      {
        cardView.Value.CollierEnable(enable);
      }
    }
  }
}