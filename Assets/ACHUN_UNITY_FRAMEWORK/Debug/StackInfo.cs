using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class StackInfo
{
    public StackInfo()
    {
        
    }

    public string Test()
    {
        string info = null;
        StackTrace st = new StackTrace();
        StackFrame[] sf = st.GetFrames();
        int length = sf.Length;
        for (int i = 0; i < length; i++)
        {
            info = info + "\r\n" + "FileName: " + sf[i].GetFileName() + " fullName: " + sf[i].GetMethod().DeclaringType.FullName + " function: " + sf[i].GetMethod().Name + " FileLineNumber: " + sf[i].GetFileColumnNumber();
        }
        //info = new StackTrace().ToString();
        return info;
    }
}
