using UnityEngine;

public static class MaterialFinder
{

    public const string OutlineMaterialPath = "ArtWork/Material/OutLine";



    public static Material GetOutlineResourceMaterial()
    {
        return Resources.Load<Material>(OutlineMaterialPath);
    }
}
