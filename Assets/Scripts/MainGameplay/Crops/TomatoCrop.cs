
using UnityEngine;

public class TomatoCrop: CropBase, IPlayerInteractable
{
    private static readonly int outlineColor = Shader.PropertyToID("_OutlineColor");
    private static readonly int outlineThickness = Shader.PropertyToID("_OutlineThickness");
    public Color[] colors;

    public TomatoCrop() : base(CropType.Tomato) {}
    
    protected override void Start()
    {
        base.Start();
        if(render) render.materials[2].color = colors[0];
        render.materials[3].SetFloat(outlineThickness, 0.01f);
    }

    public override void grow()
    {
        if(!render) return;
        render.materials[2].color = colors[stage];
    }

    protected override void Update()
    {
        base.Update();
        if (!render)
        {
            render = GetComponent<Renderer>();
        }
        render.materials[2].color = colors[stage];
    }

    public override void harvest(GameObject player)
    {
        PlayerInventoryManager playerInventory = player.GetComponent<PlayerInventoryManager>();
        if (!playerInventory)
        {
            foreach (CropDrop drop in drops)
            {
                var random = Random.Range(0f, 1f);
                if (!(random <= drop.chance)) continue;
                
                for (var i = 0; i < drop.count; i++)
                {
                    Instantiate(drop.item.droppedItem, player.transform.position, Quaternion.identity);
                }
            }
            return;
        }
        
        foreach (CropDrop drop in drops)
        {
            var random = Random.Range(0f, 1f);

            if (!(random <= drop.chance)) continue;
            for (var i = 0; i < drop.count; i++)
            {
                if (!playerInventory.addItem(drop.item))
                {
                    Instantiate(drop.item.droppedItem, player.transform.position, Quaternion.identity);
                }
            }
        }

        lifetime = 0;
        stage = 0;
        if (!render)
        {
            render = GetComponent<Renderer>();
        }
        render.materials[2].color = colors[stage];
    }

    public void highlight(bool isHovered)
    {
        if(!render) return;
        render.materials[3].SetColor(outlineColor, isHovered ? (grown() ? Color.white: Color.black): Color.clear);
    }

    public void activate(GameObject player)
    {
        if(!grown()) return;
        harvest(player);
    }

    public bool state()
    {
        return grown();
    }
}