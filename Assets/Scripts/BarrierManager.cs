using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoSingleton<BarrierManager>
{
    public void DisableForceBarrier(ForceBarrier forceBarrier)
    {
        StartCoroutine(DisableForceBarrierRoutine(forceBarrier));
    }

    IEnumerator DisableForceBarrierRoutine(ForceBarrier forceBarrier)
    {
        forceBarrier.gameObject.SetActive(false);
        yield return new WaitForSeconds(3f);
        forceBarrier.gameObject.SetActive(true);
        forceBarrier.ResetHealth();
    }
}
