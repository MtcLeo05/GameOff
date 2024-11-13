using System;
using UnityEngine;

public abstract class CropBase : MonoBehaviour
{
    public CropType cropType;
    public Renderer render;
    public Material outline;
    public DaySystem daySystem;
    public CropDrop[] drops;

    public Vector3 offset;
    
    public int maxStage;
    public int stage;
    public float[] timeForStage;

    [SerializeField] protected float lifetime;

    protected CropBase(CropType cropType)
    {
        this.cropType = cropType;
    }
    
    protected virtual void Start()
    {
        daySystem = GameObject.Find("Managers/DayManager").GetComponent<DaySystem>();

        if (timeForStage.Length != maxStage)
        {
            throw new ArgumentException("Max stage number is " + maxStage);
        }
        
        render = GetComponent<Renderer>();
        Material[] backup = new Material[render.materials.Length + 1];

        render.materials.CopyTo(backup, 0);
        backup[render.materials.Length] = outline;

        render.materials = backup;
    }
    
    protected virtual void Update()
    {
        if(!daySystem) return;
        if(!daySystem.isDay()) return;
        if(grown()) return;
        
        lifetime += Time.deltaTime;
        if (lifetime >= timeForStage[stage])
        {
            stage++;
            lifetime = 0f;
            grow();
        }
    }

    public abstract void grow();
    
    public virtual bool grown()
    {
        return stage >= maxStage;
    }

    public abstract void harvest(GameObject player);
    
    public virtual void loadData(CropData cData, Registry registry)
    {
        stage = cData.stage;
        lifetime = cData.lifetime;
    }


    public virtual CropData saveData()
    {
        return new CropData(
            cropType,
            stage,
            lifetime
        );
    }
}
