using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace versioncal
{
    class Program
    {
        static void Main(string[] args)
        {
            var versionFileTMP = @"C:\Work\Projects\OpenPublishing.Build\Shared\version.tmp";
            var versionFile = @"C:\Work\Projects\OpenPublishing.Build\Shared\version";
            var packageBaseVersionFile = @"C:\Work\Projects\OpenPublishing.Build\Shared\package_version";

            var gitVersion = System.IO.File.ReadAllText(versionFileTMP).Trim().Split('-');
            string[] version = new string[4];
            version[3] = "0";
            version[2] = gitVersion.Length > 1 ? gitVersion[1] : "0";
            version[1] = gitVersion[0].Split('.')[1];
            version[0] = gitVersion[0].Split('.')[0].Substring(1);
            string updatedVersion = string.Join(".", version);
            string PackageVersion;
            string BuildVersion = updatedVersion;
            if (gitVersion.Length > 2) BuildVersion += '.' + gitVersion[2];
            System.IO.File.WriteAllText(versionFile, updatedVersion);

            if (System.IO.File.Exists(packageBaseVersionFile))
            {
                PackageVersion = System.IO.File.ReadAllText(packageBaseVersionFile).Trim();
            }
            else
            {
                PackageVersion = gitVersion[0].Substring(1);
            }

            PackageVersion = string.Join("-", PackageVersion, "alpha", version[2].PadLeft(3, '0'), gitVersion.Length > 2 ? gitVersion[2] : "0");
            Console.ReadLine();
        }
    }
}
