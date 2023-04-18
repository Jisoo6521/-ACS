using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebCar.Models;

namespace WebCar.Controllers
{
    public class CarController : Controller
    {
        dbCarDataContext data = new dbCarDataContext();
        // GET: Car
        public ActionResult Index(int? page, string searchString)
        {
            ViewBag.Keyword = searchString;
            if (page == null) page = 1;
            var all_car = (from Car in data.Cars select Car).OrderBy(m => m.MaXe);
            if (!string.IsNullOrEmpty(searchString)) all_car = (IOrderedQueryable<Car>)all_car.Where(a => a.TenXe.Contains(searchString));
            int PageSize = 3;
            int PageNumber = page ?? 1;
            return View(all_car.ToPagedList(PageNumber, PageSize));
        }

        public ActionResult Detail(int id)
        {
            var D_xe = data.Cars.Where(m => m.MaXe == id).First();
            return View(D_xe);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Car s)
        {
            var E_tenxe = collection["TenXe"];
            var E_hinh = collection["Hinh"];
            var E_giaban = Convert.ToDecimal(collection["GiaTien"]);
            var E_soluongton = Convert.ToInt32(collection["SoLuong"]);
            //var E_hinh2 = collection["Hinh2"];
            //var E_hinh3 = collection["Hinh3"];
            var E_dongco = collection["DongCo"];
            var E_hopso = collection["HopSo"];
            var E_congsuat = collection["CongSuat"];
            var E_chieudai = Convert.ToDecimal(collection["ChieuDai"]);
            var E_chieurong = Convert.ToDecimal(collection["ChieuRong"]);
            //var E_loaixe = collection["Loai"];
            if (string.IsNullOrEmpty(E_tenxe))
            {
                ViewData["Error"] = "Dont emty";
            }
            else
            {
                s.TenXe = E_tenxe.ToString();
                s.Hinh = E_hinh.ToString();
                s.GiaBan = E_giaban;
                s.SoLuongTon = E_soluongton;
                s.DongCo = E_dongco;
                s.CongSuat = E_congsuat;
                s.HopSo = E_hopso;
                s.ChieuDai = E_chieudai;
                s.ChieuRong = E_chieurong;
                data.Cars.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var E_xe = data.Cars.First(m => m.MaXe == id);
            return View(E_xe);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var E_xe = data.Cars.First(m => m.MaXe == id);
            var E_tenxe = collection["TenXe"];
            var E_hinh = collection["Hinh"];
            var E_giaban = Convert.ToDecimal(collection["GiaTien"]);
            var E_soluongton = Convert.ToInt32(collection["SoLuong"]);
            //var E_hinh2 = collection["Hinh2"];
            //var E_hinh3 = collection["Hinh3"];
            var E_dongco = collection["DongCo"];
            var E_hopso = collection["HopSo"];
            var E_congsuat = collection["CongSuat"];
            var E_chieudai = Convert.ToDecimal(collection["ChieuDai"]);
            var E_chieurong = Convert.ToDecimal(collection["ChieuRong"]);
            //var E_loaixe = collection["Loai"];
            if (string.IsNullOrEmpty(E_tenxe))
            {
                ViewData["Error"] = "Dont Emty";
            }
            else
            {
                E_xe.TenXe = E_tenxe;
                E_xe.Hinh = E_hinh;
                E_xe.GiaBan = E_giaban;
                E_xe.SoLuongTon = E_soluongton;
                E_xe.DongCo = E_dongco;
                E_xe.HopSo = E_hopso;
                E_xe.CongSuat = E_congsuat;
                E_xe.ChieuDai = E_chieudai;
                E_xe.ChieuRong = E_chieurong;
                UpdateModel(E_xe);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var D_xe = data.Cars.First(m => m.MaXe == id);
            return View(D_xe);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_xe = data.Cars.Where(m => m.MaXe == id).First();
            data.Cars.DeleteOnSubmit(D_xe);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }

        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "Content/images/" + file.FileName;
        }
    }
}