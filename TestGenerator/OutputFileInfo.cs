using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TestGenerator
{
    public class OutputFileInfo
    {
        public string fileName;
        public readonly string text;

        public OutputFileInfo(string fileName, string text)
        {
            this.fileName = fileName;
            this.text = text;
        }
    }
}
