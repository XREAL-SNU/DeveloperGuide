/*
Path: Assets/Scripts/SingleUse/CustomDataTypeExample
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCustomSerialization {
    // Transfer Data as ByteArray or Class Type
    public int MyNumber;
    public string MyString;

    public static byte[] Serialize(object obj) {
        // Cast to Custom Type
        MyCustomSerialization data = (MyCustomSerialization)obj;
        
        // MyNumber
        byte[] myNumberBytes = BitConverter.GetBytes(data.MyNumber);
        if(BitConverter.IsLittleEndian) Array.Reverse(myNumberBytes);

        // MyString
        byte[] myStringBytes = Encoding.ASCII.GetBytes(data.MyString);
        if(BitConverter.IsLittleEndian) Array.Reverse(myStringBytes);

        return joinBytes(myNumberBytes, myStringBytes);
    }

    public static object Deserialize(byte[] bytes) {
        MyCustomSerialization data = new MyCustomSerialization();

        // MyNumber
        byte[] myNumberBytes = new byte[4];
        Array.Copy(bytes, 0, myNumberBytes, 0, myNumberBytes.Length);
        if(BitConverter.IsLittleEndian) Array.Reverse(myNumberBytes);
        data.MyNumber = BitConverter.ToInt32(myNumberBytes, 0);

        // MyString
        byte[] myStringBytes = new byte[bytes.Length - 4];
        if(myNumberBytes.Length > 0) {
            Array.Copy(bytes, 4, myStringBytes, 0, myStringBytes.Length);
            data.MyString = Encoding.UTF8.GetString(myStringBytes);
        } else {
            data.MyString = string.Empty;
        }
        return data;
    }

    private static byte[] joinBytes(params byte[][] arrays) {
        byte[] rv = new byte[arrays.Sum(a => a.Length)];
        int offset = 0;

        foreach(byte[] array in arrays) {
            System.Buffer.BlockCopy(array, 0, rv, offset, array.Length);
            offset += array.Length;
        }
        return rv;
    }
}
