using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Projectcore.Controllers
{
    public class CaptchaController : Controller
    {
        public IActionResult CaptchaImage(string prefix,bool noisy=true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            int a = rand.Next(10, 90);
            int b = rand.Next(0, 9);
            var captcha = string.Format("{0}+{1}=?", a, b);
            HttpContext.Session.SetString("Captcha" + prefix, (a + b).ToString());
            FileContentResult img = null;
            using(var mem=new MemoryStream())
                using(var bmp=new Bitmap(130,30))
            using(var gfx=Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));
                if(noisy)
                {
                    int i, r, x, y;
                    var pen = new Pen(Color.Yellow);
                    for(i=1;i<10;i++)
                    {
                        pen.Color = Color.FromArgb(
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)),
                            (rand.Next(0, 255)));
                        r = rand.Next(0, (130 / 3));
                        x = rand.Next(0, 130);
                        y = rand.Next(0, 30);
                        gfx.DrawLine(pen, x - r, y - r, r, r);
                    }
                }
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.Gray, 2, 3);
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpge");
            }
            return img;
        }
    }
}
