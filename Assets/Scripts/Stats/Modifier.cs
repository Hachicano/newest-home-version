using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier
{
    public float value;
    public string source; // ��ʶ��Դ����"װ��","����"

    public Modifier(float _value, string _source)
    {
        this.value = _value;
        this.source = _source;
    }
}
