using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Saveable
{
    public void Save();
    public void Load(string Path);

}
