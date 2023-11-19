using System.IO;
using System.Xml.Serialization;
//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Wordprocessing;
//using Word = Microsoft.Office.Interop.Word;

namespace ReinforcementColumnarFoundations
{
    public class RainforcementColumnarFoundationsSettingsT1
    {
        public string Form01Name { get; set; }
        public string Form26Name { get; set; }
        public string Form11Name { get; set; }
        public string Form21Name { get; set; }
        public string Form51Name { get; set; }
        public string RebarHookTypeForStirrupName { get; set; }

        public string IndirectBarTapeName { get; set; }
        public string FirstMainBarTapeName { get; set; }
        public string LateralBarTapeName { get; set; }
        public string SecondBarTapesName { get; set; }
        public string BottomMainBarTapeName { get; set; }
        public string FirstStirrupBarTapeName { get; set; }

        public double StepIndirectRebar { get; set; }
        public double StepLateralRebar { get; set; }
        public int CountY { get; set; }
        public int CountX { get; set; }

        public string SupracolumnRebarBarCoverTypeName { get; set; }
        public string BottomRebarCoverTypeName { get; set; }

        public RainforcementColumnarFoundationsSettingsT1 GetSettings()
        {
            RainforcementColumnarFoundationsSettingsT1 rainforcementColumnarFoundationsSettingsT1 = null;
            string assemblyPathAll = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileName = "RainforcementColumnarFoundationsSettingsT1.xml";
            string assemblyPath = assemblyPathAll.Replace("ReinforcementColumnarFoundations.dll", fileName);

            if (File.Exists(assemblyPath))
            {
                using (FileStream fs = new FileStream(assemblyPath, FileMode.Open))
                {
                    XmlSerializer xSer = new XmlSerializer(typeof(RainforcementColumnarFoundationsSettingsT1));
                    rainforcementColumnarFoundationsSettingsT1 = xSer.Deserialize(fs) as RainforcementColumnarFoundationsSettingsT1;
                    fs.Close();
                }
            }
            else
            {
                rainforcementColumnarFoundationsSettingsT1 = null;
            }

            return rainforcementColumnarFoundationsSettingsT1;
        }
        public void SaveSettings()
        {
            string assemblyPathAll = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string fileName = "RainforcementColumnarFoundationsSettingsT1.xml";
            string assemblyPath = assemblyPathAll.Replace("ReinforcementColumnarFoundations.dll", fileName);

            if (File.Exists(assemblyPath))
            {
                File.Delete(assemblyPath);
            }
            using (FileStream fs = new FileStream(assemblyPath, FileMode.Create))
            {

                XmlSerializer xSer = new XmlSerializer(typeof(RainforcementColumnarFoundationsSettingsT1));
                xSer.Serialize(fs, this);
                fs.Close();
            }
        }
        //public void CreateWordDocument()
        //{
        //    string filePath = @"C:\Users\e.egorov\source\repos\RAMplugin\RAM\\bin\Debug";
        //    using (WordprocessingDocument wordDocument = WordprocessingDocument.Create(filePath, WordprocessingDocumentType.Document))
        //    {
        //        //Добавление основных частей документа
        //        MainDocumentPart mainPart = wordDocument.AddMainDocumentPart();
        //        mainPart.Document= new Document();
        //        Body body = new Body();

        //        //Добавление текста в документ
        //        Paragraph para = new Paragraph(new Run(new Text(filePath)));
        //        body.Append(para);
        //        mainPart.Document.Append(body);
        //    }
        //}

        //public void OpenWord()
        //{
        //    //Создание экземпляра приложения Word
        //    Word.Application wordApp = new Word.Application();
        //    //Создание нового документа
        //    Word.Document doc = wordApp.Documents.Add();
        //    //Добавление текста в документ
        //    Word.Paragraph paragraph = doc.Paragraphs.Add();
        //    paragraph.Range.Text = "Пример текста для документа Word";

        //    //Отображение документа
        //    wordApp.Visible = true;
        //}


    }
}