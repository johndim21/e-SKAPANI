using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkEyes : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private SkinnedMeshRenderer eyelashesSkinnedMeshRenderer;
    [SerializeField] private int eyesIndex;
    [SerializeField] private int eyelashesIndex;

    private void Awake()
    {
        StartCoroutine(Blink());
    }

    public IEnumerator Blink()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(eyesIndex, 50f);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(eyelashesIndex, 50f);
        yield return new WaitForSeconds(0.04f);
        skinnedMeshRenderer.SetBlendShapeWeight(eyesIndex, 100f);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(eyelashesIndex, 100f);
        yield return new WaitForSeconds(0.04f);
        skinnedMeshRenderer.SetBlendShapeWeight(eyesIndex, 50f);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(eyelashesIndex, 50f);
        yield return new WaitForSeconds(0.04f);
        skinnedMeshRenderer.SetBlendShapeWeight(eyesIndex, 0);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(eyelashesIndex, 0);
        yield return new WaitForSeconds(5f);
        StartCoroutine(Blink());
    }

    IEnumerator CloseEyes()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(18, 50f);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(2, 50f);
        yield return new WaitForSeconds(0.01f);
        skinnedMeshRenderer.SetBlendShapeWeight(18, 100f);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(2, 100f);
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(CloseEyes());
    }

    IEnumerator OpenEyes()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(18, 50f);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(2, 50f);
        yield return new WaitForSeconds(0.01f);
        skinnedMeshRenderer.SetBlendShapeWeight(18, 0);
        eyelashesSkinnedMeshRenderer.SetBlendShapeWeight(2, 0);
        yield return new WaitForSeconds(5f);
        StartCoroutine(OpenEyes());
    }
}
