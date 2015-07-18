using System;
using System.IO;

static class AppendFile
{
    private static long pc;
    static void Main()
    {
        var file1 = Cir("File 1:");
        var file2 = Cir("File 2:");
        var file = Cir("File Output:");
        try
        {
            var stream1 = File.OpenRead(file1);
            var stream2 = File.OpenRead(file2);
            var stream = File.Create(file);
            try
            {
                Cp(stream1, stream);
                Cp(stream2, stream);
            }
            finally
            {
                stream1.Close();
                stream2.Close();
                stream.Close();
            }
            Cir("文件合并完成。新文件长度：" + pc.ToString("###,###") + " bytes");
        }
        catch (Exception err)
        {
            Cir(err.Message);
        }
    }
    static string Cir(string msg)
    {
        if (!string.IsNullOrEmpty(msg))
            Console.Write(msg);
        return Console.ReadLine();
    }
    private static void Cp(Stream stream1, Stream streamObject)
    {
        var buff = new byte[0x1000000];
        int len;
        do
        {
            len = stream1.Read(buff, 0, buff.Length);
            pc += len;
            if (len > 0)
                streamObject.Write(buff, 0, len);
        } while (len > 0);
    }
}