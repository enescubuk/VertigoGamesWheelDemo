using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningState : IStateCommand
{
    [SerializeField] private RectTransform WheelTransform;
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private float maxSpinSpeed = 1000f;
    [SerializeField] private int numberOfSlices = 8;
    private bool isSpinning = false;
    private float targetAngle;

    public override void Enter()
    {
        Debug.Log("SpinningState");
        StartCoroutine(SpinCoroutine());
    }

    public override void Tick()
    {
        // Döngüsel işler varsa buraya eklenebilir
    }

    public override void Exit()
    {
        Debug.Log("Spinning Finished");
    }

    private IEnumerator SpinCoroutine()
    {
        isSpinning = true;
        float elapsedTime = 0f;
        float currentAngle = 0f;
        targetAngle = Random.Range(360, 360 * 5); // Çark 5 kez dönecek ve rastgele bir yerde duracak.

        while (elapsedTime < spinDuration)
        {
            float speed = Mathf.Lerp(maxSpinSpeed, 0, elapsedTime / spinDuration);
            float deltaAngle = speed * Time.deltaTime;
            currentAngle += deltaAngle;
            WheelTransform.localRotation = Quaternion.Euler(0, 0, -currentAngle);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        float _targetAngle = Mathf.RoundToInt(WheelTransform.rotation.z / 45f) * 45;
        WheelTransform.rotation = Quaternion.Euler(0, 0, +_targetAngle);
        isSpinning = false;
    }

    private void DetermineSlice(float finalAngle)
    {
        int sliceIndex = Mathf.FloorToInt(finalAngle / (360f / numberOfSlices)); // Direkt hangi dilimde durduğunu hesapla
        Debug.Log("Landed on Slice: " + sliceIndex);
    }
}
