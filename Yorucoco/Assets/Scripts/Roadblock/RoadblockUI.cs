using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoadblockUI : MonoBehaviour
{
    public GameObject nodeUI;

    [Header("Behaviour")]
    public Button Close;
    public Button Rotate90CWButton;
    public Button Rotate90ACWButton;
    private Roadblock target;
    Animator anim;

    public static RoadblockUI Instance;
    private void Awake() {
        Instance = this;
    }

    private void Start()
    {
        nodeUI.SetActive(false);
        anim = GetComponentInChildren<Animator>();
    }

    private void Update() {
        //as long as it is in action phase the UI will be kept hidden and disabled
        if(GameManager.Instance.CurrentPhase == GameManager.Phases.Action) {
            Hide();
        }
    }
    
    public void SetTarget(Roadblock _target)
    {
        target = _target;
        transform.position = target.GetPosition();

        nodeUI.SetActive(true);
        anim.SetBool("FadeOut", true);
    }

    public void Hide() //Hide NodeUI
    {
        nodeUI.SetActive(false);
    }

    public void RotateClockWise() {
        target.Rotate90CW();
    }
    
    public void RotateAntiClockWise() {
        target.Rotate90ACW();
    }

}
