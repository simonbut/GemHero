using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreloadAssetManager : MonoBehaviour
{
    #region instance
    private static PreloadAssetManager m_instance;

    public static PreloadAssetManager Instance
    {
        get
        {
            return m_instance;
        }
    }

    void Awake()
    {
        m_instance = this;
    }
    #endregion

    public GameObject turretDemoGameObject;

    public List<GameObject> preloadGameObject = new List<GameObject>();

    public List<GameObject> trackGameObject = new List<GameObject>();
    public List<GameObject> tankBodyGameObject = new List<GameObject>();
    //public List<Mesh> barrelMesh = new List<Mesh>();
    //public List<Material> barrelMaterial = new List<Material>();
    //public List<Mesh> cannonMesh = new List<Mesh>();
    //public List<Material> cannonMaterial = new List<Material>();
    public List<GameObject> turretGameObject = new List<GameObject>();
    public List<GameObject> barrelGameObject = new List<GameObject>();

    public Mesh turretSampleMesh;
    public Material turretSampleMaterial;
    public Mesh barrelSampleMesh;
    public Material barrelSampleMaterial;

    //public List<Sprite> rareIcon = new List<Sprite>();

    //public List<GameObject> turretPieceGameObject = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
