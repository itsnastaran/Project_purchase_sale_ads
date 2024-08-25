using Microsoft.AspNetCore.Mvc;
using Projectcore.Data;
using Projectcore.Models;
using Temp;
using Utility;
using Domain;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using SmsIrRestful;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Projectcore.Controllers
{
    [Authorize(Roles ="user")]
    public class AccountController : BaseController
    {
        string merchant = "cfa83c81-89b0-4993-9445-2c3fcd323455";
        string amount;
        string authority;
        string description = "خرید آگهی ویژه";
        string callbackurl = "https://localhost:7226/Account/VerifyPayment";
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _db;
       
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting;
        public AccountController(ILogger<HomeController> logger, IUnitOfWork db, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting,UserManager<ApplicationUser> userManager)
            :base(db)
        {
            _userManager = userManager;
            _logger = logger;
            _hosting = hosting;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Category()
        {
            var result = _db.CategoryRepository.GetByList();
            return View(result);
        }
        public IActionResult SubCategory(int idcategory)
        {
            var result = _db.SubCategoryRepository.Get(x => x.CategoryID == idcategory, null, "").ToList();
            return View(result);
        }
        public IActionResult InsertAd(int idcatgory, int subcategory)
        {
            var resultcategory = _db.CategoryRepository.GetBYID(idcatgory);
            var resultsubcategory = _db.SubCategoryRepository.GetBYID(subcategory);
            
            if(resultcategory==null || resultsubcategory==null)
            {
                return NotFound();
            }
            var user = User.Identity.Name;
            var getUser = _db.ApplicationUserRepository.GetByList().Where(x => x.UserName == user).FirstOrDefault();
            if(getUser.PhoneNumber==null || getUser.NameFamily==null || getUser.SellerStatus==false)
            {
                return RedirectToAction("EditUser", "Account");
            }
            var Rules = _db.RulesRepository.GetByList();
            var ProductTypeId = _db.ProductTypeRepository.GetByList();
            var TypeOfAdId = _db.TypeOfAdRepository.GetByList();
            var CategoryDetail = _db.CategoryDetailsRepository.GetByList().Where(x => x.CategoryID == idcatgory).ToList();
            ViewData["ProductTypeId"] = ProductTypeId;
            ViewData["IDSubCategory"] = resultsubcategory.ID;
            ViewData["IDCataegory"] = resultcategory.ID;
           
            ViewData["TitleSubCategory"] = resultsubcategory.Title;
            ViewData["TitleCategory"] = resultcategory.Title;
            ViewData["Icon"] = resultcategory.Icon;
            ViewData["CategoryDetail"] = CategoryDetail;
            ViewData["UserNameFamily"] = getUser.NameFamily;
            ViewData["UserEmail"] = getUser.Email;
            ViewData["UserPhoneNumber"] = getUser.PhoneNumber;
            ViewData["UserId"] = getUser.Id;
            if (TypeOfAdId.Count() == 0)
            {
                ViewData["TypeOfAdId"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["TypeOfAdId"] = TypeOfAdId;
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> InsertAd(InsertAdTemp temp,int TypeOfAdId,int ProductTypeId,List<int> CategoryDetail,string iduser,List<IFormFile> uploadimage,int IDSubCategory,int IDCataegory, int premiumOption, int RulesCheck)
        {
            //جستجو آی دی کاربر
            var user = User.Identity.Name;
            var getuser = _db.ApplicationUserRepository.GetByList().Where(x => x.UserName == user).FirstOrDefault();
            var Rules = _db.RulesRepository.GetByList();
            var ProductTypeIds = _db.ProductTypeRepository.GetByList();
            var TypeOfAdIds = _db.TypeOfAdRepository.GetByList();
            var CategoryDetails = _db.CategoryDetailsRepository.GetByList().Where(x => x.CategoryID == IDCataegory).ToList();
            var resultcategory = _db.CategoryRepository.GetBYID(IDCataegory);
            var resultsubcategory = _db.SubCategoryRepository.GetBYID(IDSubCategory);

            ViewData["IDSubCategory"] = resultsubcategory.ID;
            ViewData["IDCataegory"] = resultcategory.ID;
            ViewData["TitleSubCategory"] = resultsubcategory.Title;
            ViewData["TitleCataegory"] = resultcategory.Title;
            ViewData["Icon"] = resultcategory.Icon;
            ViewData["UserNameFamily"] = getuser.NameFamily;
            ViewData["UserEmail"] = getuser.Email;
            ViewData["UserPhoneNumber"] = getuser.PhoneNumber;
            ViewData["UserId"] = getuser.Id;
            if (TypeOfAdIds.Count() == 0)
            {
                ViewData["TypeOfAdId"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["TypeOfAdId"] = TypeOfAdIds;
            }
            if (Rules.Count() == 0)
            {
                ViewData["Rules"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["Rules"] = Rules;
            }
            if (ProductTypeIds.Count() == 0)
            {
                ViewData["ProductTypeId"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["ProductTypeId"] = ProductTypeIds;
            }
            if (CategoryDetails.Count() == 0)
            {
                ViewData["CategoryDetail"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["CategoryDetail"] = CategoryDetails;
            }

            if (RulesCheck <= 0 || ProductTypeId==0 || TypeOfAdId==0)
                return View(temp);
            
            SaleAd ad = new SaleAd();
            ad.Brand = temp.Brand;
            ad.CategryID = IDCataegory;
            ad.Date = GetDateTime();
            ad.Description = temp.Description;
            ad.Model = temp.Model;
            ad.Price = temp.Price;
            ad.ProductTypeId = ProductTypeId;
            ad.Special = temp.Special;
            ad.SpecialDate = GetDateTime();
            ad.Status = false;
            ad.SubCategoryId = IDSubCategory;
            ad.Text = temp.Text;
            ad.Title = temp.Title;
            ad.TypeOfAdId = TypeOfAdId;
            ad.UserID = iduser;
            ad.Visits = 0;
            _db.SaleAdRepository.Insert(ad);
            int res = _db.save();
            if(res>0)
            {
                //var token = new Token().GetToken("", "");
                //var mesg = new MessageSendObject()
                //{
                //    Messages=new List<string> { "آگهی شما در سایت ما ثبت گردید"}.ToArray(),
                //    MobileNumbers=new List<string> { getuser.PhoneNumber}.ToArray(),
                //    LineNumber="",
                //    SendDateTime=null,
                //    CanContinueInCaseOfError=true
                //};
                //MessageSendResponseObject messageSendResponseObject = new MessageSend().Send(token, mesg);
                //if(messageSendResponseObject.IsSuccessful)
                //{

                //}
                //else
                //{

                //}
                if(CategoryDetail.Count()>0)
                {
                    try
                    {
                        foreach (var item in CategoryDetail)
                        {
                            CategoryDetailsAd cda = new CategoryDetailsAd();
                            cda.IDAd = ad.ID;
                            cda.IDCategoryDetails = item;
                            _db.CategoryDetailsAdRepository.Insert(cda);
                            _db.save();
                        }
                    }
                    catch
                    {
                        throw;

                    }
                }
                var upload = Path.Combine(_hosting.WebRootPath, "upload/Ad");
                if(uploadimage.Count() >0)
                {
                    try
                    {
                        foreach (var item in uploadimage)
                        {
                            Random rnd = new Random();
                            string name = rnd.Next(0000, 9999).ToString() + "-" + DateTime.Now.Millisecond.ToString();
                            string ex = Path.GetExtension(item.FileName);
                            string namefile = name + ex;
                            PhotoGallery pg = new PhotoGallery();
                            var file = Path.Combine(upload, namefile);
                            using (var f = new FileStream(file, FileMode.Create))
                            {
                                await item.CopyToAsync(f).ConfigureAwait(false);
                            }
                            pg.SaleAdId = ad.ID;
                            pg.Title = namefile;
                            _db.PhotoGalleryRepository.Insert(pg);
                            int result = _db.save();
                            

                        }
                        
                    }
                    catch
                    {
                        throw;
                    }
                }
                if(premiumOption>0)
                {
                    try
                    {

                        amount = premiumOption.ToString();
                        string[] metadata = new string[2];
                        metadata[0] = "";
                        metadata[1] = "";
                        string requesturl;
                        requesturl= "https://api.zarinpal.com/pg/v4/payment/request.json?merchant_id=" +
                            merchant + "&amount=" + amount +
                            "&callback_url=" + callbackurl +
                            "&description=" + description;
                        ;
                        var client = new RestClient(requesturl);
                        Method method = Method.Post;
                        var request = new RestRequest("", method);
                        request.AddHeader("accept", "application/json");
                        request.AddHeader("content-type", "application/json");
                        var requestresponse = client.ExecuteAsync(request);
                        Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Result.Content);
                        string errorscode = jo["errors"].ToString();
                        Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Result.Content);
                        string dataauth = jodata["data"].ToString();
                        if(dataauth!="[]")
                        {
                            authority = jodata["data"]["authority"].ToString();
                            string geturl= "https://www.zarinpal.com/pg/StartPay/" + authority;
                            try
                            {
                                Payment pay = new Payment();
                                pay.AdId = ad.ID;
                                pay.Status = false;
                                pay.Authority = authority;
                                pay.Price = amount;
                                pay.RefId = "";
                                pay.UserId = iduser;
                                _db.PaymentRepository.Insert(pay);
                                _db.save();
                            }
                            catch
                            {
                                throw;
                            }
                            return Redirect(geturl);
                        }
                        else
                        {
                            return BadRequest("error" + errorscode);
                        }
                    }
                    catch
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            return View(temp);
        }
        public IActionResult EditAd(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var username = User.Identity.Name;
            
            var Ad = _db.SaleAdRepository.GetBYID(id);
            if(Ad==null)
            {
                return NotFound();
            }
            else
            {
                var usercheck = _db.ApplicationUserRepository.GetByList().Where(x => x.UserName == username).FirstOrDefault();
                if(usercheck!=null)
                {
                    var usercheck2 = _db.SaleAdRepository.GetByList().Where(x => x.UserID == usercheck.Id && x.ID == id).FirstOrDefault();
                    if (usercheck2 == null)
                    {
                        return NoContent();
                    }
                    var ProductTypeIds = _db.ProductTypeRepository.GetByList();
                    var TypeOfAdIds = _db.TypeOfAdRepository.GetByList();
                    var CategoryDetails = _db.CategoryDetailsRepository.GetByList().Where(x => x.CategoryID == Ad.CategryID).ToList();
                    var resultcategory = _db.CategoryRepository.GetBYID(Ad.CategryID);
                    var resultsubcategory = _db.SubCategoryRepository.GetBYID(Ad.SubCategoryId);
                    ViewData["TitleSubCategory"] = resultsubcategory.Title;
                    ViewData["TitleCataegory"] = resultcategory.Title;
                    ViewData["Icon"] = resultcategory.Icon;
                    if (ProductTypeIds.Count() == 0)
                    {
                        ViewData["ProductTypeId"] = null;
                        ViewData["ErrorShow"] = "موردی ثبت نشده";
                    }
                    else
                    {
                        ViewData["ProductTypeId"] = ProductTypeIds;
                    }
                    if (TypeOfAdIds.Count() == 0)
                    {
                        ViewData["TypeOfAdId"] = null;
                        ViewData["ErrorShow"] = "موردی ثبت نشده";
                    }
                    else
                    {
                        ViewData["TypeOfAdId"] = TypeOfAdIds;
                    }
                    if (CategoryDetails.Count() == 0)
                    {
                        ViewData["CategoryDetail"] = null;
                        ViewData["ErrorShow"] = "موردی ثبت نشده";
                    }
                    else
                    {
                        ViewData["CategoryDetail"] = CategoryDetails;
                    }
                    return View(Ad);
                }
                

                return NoContent(); 
            }
            
        }
        
       [HttpPost]
        public async Task<IActionResult> EditAd(SaleAd salead)
        {
            if (salead.ID == null)
            {
                return NoContent();
            }
            
            var res =  _db.SaleAdRepository.GetBYID(salead.ID);
            var resultcategory = _db.CategoryRepository.GetBYID(salead.CategryID);
            var resultsubcategory = _db.SubCategoryRepository.GetBYID(salead.SubCategoryId);
            ViewData["TitleSubCategory"] = resultsubcategory.Title;
            ViewData["TitleCataegory"] = resultcategory.Title;
            ViewData["Icon"] = resultcategory.Icon;
            var ProductTypeIds = _db.ProductTypeRepository.GetByList();
            var TypeOfAdIds = _db.TypeOfAdRepository.GetByList();
            var CategoryDetails = _db.CategoryDetailsRepository.GetByList().Where(x => x.CategoryID == salead.CategryID).ToList();
            if (ProductTypeIds.Count() == 0)
            {
                ViewData["ProductTypeId"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["ProductTypeId"] = ProductTypeIds;
            }
            if (TypeOfAdIds.Count() == 0)
            {
                ViewData["TypeOfAdId"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["TypeOfAdId"] = TypeOfAdIds;
            }
            if (CategoryDetails.Count() == 0)
            {
                ViewData["CategoryDetail"] = null;
                ViewData["ErrorShow"] = "موردی ثبت نشده";
            }
            else
            {
                ViewData["CategoryDetail"] = CategoryDetails;
            }
            if (salead.ProductTypeId == 0 || salead.TypeOfAdId == 0)
                return View(salead);
            if (res != null)
            {
                
                res.Title = salead.Title;
                res.Brand = salead.Brand;
                res.CategryID = salead.CategryID;
                res.Date = salead.Date;
                res.Description = salead.Description;
                res.Model = salead.Model;
                res.Price = salead.Price;
                res.ProductTypeId = salead.ProductTypeId;
                res.Special = salead.Special;
                res.SpecialDate = salead.SpecialDate;
                res.Status = salead.Status;
                res.SubCategoryId = salead.SubCategoryId;
                res.Text = salead.Text;
                res.TypeOfAdId = salead.TypeOfAdId;
                res.UserID = salead.UserID;
                res.Visits = salead.Visits;


                _db.SaleAdRepository.Update(res);
                _db.save();
                
            }
            return RedirectToAction(nameof(Profile));
        }
        public IActionResult VerifyPayment()
        {
            try
            {
                if(HttpContext.Request.Query["Authority"] !="")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }
                string url = "https://api.zarinpal.com/pg/v4/payment/verify.json?merchant_id=" +
                   merchant + "&amount="
                   + amount + "&authority="
                   + authority;
                var client = new RestClient(url);
                Method method = Method.Post;
                var request = new RestRequest("", method);
                request.AddHeader("accept", "application/json");
                request.AddHeader("content-type", "application/json");
                var response = client.ExecuteAsync(request);

                Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(response.Result.Content);
                string data = jodata["data"].ToString();

                Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(response.Result.Content);
                string errors = jo["errors"].ToString();
                if(data!="[]")
                {
                    string refid = jodata["data"]["ref_id"].ToString();
                    ViewBag.code = refid;
                    var qpay = _db.PaymentRepository.GetByList().Where(x => x.Status == false && x.Authority == authority).FirstOrDefault();
                    if(qpay==null)
                    {
                        return View();
                    }
                    else
                    {
                        qpay.Status = true;
                        qpay.RefId = refid;
                        _db.PaymentRepository.Update(qpay);
                        _db.save();
                        return View();
                    }
                    return View();
                }
                else if(errors!="[]")
                {
                    string errorscode = jo["errors"]["code"].ToString();
                    return BadRequest($"خطای شماره {errorscode}");
                }
                return View();
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Favorties(int id, string idu)
        {
            if(id<=0 || idu==null)
            {
                return NoContent();
            }
            var result = _db.SaleAdRepository.GetBYID(id);
            if(result!=null)
            {
                var usercheck = _db.FavortiesRepository.GetByList().Where(x => x.UserID == idu && x.SaleAdId == id).FirstOrDefault();
                if(usercheck!=null)
                {
                    return RedirectToAction("AdSingel", "Home", new { id = id });
                }
                Favorties fa = new Favorties();
                fa.UserID = idu;
                fa.SaleAdId = id;
                _db.FavortiesRepository.Insert(fa);
                _db.save();
            }
            return RedirectToAction("AdSingel", "Home", new { id = id });
        }
        public IActionResult Report(int id)
        {
            if (id <= 0)
            {
                return NoContent();
            }
            var result = _db.SaleAdRepository.GetBYID(id);
            if (result != null)
            {
                
                Report re = new Report();
                re.status = false;
                re.Date = GetDate();
                re.AdID = id;
                
                _db.ReportRepository.Insert(re);
                _db.save();
            }
            return RedirectToAction("AdSingel", "Home", new { id = id });
        }
        public async Task<IActionResult> Profile()
        {

            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity.Name;
                var res = await _userManager.FindByNameAsync(user);
                UserTemp ut = new UserTemp();
                ut.UserName = res.UserName;
                ut.Id = res.Id;
                ut.Address = res.Address;
                ut.Mobile = res.PhoneNumber;
                ut.SellerStatus = false;
                ut.NameFamily = res.NameFamily;
                ut.Postalcode = res.Postalcode;
                var result = _db.SaleAdRepository.GetByList().Where(x => x.UserID == ut.Id).ToList();
                ViewData["MyAds"] = result.Count();
                var fav = _db.FavortiesRepository.GetByList().Where(x => x.UserID == ut.Id).ToList();
                ViewData["MyFav"] = fav.Count();
                return View(ut);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Profile(UserTemp userTemp)
        {
            if(userTemp.Id==null)
            {
                return NoContent();
            }
            var result = _db.SaleAdRepository.GetByList().Where(x => x.UserID == userTemp.Id).ToList();
            ViewData["MyAds"] = result.Count();
            var fav = _db.FavortiesRepository.GetByList().Where(x => x.UserID == userTemp.Id).ToList();
            ViewData["MyFav"] = fav.Count();
            var res = await _userManager.FindByIdAsync(userTemp.Id);
            if(res!=null)
            {
                res.Address = userTemp.Address;
                res.NameFamily = userTemp.NameFamily;
                res.PhoneNumber = userTemp.Mobile;
                res.Postalcode = userTemp.Postalcode;
                res.SellerStatus = userTemp.SellerStatus;
                await _userManager.UpdateAsync(res);
            }
            return RedirectToAction(nameof(Profile));
        }
        public async Task<IActionResult> MyAd(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var fav = _db.FavortiesRepository.GetByList().Where(x => x.UserID == id).ToList();
            ViewData["MyFav"] = fav.Count();
            List<AdTemp> temps = new List<AdTemp>();
            var result = _db.SaleAdRepository.GetByList().Where(x => x.UserID == id).ToList();
            if(result.Count() ==0)
            {
                AdTemp t = new AdTemp();
                t.Userid = user.Id;
                t.NameFamily = user.NameFamily;
                temps.Add(t);
            }
            else
            {
                foreach (var item in result)
                {
                    var cat = _db.CategoryRepository.GetBYID(item.CategryID);
                    var pic = _db.PhotoGalleryRepository.GetByList().Where(x => x.SaleAdId == item.ID).FirstOrDefault();
                    AdTemp t = new AdTemp();
                    t.Title = item.Title;
                    t.Date = item.Date;
                    t.Price = item.Price;
                    t.Description = item.Description;
                    t.Pic = pic.Title;
                    t.Userid = user.Id;
                    t.NameFamily = user.NameFamily;
                    t.ID = item.ID;
                    temps.Add(t);
                }
            }
          
            ViewData["MyAds"] = result.Count();
            return View(temps);
            
        }
        public async Task<IActionResult> MyFavourite(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var res = _db.SaleAdRepository.GetByList().Where(x => x.UserID == id).ToList();
            ViewData["MyAds"] = res.Count();
            
            List<AdTemp> temps = new List<AdTemp>();
            var fav = _db.FavortiesRepository.GetByList().Where(x => x.UserID == id).ToList();
            if(fav.Count()==0)
            {
                AdTemp t = new AdTemp();
                t.Userid = user.Id;
                t.NameFamily = user.NameFamily;
                temps.Add(t);
            }
            else
            {
                foreach (var item in fav)
                {
                    var result = _db.SaleAdRepository.GetBYID(item.SaleAdId);
                    var cat = _db.CategoryRepository.GetBYID(result.CategryID);
                    var pic = _db.PhotoGalleryRepository.GetByList().Where(x => x.SaleAdId == result.ID).FirstOrDefault();
                    AdTemp t = new AdTemp();
                    t.Title = result.Title;
                    t.Date = result.Date;
                    t.Price = result.Price;
                    t.Description = result.Description;
                    t.Pic = pic.Title;
                    t.Userid = user.Id;
                    t.NameFamily = user.NameFamily;
                    t.ID = result.ID;
                   
                    temps.Add(t);
                }
            }
            ViewData["MyFav"] = fav.Count();
            return View(temps);

        }
        public IActionResult DeleteAd(int id,string idu)
        {
            var result = _db.SaleAdRepository.GetBYID(id);
            if(result==null)
            {
                return NotFound();
            }
            else
            {
                var res = _db.CategoryDetailsAdRepository.GetByList().Where(x => x.IDAd == id).ToList();
                _db.CategoryDetailsAdRepository.DeleteRange(res);
                _db.save();
                _db.SaleAdRepository.Delete(result);
                _db.save();
            }
            return RedirectToAction(nameof(MyAd), new { id = idu });
        }
        public IActionResult DeleteFav(int idad,string idu)
        {
            var result = _db.FavortiesRepository.GetByList().Where(x => x.UserID == idu && x.SaleAdId == idad).FirstOrDefault();
            if (result != null)
                _db.FavortiesRepository.Delete(result);
            _db.save();
            return RedirectToAction(nameof(MyFavourite), new { id = idu });
        }
    }
}
