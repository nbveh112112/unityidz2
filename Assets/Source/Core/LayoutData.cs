using System;
using System.Collections.Generic;

namespace Core
{
  public class LayoutData
  {
    public event Action<LayoutData> OnChange;
    private List<CardInstance> _cardInstances;
    private Layout _layout;
    
    public LayoutData(Layout layout)
    {
      _layout = layout;
      _cardInstances = new List<CardInstance>();
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
        foreach (var cardInstance in _cardInstances)
        {
          cardInstance.Layout = value;
        }
        OnChange?.Invoke(this);
      }
    }

    public List<CardInstance> Data => _cardInstances;
    
    public void AddCard(CardInstance cardInstance)
    {
      _cardInstances.Add(cardInstance);
      cardInstance.Layout = _layout;
      OnChange?.Invoke(this);
    }
    
    public void AddCard(CardInstance cardInstance, int index)
    {
      _cardInstances.Insert(index, cardInstance);
      cardInstance.Layout = _layout;
      OnChange?.Invoke(this);
    }

    public int GetPosition(CardInstance cardInstance)
    {
      return _cardInstances.FindIndex((x) => x == cardInstance);
    }
    
    public void RemoveCard(CardInstance cardInstance)
    {
      _cardInstances.Remove(cardInstance);
      OnChange?.Invoke(this);
    }
    
    public void Shuffle()
    {
      _cardInstances.Shuffle();
      OnChange?.Invoke(this);
    }
    
  }
}

public static class StringExtensions
{
  private static Random rng = new Random();  
  public static void Shuffle<T>(this IList<T> list)  
  {  
    int n = list.Count;  
    while (n > 1) {  
      n--;  
      int k = rng.Next(n + 1);  
      (list[k], list[n]) = (list[n], list[k]);
    }  
  }

}