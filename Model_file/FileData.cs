using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Antheap.Model_file
{
    internal class FileData
    {
        protected List<Component> content = new();

        

        public FileData(string path)
        {
            var fileContent = File.ReadAllLines(path);
            if (fileContent.Length <= 0)
                return;

            //var content = new List<Components>();
            long segment = 1;
            for (long item = 0; item < fileContent.Length; item++)
            {
                //if(segment)
                if (segment.ToString() == fileContent[item])
                {
                    //MessageBox.Show(segment.ToString());
                    content.Add(new Component
                    {
                        Count = segment,
                        StartTime = fileContent[item + 1].Substring(0, fileContent[item + 1].LastIndexOf("-->")).Trim(),
                        EndTime = fileContent[item + 1].Substring(fileContent[item + 1].LastIndexOf("-->") + 3).Trim(),
                        TextLineOne = fileContent[item + 2],
                        TextLineTwo = fileContent[item + 3]

                    });
                    // The block numbers of SRT like 1, 2, 3, ... and so on
                    segment++;
                    // Iterate one block at a time
                    item += 1;
                }
            }
        }

        public Component GetComponent(long id)
        {
            return content.FirstOrDefault(z => z.Count == id);
        }

        public List<Component> GetComponents()
        {
            return content;
        }
    }
}
