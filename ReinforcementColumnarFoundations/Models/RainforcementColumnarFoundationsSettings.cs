using System.IO;
using System.Xml.Serialization;

namespace ReinforcementColumnarFoundations.Models
{
    public class RainforcementColumnarFoundationsSettings
    {
        public string SelectedTypeButtonName { get; set; }

        public RainforcementColumnarFoundationsSettings GetSettings()
        {
            RainforcementColumnarFoundationsSettings settings = null;
            string assemblyPathAll = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileName = "RainforcementColumnarFoundationsSettings.xml";
            string assemblyPath = assemblyPathAll.Replace("ReinforcementColumnarFoundations.dll", fileName);

            if (File.Exists(assemblyPath))
            {
                using (FileStream fs = new FileStream(assemblyPath, FileMode.Open))
                {
                    XmlSerializer xSer = new XmlSerializer(typeof(RainforcementColumnarFoundationsSettings));
                    settings = xSer.Deserialize(fs) as RainforcementColumnarFoundationsSettings;
                    fs.Close();
                }
            }
            else
            {
                settings = null;
            }

            return settings;
        }
        public void SaveSettings()
        {
            string assemblyPathAll = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileName = "RainforcementColumnarFoundationsSettings.xml";
            string assemblyPath = assemblyPathAll.Replace("ReinforcementColumnarFoundations.dll", fileName);

            if (File.Exists(assemblyPath))
            {
                File.Delete(assemblyPath);
            }
            using (FileStream fs = new FileStream(assemblyPath, FileMode.Create))
            {
                XmlSerializer xSer = new XmlSerializer(typeof(RainforcementColumnarFoundationsSettings));
                xSer.Serialize(fs, this);
                fs.Close();
            }
        }
    }
}
