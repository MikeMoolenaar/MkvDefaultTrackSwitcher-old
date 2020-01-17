using System.Diagnostics;

namespace MKVdefaultTrackSwitcher
{
    public static class Helper
    {
        public static string ExecuteEXE(string filename, string args)
        {
            string output = "";
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(filename);
            proc.StartInfo.Arguments = args;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();

            string line;
            while ((line = proc.StandardOutput.ReadLine()) != null)
                output += line;

            proc.Close();
            return output;
        }
    }
}
