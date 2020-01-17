using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MirrorCam_DarkMirrorPost : MonoBehaviour 
{

    public Material effect;

    void OnRenderImage(RenderTexture source, RenderTexture destination) 
	{
        Graphics.Blit(source, destination, effect);
    }
}
