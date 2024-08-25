using Domain;
using Microsoft.AspNetCore.Mvc;
using Projectcore.Models;
using System.Diagnostics;
using Temp;
using Utility;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace Projectcore.Controllers
{
    public class HomeController : BaseController
    {
        public string Getsession(string key)
        {
            try
            {
                return HttpContext.Session.GetString(key);
            }
            catch
            {
                return null;
            }
        }
        public string GetDate()
        {
            return DateTime.Now.ToPersion();
        }
        public string GetDateTime()
        {
            return DateTime.Now.ToPersion() + " " + DateTime.Now.ToString("HH:mm:ss");
        }
        public string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;
        
        public class Temp
        {
            public int IDCategory { get; set; }
            public int Count { get; set; }
        }
        public class TempGallery
        {
            public string Title { get; set; }
            public int IDPic { get; set; }
            public int IDAd { get; set; }
        }
        public class TempCategory
        {
            public string Title { get; set; }
            public int IDCategory { get; set; }
            public int IDAd { get; set; }
        }
        public HomeController(ILogger<HomeController> logger,IUnitOfWork db)
            :base(db)
        {
            _logger = logger;
            _db = db;
            
        }

        public IActionResult Index()
        {
            var category = _db.CategoryRepository.GetByList();
            var Ad = _db.SaleAdRepository.GetByList().Where(x => x.Status == true);
            
            if(category.Count()==0)
            {
                ViewData["Category"] = null;
                ViewData["ErrorCategory"] = "دسته بندی ثبت نشده است.";
            }
            else
            {
                List<Temp> temp = new List<Temp>();
                foreach (var item in category)
                {
                    var qcount = _db.SaleAdRepository.GetByList().Where(x => x.CategryID == item.ID && x.Status == true).Count();
                    Temp t = new Temp();
                    t.IDCategory = item.ID;
                    t.Count = qcount;
                    temp.Add(t);
                }
                ViewData["CountCategory"] = temp;
                ViewData["Category"] = category;
            }
            if(Ad.Count()==0)
            {
                ViewData["AdNew"] = null;
                ViewData["ErrorAdNew"] = "آگهی ثبت نشده است";
                ViewData["AdVisit"] = null;
                ViewData["ErrorAdVisit"] = "آگهی ثبت نشده است";
            }
            else
            {
                List<TempGallery> tempg = new List<TempGallery>();
                foreach (var item in Ad)
                {
                    var qpic = _db.PhotoGalleryRepository.GetByList().Where(x => x.SaleAdId == item.ID).FirstOrDefault();
                    TempGallery t = new TempGallery();
                    if (qpic != null)
                    {
                        t.IDAd = item.ID;
                        t.Title = qpic.Title;
                        t.IDPic = qpic.ID;
                    }
                    tempg.Add(t);
                }
                List<TempCategory> tempcat = new List<TempCategory>();
                foreach (var C in Ad)
                {
                    var qcat = _db.CategoryRepository.GetByList().Where(x => x.ID == C.CategryID).FirstOrDefault();
                    TempCategory t = new TempCategory();
                    t.IDAd = C.ID;
                    t.Title = qcat.Title;
                    t.IDCategory = qcat.ID;
                    tempcat.Add(t);
                }
                ViewData["AdCAt"] = tempcat;
                ViewData["AdPic"] = tempg;
                ViewData["AdVisit"] = Ad.OrderByDescending(x => x.Visits).Take(5);
                ViewData["AdNew"] = Ad.OrderByDescending(x => x.ID).Take(10);
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult ContactUs()
        {
            ViewData["About"] = _db.AboutUsRepository.GetByList().LastOrDefault();
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult ContactUs(ContactUsTemp contact,string Captcha)
        {
            if(Getsession("Captcha")==null || Getsession("Captcha")!=Captcha)
            {
                return NotFound();
            }
            if(!ModelState.IsValid)
            {
                return View(contact);
            }
            ContactUs c = new ContactUs();
            c.Email = contact.Email;
            c.NameFamily = contact.NameFamily;
            c.Message = contact.Message;
            c.Title = contact.Title;
            c.status = false;
            c.Date = GetDateTime();
            _db.ContactUsRepository.Insert(c);
            int result = _db.save();
            if(result>0)
            {
                return View();

            }
            return View(contact);
        }
        public IActionResult AboutUs()
        {
            var result = _db.AboutUsRepository.GetByList().FirstOrDefault();
            return View(result);
        }
        public async Task<IActionResult> CategoryDetails(int idcategory,int ?page,List<int> filter)
        {
            
            var allcategory = _db.CategoryRepository.GetByList();
            if (allcategory.Count() == 0)
            {
                ViewData["AllCategory"] = null;
            }
            else
            {
                ViewData["AllCategory"] = allcategory;
            }
            List<Temp> temp = new List<Temp>();
            foreach (var item in allcategory)
            {
                var qcount = _db.SaleAdRepository.GetByList().Where(x => x.CategryID == item.ID && x.Status == false).Count();
                Temp t = new Temp();
                t.IDCategory = item.ID;
                t.Count = qcount;
                temp.Add(t);
            }
            ViewData["CountCategory"] = temp;


            var pagenumber = page ?? 1;
            var pagesize = 3;
            var qtype = _db.ProductTypeRepository.GetByList();
            ViewData["ProductType"] = qtype;
            ViewData["IDCategory"] = idcategory;
            ViewData["Filter"] = filter;
            if(idcategory>0)
            {
                var qthiscat = _db.CategoryRepository.GetBYID(idcategory);
                ViewData["ThisCat"] = qthiscat;
                var qcategory = _db.SaleAdRepository.GetByList().Where(x => x.CategryID == idcategory && x.Status == false).ToList();
                if(qcategory.Count()==0)
                {
                    return View();
                }
                else
                {
                    List<TempGallery> tempg = new List<TempGallery>();
                    foreach (var item in qcategory)
                    {
                        var qpic = _db.PhotoGalleryRepository.GetByList().Where(x => x.SaleAdId == item.ID).FirstOrDefault();
                        TempGallery t = new TempGallery();
                        t.IDAd = item.ID;
                        t.Title = qpic.Title;
                        t.IDPic = qpic.ID;
                        tempg.Add(t);
                    }
                    List<TempCategory> temcat = new List<TempCategory>();
                    foreach (var C in qcategory)
                    {
                        var qcat = _db.CategoryRepository.GetByList().Where(x => x.ID == C.CategryID).FirstOrDefault();
                        TempCategory t = new TempCategory();
                        t.IDAd = C.ID;
                        t.Title = qcat.Title;
                        t.IDCategory = qcat.ID;
                        temcat.Add(t);
                    }
                    ViewData["AdCAt"] = temcat;
                    ViewData["AdPic"] = tempg;
                    if (filter.Count()>0)
                    {
                        List<SaleAd> listad = new List<SaleAd>();
                        foreach (var item in filter)
                        {
                            var q = qcategory.Where(x => x.ProductTypeId == item).ToList();
                            if(q.Count()>0)
                            
                              
                                foreach (var ad in q)
                                {
                                    listad.Add(ad);
                                }
                            
                        }
                        return View(await listad.ToPagedListAsync(pagenumber, pagesize));
                    }
                    else
                    {
                        return View(await qcategory.ToPagedListAsync(pagenumber, pagesize));
                    }
                }
            }
            else
            {
                return NotFound();
            }
           
        }
        public IActionResult AdSingel(int id)
        {
            if(id>0)
            {
                var result = _db.SaleAdRepository.GetBYID(id);
                if(result!=null)
                {
                    var category = _db.CategoryRepository.GetBYID(result.CategryID);
                    var infouser = _db.ApplicationUserRepository.GetBYID(result.UserID);
                    var categorydetail = _db.CategoryDetailsAdRepository.GetByList().Where(x => x.IDAd == result.ID).ToList();
                    List<CategoryDetails> lst = new List<CategoryDetails>();
                    if (categorydetail.Count() > 0)
                    
                        foreach (var item in categorydetail)
                        {
                            var resultcategorydetail = _db.CategoryDetailsRepository.GetBYID(item.IDCategoryDetails);
                            lst.Add(resultcategorydetail);
                        }
                    
                    var gallry = _db.PhotoGalleryRepository.GetByList().Where(x => x.SaleAdId == result.ID).ToList();
                    var typeAd = _db.TypeOfAdRepository.GetBYID(result.TypeOfAdId);
                    var producttype = _db.ProductTypeRepository.GetBYID(result.ProductTypeId);
                    ViewData["TypeAd"] = typeAd;
                    ViewData["ProductType"] = producttype;
                    ViewData["InfoUser"] = infouser;
                    ViewData["Category"] = category;
                    ViewData["Gallery"] = gallry;
                    ViewData["CategoryDetail"] = lst;
                    return View(result);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
            
        }
        public IActionResult Search(int id,string text,int ?page)
        {
            var qad = _db.SaleAdRepository.GetByList().Where(x => x.Status == true);
            if(qad.Count()==0)
            {
                return View();
            }
            List<TempGallery> tempg = new List<TempGallery>();
            foreach (var item in qad)
            {     
                var qpic = _db.PhotoGalleryRepository.GetByList().Where(x => x.SaleAdId == item.ID).FirstOrDefault();
                TempGallery t = new TempGallery();
                t.IDAd = item.ID;
                t.Title = qpic.Title;
                t.IDPic = qpic.ID;
                tempg.Add(t);
            }
            List<TempCategory> tempcat = new List<TempCategory>();
            foreach (var C in qad)
            {
                var qcat = _db.CategoryRepository.GetByList().Where(x => x.ID == C.CategryID).FirstOrDefault();
                TempCategory t = new TempCategory();
                t.IDAd = C.ID;
                t.Title = qcat.Title;
                t.IDCategory = qcat.ID;
                tempcat.Add(t);
            }
            var category = _db.CategoryRepository.GetByList();
            
            var pagenumber = page ?? 1;
            var pagesize = 3;
            //
            ViewData["category"] = category;
            ViewData["AdCAt"] = tempcat;
            ViewData["AdPic"] = tempg;
            if(id==0 && text==null)
            {
                ViewData["myid"] = id;
                ViewData["mytext"] = text;
                return View(qad.ToPagedList(pagenumber,pagesize));
            }
            else if(id>0 && text==null)
            {
                ViewData["myid"] = id;
                ViewData["mytext"] = text;
                var listt = (qad.Where(x => x.CategryID == id).ToList());
               
                return View(listt.ToPagedList(pagenumber, pagesize));
            }
            else if (id == 0 && text != null)
            {
                ViewData["myid"] = id;
                ViewData["mytext"] = text;
                var list1= (qad.Where(x => x.Title.Contains(text)).ToList());
                
                return View(list1.ToPagedList(pagenumber, pagesize));
            }
            else
            {
                ViewData["myid"] = id;
                ViewData["mytext"] = text;
                var list2 = (qad.Where(x => x.CategryID == id && x.Title.Contains(text)).ToList());
                
                return View(list2.ToPagedList(pagenumber, pagesize));
            }
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}