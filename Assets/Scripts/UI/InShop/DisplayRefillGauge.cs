using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class DisplayRefillGauge
{
    public static IEnumerator DecreaseRefillGaugeFor(Image gaugeImage, float duration)
    {
        float normalizedTime = 1f;

        while (normalizedTime >= 0f)
        {
            gaugeImage.fillAmount = normalizedTime;
            normalizedTime -= Time.deltaTime / duration;
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        gaugeImage.fillAmount = 1;
    }
}
