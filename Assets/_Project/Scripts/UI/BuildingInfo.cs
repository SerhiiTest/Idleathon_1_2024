using System;
using UnityEngine.UIElements;

public class BuildingInfo : VisualElement
{
    public event Action OnClose;

    public new class UxmlFactory : UxmlFactory<BuildingInfo> { }

    private Label _name, _level, _descr;
    private Button _upgrade;
    private BuildingBase _building;
    public BuildingInfo()
    {
        var close = new Button();
        close.text = "X";
        close.clicked += ()=> { style.display = DisplayStyle.None; _building = null; OnClose.Invoke(); };
        close.AddToClassList("closeBtn");

        _level = new Label();
        _descr = new Label();
        _name = new Label();
        _name.text = "Name Naem";
        _descr.text = "About building";
        _level.text = "Level: 1";
        _level.AddToClassList("lvl");
        _descr.AddToClassList("descr");
        _name.AddToClassList("naem");

        _upgrade = new Button();
        _upgrade.text = "X";
        _upgrade.clicked += () => { 
            if (GameManager.Instance.GetMoney(_building.PriceToUpgrade)) { _building.Upgrade(); }
            style.display = DisplayStyle.None; _building = null; OnClose.Invoke();
        };
        _upgrade.AddToClassList("up");


        hierarchy.Add(close);
        hierarchy.Add(_name);
        hierarchy.Add(_level);
        hierarchy.Add(_descr);
        hierarchy.Add(_upgrade);
    }

    internal void Set(BuildingBase comp)
    {
        style.display = DisplayStyle.Flex;
        _name.text = comp.BName;
        _descr.text = comp.Description;
        _level.text = "Level: " + (comp.Level+1).ToString();
        _upgrade.SetEnabled(false);
        if (comp is Ruin) {
            Ruin r = (Ruin)comp;
            if (r.IsMaxed)
            {
                _upgrade.text = "MAX LEVEL";
            }
            else
            {
                _upgrade.text = $"{r.Sand}/{r.PriceToUpgrade} Sand";
            }
        }
        else         
        {
            _building = comp;
            if (comp.IsMaxed)
            {
                _upgrade.text = "MAX LEVEL";
            }
            else
            {
                _upgrade.SetEnabled(true);
                _upgrade.text = $"{comp.PriceToUpgrade}$";
            }
        }

    }
}