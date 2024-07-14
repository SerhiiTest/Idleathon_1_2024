using UnityEngine;
using UnityEngine.UIElements;

public class PlayerHandler : MonoBehaviour
{
    public LayerMask mask;

    public VisualElement Root { get; private set; }

    private bool _canSelect = true;

    private void Awake()
    {
        Root = GetComponent<UIDocument>().rootVisualElement;
        Root.Q<BuildingInfo>("Info").OnClose += () => _canSelect = true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _canSelect)
        {
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100f,mask)){

                if(hit.transform.TryGetComponent<BuildingBase>(out var Comp)){
                    _canSelect = false;
                    ShowInfoFor(Comp);

                }
            }
        }
    }

    private void ShowInfoFor(BuildingBase comp)
    {
        Root.Q<BuildingInfo>("Info").Set(comp);
    }

    public void UpdateResources(int money, int sand)
    {
        Root.Q<Label>("Money").text = money.ToString();
        Root.Q<Label>("Sand").text = sand.ToString();
    }

    internal void UpdateStats(TouristManagerStats t, WorkerManagerStats w)
    {
        Root.Q<Label>("WS").text = w.Speed.ToString(); ;
        Root.Q<Label>("WCW").text = w.CarryWeight.ToString(); 
        Root.Q<Label>("WMQ").text = w.MaxAmount.ToString(); 

        Root.Q<Label>("TS").text = t.Speed.ToString(); 
        Root.Q<Label>("TCW").text = t.CarryWeight.ToString(); 
        Root.Q<Label>("TMQ").text = t.MaxAmount.ToString(); 
        Root.Q<Label>("TC").text = t.Comfort.ToString(); 
    }
}
