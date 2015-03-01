﻿using UnityEngine;
using System.Collections;

public class SelectorController : MonoBehaviour
{
    private RectTransform _rectTransform;
    public GameObject CurrentSelectedPlanet;

    public void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void SelectPlanet(GameObject target)
    {
        if (GameManager.Instance.Empire.CurrentPlanet == null)
            GameManager.Instance.Empire.CurrentPlanet = target;

        _rectTransform.sizeDelta = GameManager.Instance.Empire.CurrentPlanet == target ? new Vector2(72f,72f) : new Vector2(40f, 40f);
        transform.position = target.transform.position;

        GameManager.Instance.ViewController.SelectPlanet(target);
        CurrentSelectedPlanet = target;
    }

    public void RefreshDisplay()
    {
        SelectPlanet(CurrentSelectedPlanet);
    }

    public void SendSpy()
    {
        var controller = CurrentSelectedPlanet.GetComponent<PlanetController>();
        controller.SpySats = true;
        GameManager.Instance.Empire.Fuel -= GameManager.Instance.Empire.FuelToSpy(CurrentSelectedPlanet);
        RefreshDisplay();
    }

    public void FlyTo()
    {
        GameManager.Instance.Empire.MoveToPlanet(CurrentSelectedPlanet);
        RefreshDisplay();
    }

    public void Transfer()
    {
        GameManager.Instance.TransferController.SetActive(true);
        GameManager.Instance.TransferController.GetComponent<TransferController>().StartTransfer(CurrentSelectedPlanet);
    }
}