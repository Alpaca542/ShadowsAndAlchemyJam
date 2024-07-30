using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class vignetteTweener : MonoBehaviour
{
    public Volume volume;
    public int type;
    private void Start()
    {
        Vignette vgn;
        if (volume.profile.TryGet<Vignette>(out vgn))
        {
            vgn.active = true;
            if (type == 1)
            {
                vgn.center = new Vector2Parameter(new Vector2(-0.2f, 0.5f));
                DOTween.To(() => vgn.intensity.value, x => vgn.intensity.value = x, 0f, 1f);
            }
            else if (type == 2)
            {
                vgn.center = new Vector2Parameter(new Vector2(1.2f, 0.5f));
                DOTween.To(() => vgn.intensity.value, x => vgn.intensity.value = x, 1f, 1f);
            }
            else if (type == 3)
            {
                DOTween.To(() => vgn.intensity.value, x => vgn.intensity.value = x, 0.453f, 1f);
            }
        }
    }
}
