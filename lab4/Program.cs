using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks.Dataflow;
using System.Linq;
using System;
using TestGenerator;
using System.Threading.Tasks;

namespace lab4
{
    class Program
    {
        private static string inputFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "resourses\\input");
        private static string outputFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "resourses\\output\\");

        static async Task Main(string[] args)
        {
            Generator testGen = new Generator();

            var downloadFile = new TransformBlock<string, string>(async filepath =>
            {
                return await File.ReadAllTextAsync(filepath);
            });

            var generateTest = new TransformManyBlock<string, OutputFileInfo>(async testsFile =>
            {
                return await Task.Run(() => testGen.GenerateTestClasses(testsFile).ToArray());
            });

            var createOutputFiles = new ActionBlock<OutputFileInfo>(async testFile =>
            {
                await File.WriteAllTextAsync(outputFilesPath + testFile.fileName, testFile.text);
            });

            var linkOptions = new DataflowLinkOptions { PropagateCompletion = true };

            downloadFile.LinkTo(generateTest, linkOptions);
            generateTest.LinkTo(createOutputFiles, linkOptions);

            string[] filePaths = Directory.GetFiles(inputFilesPath);

            foreach (string filePath in filePaths)
            {
                if (filePath.EndsWith(".cs"))
                    downloadFile.Post(filePath);
            }
            downloadFile.Complete();
            createOutputFiles.Completion.Wait();


        }
    }
}
