using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class Binary
{
#region Reading and Writing to File
    static void WriteToFile(object obj, string path, string fileName) {
        byte[] objInBinary = ObjectToBinary(obj);

        //If path does not exist then creates the path by creating folders needed
        if(!Directory.Exists(Path.GetDirectoryName(path)))
            Directory.CreateDirectory(Path.GetDirectoryName(path));

        //Creating or overding file with required data
        using(BinaryWriter writer = new BinaryWriter(File.Open(path + fileName, FileMode.Create)))
            writer.Write(objInBinary);
    }

    static object ReadFromFile(string path, string fileName) {
        //Check if the file existes
        if(File.Exists(path + fileName))
        {
            object obj = BinaryToObject(File.ReadAllBytes(path + fileName));
            return obj;
        }
        else
        {
            Debug.Log("<color=red> THE FILE YOU ARE TRYING TO READ DOES NOT EXIST </color>");
            return null;
        }
    }
#endregion

#region Serelizer (Converting things to Binary or back to Object)
//Function that converts any object in c# into binary
    public static byte[] ObjectToBinary(object obj) {
        BinaryFormatter binaryFormat = new BinaryFormatter();      //Binary formatter used to convert to binary

        using(var memoryStream = new MemoryStream())
        {
            binaryFormat.Serialize(memoryStream, obj);         //Converts to binary
            return memoryStream.ToArray();                    //Making into array of bytes
        }
    }

    //Function that converts any byte array to the object it is
    public static object BinaryToObject(byte[] byteArray) {
        BinaryFormatter binaryFormatter = new BinaryFormatter();     //Binary formatter used to convert back to object

        using(var memoryStream = new MemoryStream())
        {
            memoryStream.Write(byteArray, 0, byteArray.Length);       //Writeing the thing that needes to be converted onto the memory stream
            memoryStream.Seek(0, SeekOrigin.Begin);                   //Sets the position form where the stream needs to be read from
            object obj = binaryFormatter.Deserialize(memoryStream);   //Converting byte array on the memory stream into an object
            return obj;
        }
    }
#endregion

}
