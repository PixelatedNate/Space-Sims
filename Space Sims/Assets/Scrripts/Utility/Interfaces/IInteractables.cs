using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractables {

    void OnSelect();

    void OnDeselect();
    //void OnDoubleClick();

    bool OnHold();

    void OnHoldRelease();


 }
