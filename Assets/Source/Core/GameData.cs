using System.Collections.Generic;

namespace Core
{
  public class GameData
  {
    private Dictionary<Layout, LayoutData> _cardLayouts;
    
    public GameData()
    {
      _cardLayouts = new Dictionary<Layout, LayoutData>();
      _cardLayouts.Add(Layout.Player1, new LayoutData(Layout.Player1));
      _cardLayouts.Add(Layout.Player2, new LayoutData(Layout.Player2));
      _cardLayouts.Add(Layout.Deck, new LayoutData(Layout.Deck));
      _cardLayouts.Add(Layout.Reset, new LayoutData(Layout.Reset));
      _cardLayouts.Add(Layout.Table, new LayoutData(Layout.Table));
    }
    
    public LayoutData this[Layout index] => _cardLayouts[index];
  }
}