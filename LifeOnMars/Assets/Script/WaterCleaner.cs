using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class WaterCleaner : Receiver
{

    const float WATER_GROW_AMOUNT = 0.2f;
    const float WATER_MAX_AMOUNT = 1.0f;

    [SerializeField]
    private Transform dirtyTank,
        cleanTank;
    private float dirtyWaterLevel = 0f;

    [SerializeField] private Material enabledMat;
    [SerializeField] private MeshRenderer screen;

    [SerializeField] string task_to_update;

    bool full = false, cleaned = false;

    [SerializeField] private TextMeshPro screenText;
    const string startCleaningPrompt = "Premi spazio per continuare",
        cleaningPrompt = "Filtraggio in corso...",
        completePrompt = "Filtraggio completato!";

    public override bool CanReceive(Storable s)
    {
        return s.ItemName=="Water" && !full;
    }
    public override void Receive(GameObject g)
    {
        TaskSystem.instance.UpdateTask(task_to_update,1);
        Destroy(g);
        FillTank();
    }

    public void PushButton(SelectEnterEventArgs args)
    {
        if (!full)
        {
            print("Load more water");
        }
        else
        {
            if (!cleaned)
            {
                CleanWater();
                cleaned = !cleaned;
            }
        }
    }

    private void FillTank()
    {
        if (!full)
        {
            // increas the water level by an offset
            dirtyWaterLevel += WATER_GROW_AMOUNT;
            if (dirtyWaterLevel < WATER_MAX_AMOUNT)
            {
                print("Riempimento cisterna: " + dirtyWaterLevel * 100f + "%");
                StartCoroutine(Grow(dirtyWaterLevel, dirtyTank, 0.01f));
            }
            else
            {
                print("Cisterna piena!");
                StartCoroutine(Grow(dirtyWaterLevel, dirtyTank, 0.01f));

                // change material of the screen when the tank is full
                Material[] mats = screen.materials;
                mats[1] = new Material(enabledMat);
                screen.materials = mats;

                // change screentext
                screenText.text = startCleaningPrompt;

                full = !full;
            }
        }
    }

    private void CleanWater()
    {
        // set screen text
        screenText.text = cleaningPrompt;
        // empty the dirty water tank
        StartCoroutine(Shrink(dirtyTank, 0.01f));
        // fill the clean water tank
        StartCoroutine(Grow(WATER_MAX_AMOUNT, cleanTank, 0.01f));
    }

    #region Coroutines
    IEnumerator Grow(float newLevel, Transform target, float speed)
    {
        for (float scale = target.localScale.z; scale <= (Mathf.Min(newLevel, WATER_MAX_AMOUNT)); scale += WATER_GROW_AMOUNT / 10.0f)
        {
            target.localScale = new(1.0f, 1.0f, scale);
            yield return new WaitForSeconds(speed);
        }
        if (target == cleanTank)
            screenText.text = completePrompt;
    }

    IEnumerator Shrink(Transform target, float speed)
    {
        for (float scale = WATER_MAX_AMOUNT; scale >= 0; scale -= WATER_GROW_AMOUNT / 10.0f)
        {
            target.localScale = new(1.0f, 1.0f, scale);
            yield return new WaitForSeconds(speed);
        }
    }
    #endregion
}