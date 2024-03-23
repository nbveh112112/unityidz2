using System;
using Core;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using Exception = System.Exception;

namespace Game
{
  public class CardView : MonoBehaviour
  {
    [SerializeField]
    private SpriteRenderer _spriteRenderer;
    [SerializeField] 
    private SpriteRenderer _backSpriteRenderer;
    [SerializeField]
    private Transform _transform;
    [SerializeField]
    private Collider2D _collider;
    private CardInstance _cardInstance;
    private CardGame _cardGame;

    

    public void Initialize(CardInstance cardInstance, CardGame cardGame)
    {
      _cardInstance = cardInstance;
      _cardGame = cardGame;
      _spriteRenderer.sprite = _cardInstance.CardAsset.Sprite;
    }

    public CardInstance CardInstance
    {
      get => _cardInstance;
      set => _cardInstance = value;
    }

    public void Transform(Vector3 position, float rotation = 0)
    {
      _transform.position = position;
      _transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    public void SetSortingOrder(int order)
    {
      _spriteRenderer.sortingOrder = order;
      _backSpriteRenderer.sortingOrder = order;
      _collider.layerOverridePriority = order;
    }
    public void Rotate(bool up)
    {
      _spriteRenderer.enabled = up;
      _backSpriteRenderer.enabled = !up;
    }

    public void CollierEnable(bool enable)
    {
      _collider.enabled = enable;
    }

    private Vector3 _position;
    private bool _isHeld;
    private float _downTime;
    public void OnMouseDown()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _spriteRenderer.sortingOrder = 100;
        _backSpriteRenderer.sortingOrder = 100;
        _downTime = Time.time;
        Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        _position = mousePos;
        _cardInstance.RemoveFromLayout();
        _cardGame.CardCollidersEnable(false);
        _cardGame.LayoutCollidersEnable(true);
        _isHeld = true;
      }
    }
    public void OnMouseDrag()
    {
      if (_isHeld)
      {
        Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        _transform.position += (mousePos - _position);
        _position = mousePos;
        
      }
    }

    public void OnMouseUp()
    {
      if (_isHeld)
      {
        _cardInstance.ReturnCard();
        if (Time.time - _downTime < 0.1)
        {
          switch (_cardInstance.Layout)
          {
            case Layout.Player1:
            case Layout.Player2:
              _cardInstance.MoveToLayout(Layout.Table);
              break;
            case Layout.Table:
              _cardInstance.MoveToLayout(Layout.Reset);
              break;
            case Layout.Deck:
              _cardGame.DealCard(1);
              _cardGame.DealCard(2);
              break;
          }
        }
        else
        {
          Vector3 mousePos = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
          RaycastHit2D hit = Physics2D.Raycast(new Vector2(mousePos.x, mousePos.y), new Vector2(mousePos.x, mousePos.y));
          if (hit.collider != null)
          {
            CardLayout cl = hit.collider.GetComponentInParent<CardLayout>();
            if (cl != null)
            {
              _cardInstance.MoveToLayout(cl.Layout);
            }
          }
        }
        _cardGame.CardCollidersEnable(true);
        _cardGame.LayoutCollidersEnable(false);
        _isHeld = false;
      }
    }
  }
}