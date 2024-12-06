using System;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace CustomBuildTasks
{
    public class LineCounter : Task
    {
        [Required]
        public ITaskItem[] SourceFiles { get; set; }

        [Output]
        public int TotalLines { get; set; }

        public override bool Execute()
        {
            try
            {
                TotalLines = SourceFiles
                    .Select(file => File.ReadAllLines(file.ItemSpec).Length)
                    .Sum();

                Log.LogMessage(MessageImportance.High,
                    $"Total lines of code: {TotalLines}");

                return true;
            }
            catch (Exception ex)
            {
                Log.LogError($"Error counting lines: {ex.Message}");
                return false;
            }
        }
    }
}