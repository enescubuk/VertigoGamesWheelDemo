using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdleState : IStateCommand
{
    private RectTransform wheelTransform;
    [SerializeField] private Button SpinButton;
    [SerializeField] private GameObject slicePrefab;
    [SerializeField] private List<SliceData> sliceDataList;
    [SerializeField] private SliceData deathData; // DeathData'yı ayrı tanımladık.
    [SerializeField] private int numberOfSlices = 8;
    [SerializeField] private float radius = 235f;

    public override void Enter()
    {
        Debug.Log("IdleState");
        wheelTransform = StateController.Instance.WheelTransform;
        SpinButton.interactable = true;
        GenerateSlices();
    }

    public override void Tick()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpinButtonEvent();
        }
    }

    public override void Exit()
    {
        SpinButton.interactable = false;
    }

    private void GenerateSlices()
{
    float angleStep = 360f / numberOfSlices;
    int deathDataIndex = Random.Range(0, numberOfSlices);

    // sliceDataList'i karıştır
    List<SliceData> shuffledDataList = new List<SliceData>(sliceDataList);
    Shuffle(shuffledDataList);

    for (int i = 0; i < numberOfSlices; i++)
    {
        float angle = i * angleStep * Mathf.Deg2Rad;
        Vector2 position = new Vector2(
            Mathf.Sin(angle) * radius,
            Mathf.Cos(angle) * radius
        );

        GameObject slice = Instantiate(slicePrefab, wheelTransform);
        RectTransform sliceRect = slice.GetComponent<RectTransform>();
        sliceRect.anchoredPosition = position;
        sliceRect.localRotation = Quaternion.Euler(0, 0, -i * angleStep);

        SliceData data = (i == deathDataIndex) ? deathData : shuffledDataList[i % shuffledDataList.Count];
        slice.GetComponent<SliceBehavior>().Initialize(data);
    }
}

// Listeyi karıştıran fonksiyon
private void Shuffle<T>(List<T> list)
{
    for (int i = list.Count - 1; i > 0; i--)
    {
        int randomIndex = Random.Range(0, i + 1);
        T temp = list[i];
        list[i] = list[randomIndex];
        list[randomIndex] = temp;
    }
}


    public void SpinButtonEvent()
    {
        StateController.Instance.ChangeState<SpinningState>();
    }

}
