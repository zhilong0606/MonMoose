using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public abstract class StaticDataLoader
{
    private string m_fileName;
    private string m_filePath;
    private string m_md5;

    public string fileName
    {
        get { return m_fileName; }
    }

    public string filePath
    {
        get { return m_filePath; }
    }

    public void Load(string fileName, string filePath, byte[] buffer)
    {
        m_fileName = fileName;
        m_filePath = filePath;
        OnLoad(buffer);
    }

    protected abstract void OnLoad(byte[] buffer);
}
