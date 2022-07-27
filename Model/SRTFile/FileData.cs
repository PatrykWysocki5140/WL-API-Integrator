using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Antheap.Model.SRTFile
{
    internal class FileData
    {
        protected List<Component> content = new();
        protected List<Component> cutContentFirst = new();
        protected List<Component> cutContentSecond = new();


        protected string filePath { get; set; }

        public FileData(string path)
        {
            filePath = path;

            var fileContent = File.ReadAllLines(path);
            if (fileContent.Length <= 0)
                return;

            long segment = 1;
            for (long item = 0; item < fileContent.Length; item++)
            {
                if (segment.ToString() == fileContent[item])
                {
                    if ((item + 3) < fileContent.Length)
                    {
                        content.Add(new Component
                        {
                            Count = segment,
                            StartTime = TimeSpan.Parse(fileContent[item + 1].Substring(0, fileContent[item + 1].LastIndexOf("-->")).Trim()),
                            EndTime = TimeSpan.Parse(fileContent[item + 1].Substring(fileContent[item + 1].LastIndexOf("-->") + 3).Trim()),
                            TextLineOne = fileContent[item + 2],
                            TextLineTwo = fileContent[item + 3]

                        });
                    }
                    else
                    {
                        content.Add(new Component
                        {
                            Count = segment,
                            StartTime = TimeSpan.Parse(fileContent[item + 1].Substring(0, fileContent[item + 1].LastIndexOf("-->")).Trim()),
                            EndTime = TimeSpan.Parse(fileContent[item + 1].Substring(fileContent[item + 1].LastIndexOf("-->") + 3).Trim()),
                            TextLineOne = fileContent[item + 2],
                            TextLineTwo = ""

                        });
                    }
                    segment++;

                }
            }          
        }
        public bool FileOperations()
        {
            MoveTheTime();
            CutComponents();
            File.Delete(filePath);
            WriteToFile(filePath);
            return true;
        }

        public Component GetComponent(long id)
        {
            return content.FirstOrDefault(z => z.Count == id);
        }

        public List<Component> GetComponents()
        {
            return content;
        }

        private void MoveTheTime()
        {
            TimeSpan moveTimeSpan = new TimeSpan(0, 0, 0, 05, 880);
            foreach (var obj in content)
            {
                obj.StartTime = obj.StartTime.Add(moveTimeSpan);
                obj.EndTime = obj.EndTime.Add(moveTimeSpan);
            }
        }

        private void CutComponents()
        {
            long i = 1;
            long j = 1;
            foreach(var obj in content)
            {
                var component = obj as Component;
                if (component.StartTime.Milliseconds > 0)
                {
                    component.Count = i;                 
                    cutContentSecond.Add(component);
                    i++;
                }
                else
                {
                    component.Count = j;
                    cutContentFirst.Add(component);
                    j++;
                }
            }
            content.Clear();
        }

        private void WriteToFile(string path)
        {
            string filename = Path.GetFileName(path);
            string newPath = path.Replace(filename,"") + @"\napisy do filmu2.srt";
            using (StreamWriter writer = new StreamWriter(newPath))
            {
                foreach(var component in cutContentFirst)
                {
 
                    writer.WriteLine(component.Count.ToString());
                    writer.WriteLine("{0} --> {1}",component.StartTime.ToString(), component.EndTime.ToString());
                    writer.WriteLine(component.TextLineOne.ToString());
                    if (component.TextLineTwo.ToString() != "")
                        writer.WriteLine(component.TextLineTwo.ToString());

                    writer.WriteLine();
                }
                
            }
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var component in cutContentSecond)
                {
                    writer.WriteLine(component.Count.ToString());
                    writer.WriteLine("{0} --> {1}", component.StartTime.ToString(), component.EndTime.ToString());
                    writer.WriteLine(component.TextLineOne.ToString());
                    if (component.TextLineTwo.ToString() != "")
                        writer.WriteLine(component.TextLineTwo.ToString());

                    writer.WriteLine();
                }
            }
        }
    }
}
