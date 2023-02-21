using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSection
{
    
    public string Text;

    public Action EndAction;
    public Action startAction;
    
    DialogSection nextSection;


    public void DialogSectionStart()
    {
        startAction?.Invoke();
    }

    public void DialogSectionEnd()
    {
        EndAction?.Invoke();
    }





}
