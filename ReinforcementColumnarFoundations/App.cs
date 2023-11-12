using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Selection;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using System.Windows.Media;
using ReinforcementColumnarFoundations;

namespace ReinforcementColumnarFoundations
{
    public class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            application.CreateRibbonTab("RAM");

            //Создание категории ""
            RibbonPanel panel = application.CreateRibbonPanel("RAM", "Общая");

            PushButtonData pbdRCF = new PushButtonData("Армирование фундамента", "Армирование\nфундамента", assemblyPath, "ReinforcementColumnarFoundations.ReinforcementColumnarFoundationsCommand");

            Image img5 = ReinforcementColumnarFoundations.Properties.Resource.img5;
            ImageSource imgLarge5 = GetImageSourse(img5);
            pbdRCF.LargeImage = imgLarge5;
            panel.AddItem(pbdRCF);


            return Result.Succeeded;
        }
        private BitmapSource GetImageSourse(Image img)
        {
            BitmapImage bmp = new BitmapImage();
            using (MemoryStream ms = new MemoryStream())
            {
                img.Save(ms, ImageFormat.Png);
                ms.Position = 0;

                bmp.BeginInit();

                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.UriSource = null;
                bmp.StreamSource = ms;

                bmp.EndInit();
            }
            return bmp;
        }

    }
}
