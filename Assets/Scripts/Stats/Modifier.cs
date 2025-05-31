using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public float value;
    public string source; // 标识来源，如"装备","技能"

    public Modifier(float _value, string _source)
    {
        this.value = _value;
        this.source = _source;
    }
}
